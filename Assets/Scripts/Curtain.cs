using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    internal static Curtain Instance;

    private Action Callback;

    public void RunCallback()
    {
        Callback?.Invoke();
    }
    public void Open(Action Callback = null)
    {
        UpdateCurtainSize();
        this.Callback = Callback;
        GetComponent<Animator>().SetBool("OpenCurtain", true);
    }

    public void Close(Action Callback = null)
    {
        UpdateCurtainSize();
        this.Callback = Callback;
        GetComponent<Animator>().SetBool("OpenCurtain", false);
    }

    private IEnumerator f()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<Animator>().SetBool("OpenCurtain", false);
    }


    private void Start()
    {
        Instance = this;
        Open();
    }

    private void UpdateCurtainSize()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width + 50, Screen.height + 50);
    }
}
