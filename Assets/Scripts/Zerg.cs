using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zerg : MonoBehaviour
{
    [SerializeField] private GameObject laser = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float fireRate;
    [SerializeField] private float lookRadius = 0;
    [SerializeField] private Transform target = null;
    [SerializeField] private Transform laserSpawn = null;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Transform trans = null;
    [SerializeField] private Transform transB = null;
    [SerializeField] private float speed = 0f;
    [SerializeField] private bool flip = true;
    [SerializeField] private bool knockback = true;
    private float nextFire;
    private int currentHealth;   
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;
    

    void Start()
    {
        currentHealth = maxHealth;
        fireRate = 1.5f;
        nextFire = Time.time;
        posA = trans.localPosition;
        posB = transB.localPosition;
        nextPos = posB;
    }

    void Move()
    {
        trans.localPosition = Vector3.MoveTowards(trans.localPosition, nextPos, speed * Time.deltaTime);
        if(Vector3.Distance(trans.localPosition, nextPos) <= 0.1)
        {
            MoveBack();
        }
    }

    void MoveBack()
    {
        nextPos = nextPos != posA ? posA : posB;
    }

    public void TakeDamage(int damage, float knockbackx, float knockbacky)
    {
        currentHealth -= damage;
        if(knockback)
        {
            if(target.transform.position.x > this.transform.position.x)
            {
                rb.velocity = new Vector2(-knockbackx, knockbacky);
            }
            else
            {
                rb.velocity = new Vector2(knockbackx, knockbacky);
            }
        }
        animator.SetTrigger("Hurt");
        if(currentHealth <= 0)
        {
            animator.SetBool("IsDead", true);
            Invoke("Die", 1f);
            Invoke("Respawn", 25f);
        }
    }

    void ShootLaser()
    {
        GameObject las = Instantiate(laser) as GameObject;
        FindObjectOfType<AudioManager>().Play("Fireball");
        las.transform.position = laserSpawn.transform.position;
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            ShootLaser();
            nextFire = Time.time + fireRate;
        }
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         collision.collider.transform.SetParent(transform);
    //     }
    // }

    // void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         collision.collider.transform.SetParent(null);
    //     }
    // }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius)
        {
            CheckIfTimeToFire();
        }
        if(this.rb.position.y < -25)
        {
            Invoke("Die", 1f);
            Invoke("Respawn", 25f);
        }
    }

    void FixedUpdate()
    {
        Move();
        if(flip)
        {
            Flip();
        }
    }

    void Flip()
	{
        Vector3 rotation = transform.eulerAngles;
        if(target.transform.position.x > this.transform.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }
        transform.eulerAngles = rotation;
	}

    public void Die()
    {  
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        this.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        this.transform.position = posA;
        GetComponent<Collider2D>().enabled = true;
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
