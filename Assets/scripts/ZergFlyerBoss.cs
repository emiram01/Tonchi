using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ZergFlyerBoss : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private int maxHealth = 500;
    [SerializeField] private float fireRate = 0;
    [SerializeField] private float lookRadius = 0;
    [SerializeField] private float shootRadius = 0;
    [SerializeField] private Transform player = null;
    [SerializeField] private Collider2D dead = null;
    [SerializeField] private GameObject UI = null;
    [SerializeField] private GameObject bar = null;
    [SerializeField] private GameObject laser = null;
    [SerializeField] private GameObject hurt = null;
    [SerializeField] private GameObject zergFlyer = null;
    [SerializeField] private RegularDoor door = null;
    [SerializeField] private AIPath aI = null;
    [SerializeField] private AudioSource bossMusic = null;
    private bool canTakeDam = true;
    private int currentHealth;
    private float fr;
    private float nextFire;
    private IntroLoop clip;
    private bool playMusic = true;

    void Start()
    {
        currentHealth = maxHealth;
        canTakeDam = true;
        fr = fireRate;
        nextFire = Time.time;
        clip = new IntroLoop(bossMusic, 0f, 21.6f, 44f);
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
                aI.enabled = false;
                animator.SetBool("IsDead", true);
                Invoke("Die", 0f);
            }
        }
    }

    void Die()
    {  
        clip.stop();
        FindObjectOfType<PlayerMovement>().clip.start();
        GetComponent<Collider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;
        zergFlyer.SetActive(false);
        UI.SetActive(false);
        hurt.SetActive(false);
        dead.enabled = true;
        this.enabled = false;
        door.MoveBack();
        FindObjectOfType<PlayerMovement>().isDJumpActive = true;
        FindObjectOfType<CharacterController2D>().isDJumpActive = true;
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            ShootLaser();
            nextFire = Time.time + fr;
        }
    }

    void ShootLaser()
    {
        FindObjectOfType<AudioManager>().Play("ZergBossFireball");
        GameObject las = Instantiate(laser) as GameObject;
        las.transform.position = this.transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        clip.checkTime();
        if(distance <= lookRadius)
        {
            UI.SetActive(true);
            aI.enabled = true;
            if(playMusic)
            {
                FindObjectOfType<PlayerMovement>().clip.stop();
                clip.start();
                playMusic = false;
            }
        }
        if(distance <= shootRadius)
        {
            CheckIfTimeToFire();
        }
        if(currentHealth <= 250)
        {
            zergFlyer.SetActive(true);
        }
        if(currentHealth <= 150)
        {
            aI.maxSpeed = 8;
            fr = 1;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }
}
