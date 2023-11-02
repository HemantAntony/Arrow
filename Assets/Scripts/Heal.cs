using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private bool healed = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" || healed)
        {
            return;
        }

        Character.instance.setHealth(100);
        healed = true;
        GetComponent<Animator>().SetBool("Healed", true);
    }
}
