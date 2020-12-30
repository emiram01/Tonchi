using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject boat = null;
    [SerializeField] private GameObject coll = null;
    private ZergRollerFollow[] rollers = null;

    void Start()
    {
        rollers = FindObjectsOfType<ZergRollerFollow>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), coll.GetComponent<Collider2D>());
        if(other.CompareTag("Player") == true)
        {
            player.GetComponent<PlayerMovement>().DeathCheck();
            boat.GetComponent<BoatMovement>().Dead();
            foreach(ZergRollerFollow roller in rollers)
            {
                roller.Dead();
            }
            //FindObjectOfType<ZergRollerFollow>().Dead();
        }
    }
}
