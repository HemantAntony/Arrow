using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InitialiseLevel : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject endPrefab;
    [SerializeField] private GameObject arrowBounds;
    [SerializeField] private Transform ground;

    private Vector2 playerPositionOffset = new Vector2(1, 2);
    private float endPositionOffset = -1;

    void Start()
    {
        if (Camera.main.GetComponent<CameraMovement>() == null)
        {
            Vector2 scale = ground.localScale;
            scale.x = (Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) - Camera.main.ViewportToWorldPoint(new Vector2(0, 0))).x;
            ground.localScale = scale;
            PlayerBounds.instance.UpdatePlayerBounds();
        }

        Instantiate(playerPrefab, Camera.main.ViewportToWorldPoint(new Vector2(0, 0))
            + playerPrefab.transform.localScale / 2
            + (Vector3)playerPositionOffset, Quaternion.identity);
        float x = ground.position.x + ground.localScale.x / 2 - endPrefab.transform.localScale.x / 2 + endPositionOffset;
        float y = ground.position.y + ground.localScale.y / 2 + endPrefab.transform.localScale.y / 2;
        Instantiate(endPrefab, new Vector2(x, y), Quaternion.identity);

        CameraMovement cameraMovement = Camera.main.GetComponent<CameraMovement>();
        if (cameraMovement != null)
        {
            cameraMovement.Initialise();
        }

        arrowBounds.GetComponent<ArrowBounds>().Initialise();
    }
}
