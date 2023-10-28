using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
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
        Curtain.Instance.Close(() => SceneManager.LoadSceneAsync("Choose level"));
    }

    public void RestartLevel()
    {
        Curtain.Instance.Close(() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }
}
