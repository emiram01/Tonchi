using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZergBoss : MonoBehaviour
{
    [SerializeField] private GameObject laser = null;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private int maxHealth = 500;
    [SerializeField] private float fireRate = 0;
    [SerializeField] private float lookRadius = 0;
    [SerializeField] private float lookRadius1 = 0;
    [SerializeField] private Transform target = null;
    [SerializeField] private Transform posA = null;
    [SerializeField] private Transform posB = null;
    [SerializeField] private Collider2D dead = null;
    [SerializeField] private PlatformDoor door = null;
    [SerializeField] private GameObject UI = null;
    [SerializeField] private GameObject atk = null;
    [SerializeField] private GameObject atk2 = null;
    [SerializeField] private ParticleSystem dust = null;
    [SerializeField] private ParticleSystem dust2 = null;
    [SerializeField] private Transform[] waypoints = null;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private GameObject bar = null;
    [SerializeField] private AudioSource bossMusic = null;
    private float fr;
    private float nextFire;
    private int currentHealth;
    private bool isActive;
    private bool attack = true;
    private bool attack2 = true;
    private bool move = true;
    private bool canTakeDam;
    private int waypointIndex = 0;
    private IntroLoop clip;
    private bool playMusic = true;

    void Start()
    {
        currentHealth = maxHealth;
        canTakeDam = true;
        fr = fireRate;
        nextFire = Time.time;
        transform.position = waypoints[waypointIndex].transform.position;
        clip = new IntroLoop(bossMusic, 0f, 5.18f, 47.53f);
    }

    public void TakeDamage(int damage)
    {
        if(canTakeDam)
        {
            currentHealth -= damage;
            bar.GetComponent<ZergHealthBar>().curr -= damage;
            animator.SetTrigger("Hurt");
            if(currentHealth <= 0)
            {
                animator.SetBool("IsDead", true);
                Invoke("Die", 2f);
            }
        }
    }

    void ShootLaser()
    {
        FindObjectOfType<AudioManager>().Play("ZergBossFireball");
        GameObject las = Instantiate(laser) as GameObject;
        GameObject las2 = Instantiate(laser) as GameObject;
        GameObject las3 = Instantiate(laser) as GameObject;
        las.transform.position = this.transform.position;
        las2.transform.position = posA.transform.position;
        las3.transform.position = posB.transform.position;
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            ShootLaser();
            nextFire = Time.time + fr;
        }
    }

    void Update () 
    {
        float distance = Vector3.Distance(target.position, transform.position);
        clip.checkTime();
        if(distance <= lookRadius1)
        {
            UI.SetActive(true);
            isActive = true;
            animator.SetBool("IsActive", true);
            
            if(playMusic)
            {
                FindObjectOfType<PlayerMovement>().clip.stop();
                clip.start();
                playMusic = false;
            }
        }
        if(distance <= lookRadius && isActive)
        {
            CheckIfTimeToFire();
        }
        if(currentHealth <= 350 && attack)
        {
            StartCoroutine("Attack");
            ScreenShake.Instance.Shake(5f, 7f);
            attack = false;
        }
        if(currentHealth <= 200)
        {
            if(move)
            {
                StartCoroutine("Move");
                move = false;
            }
            Movement();
        }
        if(currentHealth <= 130 && attack2)
        {
            StartCoroutine("Attack");
            ScreenShake.Instance.Shake(5f, 7f);
            attack2 = false;
        }
	}
    
    IEnumerator Move()
    {
        animator.SetBool("IsMoving", true);
        fr -= 1.5f;
        yield return new WaitForSeconds(13);
        animator.SetBool("IsMoving", false);
        fr += 1.5f;
    }

    private void Movement()
    {
        if(waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
            if(transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }

    IEnumerator Attack()
    {
        canTakeDam = false;
        animator.SetBool("IsAttacking", true);
        dust.Play();
        // atk.SetActive(true);
        StartCoroutine("AttackDam");
        StartCoroutine("Attack2");
        yield return new WaitForSeconds(7);
        canTakeDam = true;
        atk.SetActive(false);
        animator.SetBool("IsAttacking", false);
    }

    IEnumerator Attack2()
    {
        yield return new WaitForSeconds(2);
        atk2.SetActive(true);
        dust2.Play();
        yield return new WaitForSeconds(5);
        atk2.SetActive(false);
    }

    IEnumerator AttackDam()
    {
        yield return new WaitForSeconds(1);
        atk.SetActive(true);
    }

    void Die()
    {  
        clip.stop();
        FindObjectOfType<PlayerMovement>().clip.start();
        GetComponent<Collider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        UI.SetActive(false);
        dead.enabled = true;
        this.enabled = false;
        door.MoveBack();
        FindObjectOfType<PlayerMovement>().isShootActive = true;
        //this.gameObject.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, lookRadius1);
    }
}
