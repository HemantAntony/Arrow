using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    private Action Callback;

    public void RunCallback()
    {
        Callback?.Invoke();
    }
    public void Open(Action Callback = null)
    {
        UpdateCurtainSize();
        Debug.Log("Going to play");
        GetComponent<Animator>().SetBool("OpenCurtain", true);
        this.Callback = Callback;
    }

    public Action OnOpened;

    public void Close(Action Callback = null)
    {
        UpdateCurtainSize();
        GetComponent<Animator>().SetBool("OpenCurtain", false);
        this.Callback = Callback;
    }

    private void Start()
    {
        Open();
    }

    private void UpdateCurtainSize()
    {
        GetComponent<RectTransform>().localScale = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }
}
