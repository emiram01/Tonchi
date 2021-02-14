using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
    [SerializeField] private GameObject alarm = null;
    [SerializeField] private GameObject col = null;
    [SerializeField] private GameObject colDia = null;
    [SerializeField] private GameObject bossDoor1 = null;
    [SerializeField] private GameObject bossDoor2 = null;
    [SerializeField] private GameObject lock1 = null;
    [SerializeField] private GameObject lock2 = null;
    [SerializeField] private AudioSource siren = null;
    public bool hit = false;

    IEnumerator Alarm()
    {
        yield return new WaitForSeconds(0f);
        FindObjectOfType<SpawnIn2>().clip.stop();
        siren.Play();
        bossDoor1.GetComponent<RegularDoor>().canCloseL = true;
        bossDoor2.GetComponent<RegularDoor>().canCloseL = true;
        lock1.GetComponent<RegularDoor>().canCloseL = true;
        lock2.GetComponent<RegularDoor>().canCloseR = true;
        FindObjectOfType<AudioManager>().Play("alarm");
        FindObjectOfType<SpawnIn2>().clip.stop();
        alarm.SetActive(true);
        col.SetActive(false);
        colDia.SetActive(true);
    }

    void Update()
    {
        if(hit)
        {
            StartCoroutine("Alarm");
            hit = false;
        }
    }
}
