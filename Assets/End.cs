using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("HERE: " + collision.gameObject.name);
        if (collision.gameObject.tag != "Player")
        {
            return;
        }

        Debug.Log("Going to change");
        SceneManager.LoadSceneAsync("Choose level");
    }
}
