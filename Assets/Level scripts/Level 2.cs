using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField] private Animator introtextAnimator;
    [SerializeField] private Animator fireTextAnimator;

    private Vector2 lastMousePosition;
    private bool introtextAnimationPlayed = false;
    void Update()
    {
        if (introtextAnimationPlayed)
        {
            return;
        }

        if (lastMousePosition != (Vector2) Input.mousePosition && Input.GetMouseButton(0))
        {
            Debug.Log("Going to animate");
            introtextAnimator.SetTrigger("FlipButton");
            fireTextAnimator.SetTrigger("FlipButton");
        }

        lastMousePosition = Input.mousePosition;
    }
}
