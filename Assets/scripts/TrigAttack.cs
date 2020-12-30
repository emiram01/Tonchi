using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigAttack : MonoBehaviour
{
    [SerializeField] private int sawDamage = 1;
    [SerializeField] private bool roller = false;
    private float curr = 0;
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(!roller)
            {
                if(curr >= 0)
                {
                    Damage();
                    curr = -2;
                }
                else
                {
                    curr += Time.deltaTime;
                }
            }
            else if(roller)
            {
                if(curr >= 0)
                {
                    Damage();
                    curr = - .2f;
                }
                else
                {
                    curr += Time.deltaTime;
                }
            }
        }
    }

    void Damage()
    {
        FindObjectOfType<Health>().SubHealth(sawDamage);
    }
}
