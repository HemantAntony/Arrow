using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    internal static Character instance;

    [SerializeField] private RawImage healthBar;

    private int health = 100;

    public void decreaseHealth(int amount)
    {
        health -= amount;
        HealthBar.instance.UpdateBar(health);
    }

    public void showDeathAnimation()
    {
        GetComponent<Animator>().SetBool("Death", true);
    }

    public void onDeathAnimationFinished()
    {
        Curtain.Instance.Close(() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }

    void Start()
    {
        instance = this;
    }
}
