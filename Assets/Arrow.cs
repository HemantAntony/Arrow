using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool fired = false;

    private List<string> detectCollisions = new List<string>() { "Bounds" };

    public void Fired()
    {
        fired = true;
    }

    private void Update()
    {
        if (!fired)
        {
            return;
        }

        Vector2 vel = GetComponent<Rigidbody2D>().velocity;
        if (vel.magnitude == 0f)
        {
            return;
        }

        float angle = Vector2.SignedAngle(Vector2.right, vel);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (detectCollisions.Contains(collision.tag))
        {
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
