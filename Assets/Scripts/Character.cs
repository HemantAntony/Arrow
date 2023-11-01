using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Start()
    {
        instance = this;
    }
}
