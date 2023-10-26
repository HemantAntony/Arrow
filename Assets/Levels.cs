using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Levels : MonoBehaviour
{
    [SerializeField] private Transform levelsGrid;
    [SerializeField] private Curtain curtain;
    void Start()
    {
        for (int i = 0; i < levelsGrid.childCount; i++)
        {
            int j = i;
            levelsGrid.GetChild(i).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { ChooseLevel((j + 1).ToString()); });
        }
    }

    public void ChooseLevel(string text)
    {
        curtain.Close(() => SceneManager.LoadSceneAsync(text));
    }
}
