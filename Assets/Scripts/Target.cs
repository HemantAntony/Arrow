using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private bool destroy = false;

    private void Update()
    {
        if (!destroy)
        {
            return;
        }

        Color color = GetComponent<SpriteRenderer>().color;
        color.a -= 0.01f;

        if (color.a < 0)
        {
            Destroy(gameObject);
        }
        GetComponent<SpriteRenderer>().color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Arrow" && !collision.gameObject.GetComponent<Arrow>().fired)
        {
            return;
        }

        destroy = true;
        Destroy(collision.gameObject);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
