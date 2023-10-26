using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Levels : MonoBehaviour
{
    [SerializeField] private GameObject levelsGrid;
    [SerializeField] private GameObject levelButton;
    [SerializeField] private Curtain curtain;
    //[SerializeField] private Animator curtainAnimator;

    void Start()
    {
        Object[] levels = Resources.LoadAll("Levels");

        for (int i = 0; i < levels.Length; i++)
        {
            int j = i;
            var level = Instantiate(levelButton, Vector2.zero, Quaternion.identity);
            level.transform.SetParent(levelsGrid.transform, false);
            level.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            level.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { ChooseLevel((j + 1).ToString()); });
        }
    }

    private void ChooseLevel(string text)
    {
        curtain.Close(() => SceneManager.LoadSceneAsync(text));
    }
}
