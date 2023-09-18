using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    [SerializeField] private GameObject levelsGrid;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < levelsGrid.transform.childCount; i++)
        {
            Button button = levelsGrid.transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() => { ChooseLevel(button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text); });
        }
    }

    private void ChooseLevel(string text)
    {
        SceneManager.LoadSceneAsync(text);
    }
}
