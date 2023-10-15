using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;

    void Start()
    {
        Vector2 start = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 end = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Transform wall = Instantiate(wallPrefab, Vector2.zero, Quaternion.identity).transform;
        wall.localScale = new Vector2(end.x - start.x + 1f, 1f);
        wall.position = new Vector2((end.x + start.x) / 2, end.y + wall.transform.localScale.y / 2);
        wall.parent = transform;

        wall = Instantiate(wallPrefab, Vector2.zero, Quaternion.identity).transform;
        wall.localScale = new Vector2(1f, end.y - start.y + 1f);
        wall.position = new Vector2(start.x - wall.transform.localScale.x / 2, (start.y + end.y) / 2);
        wall.parent = transform;

        if (Camera.main.GetComponent<CameraMovement>())
        {
            return;
        }

        wall = Instantiate(wallPrefab, Vector2.zero, Quaternion.identity).transform;
        wall.localScale = new Vector2(1f, end.y - start.y + 1f);
        wall.position = new Vector2(end.x + wall.transform.localScale.x / 2, (start.y + end.y) / 2);
        wall.parent = transform;
    }
}
