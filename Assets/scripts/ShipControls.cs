using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
    [SerializeField] private GameObject alarm = null;
    [SerializeField] private GameObject col = null;
    [SerializeField] private GameObject colDia = null;
    public bool hit = false;

    void Start()
    {
        col.SetActive(false);
    }

    IEnumerator Alarm()
    {
        yield return new WaitForSeconds(0f);
        FindObjectOfType<AudioManager>().Play("alarm");
        FindObjectOfType<SpawnIn2>().clip.stop();
        alarm.SetActive(true);
        col.SetActive(false);
        colDia.SetActive(true);
    }

    void Update()
    {
        if(hit == true)
        {
            StartCoroutine("Alarm");
        }
    }
}
