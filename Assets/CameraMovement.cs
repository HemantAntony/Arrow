using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;

    private float movementOffset;
    private float initialCameraPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movementOffset = (transform.position - player.transform.position).x;
        initialCameraPosition = transform.position.x;
    }

    private void Update()
    {
        if (transform.position.x != player.transform.position.x + movementOffset
            && transform.position.x >= initialCameraPosition)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x + movementOffset, initialCameraPosition, Mathf.Infinity),
                transform.position.y, transform.position.z);
        }
    }
}
