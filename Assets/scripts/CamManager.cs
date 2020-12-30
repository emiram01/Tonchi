using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField] private GameObject cam = null;
    [SerializeField] private GameObject coll = null;

    void OnTriggerStay2D(Collider2D other)
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), coll.GetComponent<Collider2D>());
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            cam.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            cam.SetActive(false);
        }
    }
}
