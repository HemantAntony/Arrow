using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator menuAnimator;

    public void OpenMenu()
    {
        menuAnimator.SetBool("OpenMenu", true);
    }

    public void CloseMenu()
    {
        menuAnimator.SetBool("OpenMenu", false);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadSceneAsync("Choose level");
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
