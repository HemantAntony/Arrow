using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    private float movementOffset;
    private float leftClampPosition;
    private float rightClampPosition;

    public void Initialise()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Transform ground = GameObject.FindGameObjectWithTag("Ground").transform;
        movementOffset = (transform.position - player.transform.position).x;
        leftClampPosition = transform.position.x;
        rightClampPosition = ground.position.x + ground.localScale.x / 2 - Camera.main.aspect * Camera.main.orthographicSize;
    }


    private void Update()
    {
        if (transform.position.x != player.transform.position.x + movementOffset)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x + movementOffset, leftClampPosition, rightClampPosition),
                transform.position.y, transform.position.z);
        }
    }
}
