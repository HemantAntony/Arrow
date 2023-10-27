using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseLevel : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector2 playerPositionOffset;
    [SerializeField] private GameObject endPrefab;
    [SerializeField] private Vector2 endPositionOffset;
    [SerializeField] private GameObject arrowBounds;
    void Start()
    {
        Instantiate(playerPrefab, Camera.main.ViewportToWorldPoint(new Vector2(0, 0))
            + playerPrefab.transform.localScale / 2
            + (Vector3)playerPositionOffset, Quaternion.identity);
        
        if (endPrefab != null )
        {
            Instantiate(endPrefab, (Vector2)Camera.main.ViewportToWorldPoint(new Vector3(1, 0))
                + new Vector2(-endPrefab.transform.localScale.x / 2, endPrefab.transform.localScale.y / 2)
                + endPositionOffset, Quaternion.identity);
        }

        CameraMovement cameraMovement = Camera.main.GetComponent<CameraMovement>();
        if (cameraMovement != null)
        {
            cameraMovement.Initialise();
        }

        arrowBounds.GetComponent<ArrowBounds>().Initialise();
    }
}
