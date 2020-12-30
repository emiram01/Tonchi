using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZergActually : MonoBehaviour
{
    [SerializeField] private GameObject UI = null;
    [SerializeField] private GameObject col = null;
    public bool hurt = false;

    void Start()
    {
        col.SetActive(false);
    }

    IEnumerator hit()
    {
        yield return new WaitForSeconds(0f);
        FindObjectOfType<SpawnIn2>().clip.stop();
        UI.SetActive(false);
        col.SetActive(true);
    }

    void Update()
    {
        if(hurt == true)
        {
            StartCoroutine("hit");
        }
    }
}
