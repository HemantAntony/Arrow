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
        
        Vector2 direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rigidbody.AddForce(direction * firePower);

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
        return collider != null && (collider.CompareTag("Ground") || collider.CompareTag("Static"));
    }
}
