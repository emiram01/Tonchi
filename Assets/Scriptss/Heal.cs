using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private int healthAmount = 1;
    private Health currHealth = null;

    void Start()
    {
        currHealth = FindObjectOfType<Health>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && currHealth.health < 9)
        {
            currHealth.AddHealth(healthAmount);
            Destroy(gameObject);
        }
    }
}