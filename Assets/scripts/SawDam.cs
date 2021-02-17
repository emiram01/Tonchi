using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawDam : MonoBehaviour
{
    [SerializeField] private int sawDamage = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damage();
        }
    }

    void Damage()
    {
        FindObjectOfType<Health>().SubHealth(sawDamage);
        FindObjectOfType<AudioManager>().Play("Slice");
    }
}
