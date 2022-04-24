using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image img = null;
    [SerializeField] private float max = 3;
    [SerializeField] private float curr = 3;

    void Start()
    {
        curr = 0;
    }

    void GetCurrFill()
    {
        float fill = curr / max;
        img.fillAmount = fill;
    }

    void Update()
    {
        GetCurrFill();
        curr += Time.deltaTime;
    }

    public void IsDashActive()
    {
        curr = 0;
    }
}
