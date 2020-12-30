using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(transform.position != (Vector3) gm.lastCheck)
            {
                FindObjectOfType<AudioManager>().Play("Checkpoint");
                gm.lastCheck = transform.position;
            }
        }
    }
}
