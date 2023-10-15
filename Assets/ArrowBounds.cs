using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ArrowBounds : MonoBehaviour
{
    [SerializeField] private GameObject arrowBoundPrefab;

    private GameObject player;
    private GameObject leftArrowBound;
    private float boundPositionOffset = 20;
    private float movementOffset;
    private float initialLeftArrowBoundPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movementOffset = (transform.position - player.transform.position).x;
        initialLeftArrowBoundPosition = transform.position.x;

        Vector3 offset = new Vector3(boundPositionOffset, boundPositionOffset);
        Vector2 start = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) - offset;
        Vector2 end = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) + offset;

        Transform wall = Instantiate(arrowBoundPrefab, Vector2.zero, Quaternion.identity).transform;
        wall.localScale = new Vector2(end.x - start.x + 1f, 1f);
        wall.position = new Vector2((end.x + start.x) / 2, end.y + wall.transform.localScale.y / 2);
        wall.parent = transform;

        wall = Instantiate(arrowBoundPrefab, Vector2.zero, Quaternion.identity).transform;
        wall.localScale = new Vector2(end.x - start.x + 1f, 1f);
        wall.position = new Vector2((end.x + start.x) / 2, start.y - wall.transform.localScale.y / 2);
        wall.parent = transform;

        wall = Instantiate(arrowBoundPrefab, Vector2.zero, Quaternion.identity).transform;
        wall.localScale = new Vector2(1f, end.y - start.y + 1f);
        wall.position = new Vector2(start.x - wall.transform.localScale.x / 2, (start.y + end.y) / 2);
        wall.parent = transform;

        wall = Instantiate(arrowBoundPrefab, Vector2.zero, Quaternion.identity).transform;
        wall.localScale = new Vector2(1f, end.y - start.y + 1f);
        wall.position = new Vector2(end.x + wall.transform.localScale.x / 2, (start.y + end.y) / 2);
        wall.parent = transform;
    }
    private void Update()
    {
        if (transform.position.x != player.transform.position.x + movementOffset
            && transform.position.x >= initialLeftArrowBoundPosition)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x + movementOffset, initialLeftArrowBoundPosition, Mathf.Infinity),
                transform.position.y, transform.position.z);
        }
    }
}
