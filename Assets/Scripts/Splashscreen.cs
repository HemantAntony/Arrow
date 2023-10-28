using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splashscreen : MonoBehaviour
{
    public void OnAnimationFinish()
    {
        SceneManager.LoadSceneAsync("Choose level");
    }
}
