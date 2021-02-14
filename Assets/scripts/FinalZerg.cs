using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZerg : MonoBehaviour
{
    [SerializeField] private Collider2D stopper1 = null;
    [SerializeField] private GameObject UI = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private int maxHealth = 1500;
    [SerializeField] private float lookRadius = 0;
    [SerializeField] private Transform target = null;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private GameObject bar = null;
    [SerializeField] private GameObject laser = null;
    [SerializeField] private GameObject bossDoor1 = null;
    [SerializeField] private GameObject bossDoor2 = null;
    [SerializeField] private AudioSource bossMusic = null;
    [SerializeField] private float fireRate = 0;
    public bool canTakeDam = true;
    private float fr;
    private float nextFire;
    private int currentHealth;
    private bool isActive = false;
    private bool end = true;
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
            // if(currentHealth <= 0)
            // {
            //     animator.SetBool("IsDead", true);
            //     Invoke("Die", 2f);
            // }
        }
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

    IEnumerator BossStart()
    {
        yield return new WaitForSeconds(0f);
        if(playMusic)
        {
            FindObjectOfType<ShipControls>().siren.Stop();
            clip.start();
            playMusic = false;
        }
        bossDoor1.GetComponent<RegularDoor>().canCloseL = true;
        bossDoor2.GetComponent<RegularDoor>().canCloseL = true;
        UI.SetActive(true);
        animator.SetBool("Start", true);
        isActive = true;
    }

    void Update()
    {
        clip.checkTime();
        if(stopper1.enabled == false && end)
        {
            StartCoroutine("BossStart");
            end = false;
        }

        if(isActive)
        {
            CheckIfTimeToFire();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
