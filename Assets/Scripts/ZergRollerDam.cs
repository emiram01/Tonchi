using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZergRollerDam : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private int maxHealth = 70;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private bool isBoss = false;
    private int currentHealth;
    private Vector3 startPos;
    //private bool isAlive = true;

	void Start () 
    {
        currentHealth = maxHealth;
        startPos = transform.position;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if(currentHealth <= 0)
        {
            animator.SetBool("IsDead", true);
            Invoke("Die", 0f);
            if(!isBoss)
            {
                Invoke("Respawn", 25f);
            }
        }
    }

    public void Die()
    {  
        //isAlive = false;
        Invoke("DieDie", .5f);
        GetComponent<Collider2D>().enabled = false;
        rb.isKinematic = false;
    }

    void DieDie()
    {
        this.gameObject.SetActive(false);
        this.enabled = false;
    }

    public void Respawn()
    {
        //isAlive = true;
        this.transform.position = startPos;
        GetComponent<Collider2D>().enabled = true;
        rb.isKinematic = true;
        currentHealth = maxHealth;
        this.enabled = true;
        this.gameObject.SetActive(true);
    }
}
