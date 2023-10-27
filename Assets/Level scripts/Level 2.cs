using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector2 playerPositionOffset;
    [SerializeField] private GameObject endPrefab;
    [SerializeField] private Vector2 endPositionOffset;
    [SerializeField] private Animator introtextAnimator;
    [SerializeField] private Animator fireTextAnimator;

    private Vector2 lastMousePosition;
    private bool introtextAnimationPlayed = false;

    void Start()
    {
        Instantiate(playerPrefab, Camera.main.ViewportToWorldPoint(new Vector2(0, 0))
            + playerPrefab.transform.localScale / 2
            + (Vector3)playerPositionOffset, Quaternion.identity);

        Instantiate(endPrefab, (Vector2)Camera.main.ViewportToWorldPoint(new Vector3(1, 0))
            + new Vector2(-endPrefab.transform.localScale.x / 2, endPrefab.transform.localScale.y / 2)
            + endPositionOffset, Quaternion.identity);

    }
    void Update()
    {
        if (introtextAnimationPlayed)
        {
            return;
        }

        if (lastMousePosition != (Vector2) Input.mousePosition && Input.GetMouseButton(0))
        {
            introtextAnimator.SetTrigger("FlipButton");
            fireTextAnimator.SetTrigger("FlipButton");
        }

        lastMousePosition = Input.mousePosition;
    }
}
