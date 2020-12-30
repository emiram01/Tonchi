using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZerg : MonoBehaviour
{
    [SerializeField] private GameObject zerg = null;
    [SerializeField] private Collider2D stopper1 = null;
    [SerializeField] private GameObject UI = null;
    private bool end = true;


    void Start()
    {
        zerg.GetComponent<ZergFlyerRotate>().enabled = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            zerg.GetComponent<ZergFlyerRotate>().enabled = true;
        }
    }

    IEnumerator UIstart()
    {
        yield return new WaitForSeconds(0f);
        UI.SetActive(true);
    }

    void Update()
    {
        if(stopper1.enabled == false && end)
        {
            StartCoroutine("UIstart");
            end = false;
        }
    }
}
