using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZergFly : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private float amp = 0;
    [SerializeField] private float wave = 1;
    [SerializeField] private float lookRadius = 0;
    [SerializeField] private Transform target = null;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Animator animator = null;
    [SerializeField] private Rigidbody2D rb = null;
    private bool positveAndNegative = true;
    private bool positive = true;
    private Vector3 startPos;
    private int currentHealth;   
    private bool isAlive = true;

	void Start () 
    {
        currentHealth = maxHealth;
        startPos = transform.position;
	}

	void Update () 
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius && isAlive)
        {   
            Vector3 currPos = transform.position;
            currPos.x += speed * Time.deltaTime;
            currPos.y = startPos.y + amp * Mathf.Sin(wave * Mathf.PI * (currPos.x ));
            if (!positveAndNegative)
            {
                currPos.y = Mathf.Abs(currPos.y);
                if (!positive)
                {
                    currPos.y *= (-1);
                }
            }
            transform.position = currPos;
        }
        if(distance > 70 && isAlive)
        {
            transform.position = startPos;
        }
	}

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if(currentHealth <= 0)
        {
            animator.SetBool("IsDead", true);
            Invoke("Die", 0f);
            Invoke("Respawn", 25f);
        }
    }

    public void Die()
    {  
        isAlive = false;
        GetComponent<Collider2D>().enabled = false;
        rb.isKinematic = false;
        Invoke("DieDie", 5f);
    }

    void DieDie()
    {
        this.enabled = false;
        this.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        isAlive = true;
        this.transform.position = startPos;
        GetComponent<Collider2D>().enabled = true;
        rb.isKinematic = true;
        currentHealth = maxHealth;
        this.enabled = true;
        this.gameObject.SetActive(true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
