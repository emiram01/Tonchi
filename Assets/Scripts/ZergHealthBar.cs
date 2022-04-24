using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZergHealthBar : MonoBehaviour
{
    [SerializeField] private Image img = null;
    [SerializeField] private float max = 500;
    public float curr = 500;

    void Start()
    {
        curr = max;
    }

    void GetCurrFill()
    {
        float fill = curr / max;
        img.fillAmount = fill;
    }

    void Update()
    {
        GetCurrFill();
        //curr += Time.deltaTime;
    }
}
