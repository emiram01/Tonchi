using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZergChange : MonoBehaviour
{
    [SerializeField] private Collider2D stopper = null;
    [SerializeField] private GameObject stopper1 = null;
    [SerializeField] private Animator zerg = null;
    private bool spawned = false;

    void Update()
    {
        if(stopper.enabled == false && !spawned)
        {
            stopper1.SetActive(true);
            zerg.SetBool("Trans", true);
            spawned = true;
        }
    }
}
