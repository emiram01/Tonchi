using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZergDam : MonoBehaviour
{
    [SerializeField] private int zergDamage = 1;
    private float curr = 0;
    private float nextDam = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(curr <= 0)
            {
                Damage();
                curr = nextDam;
            }
            else
            {
                curr -= 1;
            }
        }
    }

    void Damage()
    {
        FindObjectOfType<Health>().SubHealth(zergDamage);
    }
}
