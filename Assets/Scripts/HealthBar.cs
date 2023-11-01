using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    internal static HealthBar instance;

    [SerializeField] private float scalingSpeed;

    public void UpdateBar(int amount)
    {
        StartCoroutine(ChangeBar(GetComponent<Slider>().value, (float)amount / 100));

        if (amount == 0)
        {
            Character.instance.showDeathAnimation();
        }
    }

    private void Start()
    {
        instance = this;
    }

    private IEnumerator ChangeBar(float start, float end)
    {
        float t = 0.0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2;
            GetComponent<Slider>().value = Mathf.Lerp(start, end, t);

            yield return null;
        }
    }
}
