using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform arrowPivot;
    [SerializeField] private Transform arrow;
    [SerializeField] private GameObject gripPrefab;
    [SerializeField] private GameObject arrowPivotPrefab;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private int jumpPower = 400;
    [SerializeField] private int firePower = 100;

    private Rigidbody2D playerRigidBody;

    private float raycastDistance = 0.8f;
    private float reloadTime = 1f;

    void Start()
    {
        playerRigidBody = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MousePositionChanged();
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseReleased();
        }

        MovePlayer();

        if (Input.GetButton("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    private void MousePositionChanged()
    {
        DestroyGrips();

        if (!arrowPivot)
        {
            return;
        }

        CreateGrips();
        RotateArrow();
    }

    private void MouseReleased()
    {
        DestroyGrips();

        if (!arrowPivot)
        {
            return;
        }
        FireArrow();
        Invoke("ReloadArrow", reloadTime);
    }

    private void DestroyGrips()
    {
        foreach (Transform gripTransform in transform)
        {
            if (gripTransform.CompareTag("Grip"))
            {
                Destroy(gripTransform.gameObject);
            }
        }
    }

    private void CreateGrips()
    {
        float minGray = 0.73f;
        float maxGray = 0.60f;
        int gripNumber = 5;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Vector2?
        mousePos.z = -transform.localScale.z / 2;
        Vector3 division = (transform.position - mousePos) / gripNumber;
        division.z = 0;

        for (int i = 0; i < gripNumber; i++)
        {
            GameObject grip = Instantiate(gripPrefab, mousePos + i * division, Quaternion.identity);
            grip.transform.SetParent(transform);
            grip.name = "Grip " + (i + 1);
            grip.tag = "Grip";

            float rgb = minGray - i * (minGray - maxGray) / (gripNumber - 1);
            Color color = new Color(rgb, rgb, rgb);
            grip.GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void RotateArrow()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 orientation = transform.position - mousePos;
        float angle = Vector2.SignedAngle(Vector2.right, orientation);
        arrowPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void FireArrow()
    {
        arrowPivot.parent = null;
        Rigidbody2D rigidbody = arrow.AddComponent<Rigidbody2D>();

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 bottomRight = new Vector2(topRight.x, bottomLeft.y);
        Vector2 topLeft = new Vector2(bottomLeft.x, topRight.y);

        float finalMousePositionX = Mathf.Clamp(mousePosition.x, bottomLeft.x, topRight.x);
        float finalMousePositionY = Mathf.Clamp(mousePosition.y, bottomLeft.y, topRight.y);
        Vector2 finalMousePosition = new Vector2(finalMousePositionX, finalMousePositionY);
        Vector2 arrowDirection = transform.position - (Vector3) finalMousePosition;
        
        float angleBtwArrowDirectionBottomLeft = Vector2.SignedAngle((Vector2)transform.position - bottomLeft, arrowDirection);
        float angleBtwBottomLeftTopLeft = Vector2.SignedAngle((Vector2)transform.position - bottomLeft, (Vector2)transform.position - topLeft);
        float angleBtwBottomLeftTopRight = Vector2.SignedAngle((Vector2)transform.position - bottomLeft, (Vector2)transform.position - topRight);
        float angleBtwBottomLeftBottomRight = Vector2.SignedAngle((Vector2)transform.position - bottomLeft, (Vector2)transform.position - bottomRight);

        // y = mx + c
        // Finding c
        // The finding x or y

        if (angleBtwArrowDirectionBottomLeft < 0f)
        {
            angleBtwArrowDirectionBottomLeft = 360 + angleBtwArrowDirectionBottomLeft;
        }

        if (angleBtwBottomLeftTopLeft < 0f)
        {
            angleBtwBottomLeftTopLeft = 360 + angleBtwBottomLeftTopLeft;
        }

        if (angleBtwBottomLeftTopRight < 0f)
        {
            angleBtwBottomLeftTopRight = 360 + angleBtwBottomLeftTopRight;
        }

        if (angleBtwBottomLeftBottomRight < 0f)
        {
            angleBtwBottomLeftBottomRight = 360 + angleBtwBottomLeftBottomRight;
        }

        float maxMagntiude = 1f;

        if (angleBtwArrowDirectionBottomLeft < angleBtwBottomLeftBottomRight)
        {
            float c = finalMousePositionY - arrowDirection.y / arrowDirection.x * finalMousePositionX;
            float x = (bottomLeft.y - c) / (arrowDirection.y / arrowDirection.x);
            maxMagntiude = ((Vector2)transform.position - new Vector2(x, bottomLeft.y)).magnitude;
        } else if (angleBtwArrowDirectionBottomLeft < angleBtwBottomLeftTopRight)
        {
            float c = finalMousePositionY - arrowDirection.y / arrowDirection.x * finalMousePositionX;
            float y = arrowDirection.y / arrowDirection.x * bottomRight.x + c;
            maxMagntiude = ((Vector2)transform.position - new Vector2(bottomRight.x, y)).magnitude;
        }
        else if (angleBtwArrowDirectionBottomLeft < angleBtwBottomLeftTopLeft)
        {
            float c = finalMousePositionY - arrowDirection.y / arrowDirection.x * finalMousePositionX;
            float x = (topLeft.y - c) / (arrowDirection.y / arrowDirection.x);
            maxMagntiude = ((Vector2)transform.position - new Vector2(x, topLeft.y)).magnitude;
        } else if (angleBtwArrowDirectionBottomLeft < 360f)
        {
            float c = finalMousePositionY - arrowDirection.y / arrowDirection.x * finalMousePositionX;
            float y = arrowDirection.y / arrowDirection.x * bottomLeft.x + c;
            maxMagntiude = ((Vector2)transform.position - new Vector2(bottomLeft.x, y)).magnitude;
        }

        rigidbody.AddForce(arrowDirection / maxMagntiude * firePower);

        arrow.GetComponent<Arrow>().Fired();

        arrowPivot = null;
        arrow = null;
    }

    private void ReloadArrow()
    {
        GameObject gameobject = Instantiate(arrowPivotPrefab, transform.position, Quaternion.identity);
        gameobject.name = gameobject.name.Replace("(Clone)", "").Trim();
        gameobject.transform.parent = player.transform;
        arrowPivot = gameobject.transform;
        arrow = gameobject.transform.Find("Arrow");
    }

    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        playerRigidBody.velocity = new Vector2(horizontalInput * playerSpeed, playerRigidBody.velocity.y);
    }

    private void Jump()
    {
        playerRigidBody.totalForce = new Vector2(0, 0);
        playerRigidBody.AddForce(new Vector2(0, jumpPower));
    }

    private bool IsGrounded()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance);
        Collider2D collider = groundCheck.collider;
        return collider != null && (collider.CompareTag("Ground") || collider.CompareTag("Static") || collider.CompareTag("Harm"));
    }
}
