using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZerg : MonoBehaviour
{
    [SerializeField] private Collider2D stopper1 = null;
    [SerializeField] private CircleCollider2D flyColl = null;
    [SerializeField] private Collider2D coll = null;
    [SerializeField] private GameObject UI = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private int maxHealth = 1500;
    [SerializeField] private float lookRadius = 0;
    [SerializeField] private Transform target = null;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private GameObject bar = null;
    [SerializeField] private GameObject laser1 = null;
    [SerializeField] private Transform pos1 = null;
    [SerializeField] private Transform pos2 = null;
    [SerializeField] private Transform pos3 = null;
    [SerializeField] private Transform pos4 = null;
    [SerializeField] private Transform pos5 = null;
    [SerializeField] private Transform pos6 = null;
    [SerializeField] private GameObject roller1 = null;
    [SerializeField] private GameObject roller2 = null;
    [SerializeField] private GameObject roller3 = null;
    [SerializeField] private GameObject waller1 = null;
    [SerializeField] private GameObject waller2 = null;
    [SerializeField] private GameObject waller3 = null;
    [SerializeField] private GameObject waller4 = null;
    [SerializeField] private GameObject flyer1 = null;
    [SerializeField] private GameObject flyer2 = null;
    [SerializeField] private GameObject bossDoor1 = null;
    [SerializeField] private GameObject bossDoor2 = null;
    [SerializeField] private GameObject splat1 = null;
    [SerializeField] private GameObject splat2 = null;
    [SerializeField] private GameObject splat3 = null;
    [SerializeField] private GameObject dead = null;
    [SerializeField] private GameObject spikes = null;
    [SerializeField] private Transform[] waypoints = null;
    [SerializeField] private AudioSource bossMusic = null;
    [SerializeField] private float fireRate = 0;
    public bool isDead = false;
    public bool canTakeDam = false;
    public IntroLoop clip;
    private int waypointIndex = 0;
    private float fr;
    private float nextFire;
    private int currentHealth;
    private bool isActive = false;
    private bool end = true;
    private bool playMusic = true;
    private bool move = true;
    private bool move2 = true;
    private bool shoot1 = false;
    private bool shoot2 = false;
    private bool atk1 = true;

    void Start()
    {
        currentHealth = maxHealth;
        canTakeDam = false;
        fr = fireRate;
        nextFire = Time.time;
        transform.position = waypoints[waypointIndex].transform.position;
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
                FindObjectOfType<PlayerMovement>().runSpeed = 0f;
                FindObjectOfType<PlayerMovement>().rb.velocity = Vector2.zero;
                FindObjectOfType<PlayerMovement>().rb.angularVelocity = 0f;
                FindObjectOfType<PlayerMovement>().rb.Sleep();
                FindObjectOfType<PlayerMovement>().canMove = false;
                isDead = true;
                splat1.SetActive(true);
                splat2.SetActive(true);
                splat3.SetActive(true);
                animator.SetBool("Tired", false);
                animator.SetBool("Dead", true);
                clip.stop();
                FindObjectOfType<AudioManager>().Play("HeadDecap");
                Invoke("Die", 1f);
            }
        }
    }

    void CheckTimeToSpike()
    {
        if (Time.time > nextFire)
        {
            SpawnSpike();
            nextFire = Time.time + fr;
        }
    }

    void SpawnSpike()
    {
        GameObject spike = Instantiate(spikes) as GameObject;
        spike.transform.position = new Vector2(target.transform.position.x, 270.385f);
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            ShootLaser();
            nextFire = Time.time + fr;
        }
    }

    void CheckIfTimeToFire2()
    {
        if (Time.time > nextFire)
        {
            ShootLaser2();
            nextFire = Time.time + fr;
        }
    }

    void ShootLaser()
    {
        FindObjectOfType<AudioManager>().Play("ZergBossFireball");
        GameObject las = Instantiate(laser1) as GameObject;
        GameObject las2 = Instantiate(laser1) as GameObject;
        las.transform.position = pos3.transform.position;
        las2.transform.position = pos4.transform.position;
    }

    void ShootLaser2()
    {
        FindObjectOfType<AudioManager>().Play("ZergBossFireball");
        GameObject las = Instantiate(laser1) as GameObject;
        GameObject las2 = Instantiate(laser1) as GameObject;
        GameObject las3 = Instantiate(laser1) as GameObject;
        GameObject las4 = Instantiate(laser1) as GameObject;
        GameObject las5 = Instantiate(laser1) as GameObject;
        GameObject las6 = Instantiate(laser1) as GameObject;
        las.transform.position = pos1.transform.position;
        las2.transform.position = pos2.transform.position;
        las3.transform.position = pos3.transform.position;
        las4.transform.position = pos4.transform.position;
        las5.transform.position = pos5.transform.position;
        las6.transform.position = pos6.transform.position;
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
        canTakeDam = true;
        bossDoor1.GetComponent<RegularDoor>().canCloseL = true;
        bossDoor2.GetComponent<RegularDoor>().canCloseL = true;
        UI.SetActive(true);
        animator.SetBool("Start", true);
        isActive = true;
    }

    void Die()
    {  
        dead.SetActive(true);
        GetComponent<Collider2D>().enabled = false;
        if(roller1.activeInHierarchy)
            roller1.GetComponent<ZergRollerDam>().TakeDamage(200);
        if(roller2.activeInHierarchy)
            roller2.GetComponent<ZergRollerDam>().TakeDamage(200);
        if(roller3.activeInHierarchy)
            roller3.GetComponent<ZergRollerDam>().TakeDamage(200);
        if(waller1.activeInHierarchy)
            waller1.GetComponent<Zerg>().TakeDamage(200, 0 , 0);
        if(waller2.activeInHierarchy)
            waller2.GetComponent<Zerg>().TakeDamage(200, 0 , 0);
        if(waller3.activeInHierarchy)
            waller3.GetComponent<Zerg>().TakeDamage(200, 0 , 0);
        if(waller4.activeInHierarchy)
            waller4.GetComponent<Zerg>().TakeDamage(200, 0 , 0);
        if(flyer1.activeInHierarchy)
            flyer1.GetComponent<ZergFly>().TakeDamage(200);
        if(flyer2.activeInHierarchy)
            flyer2.GetComponent<ZergFly>().TakeDamage(200);
        this.enabled = false;
        UI.SetActive(false);
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
            if(atk1)
            {
                CheckTimeToSpike();
            }
            if(shoot1)
                CheckIfTimeToFire();
            if(shoot2)
                CheckIfTimeToFire2();
        }

        if(currentHealth <= 800)
        {
            waller1.SetActive(true);
            waller4.SetActive(true);
        }

        if(currentHealth <= 600)
        {
            if(move)
            {
                StartCoroutine("Move");
                move = false;
            }
            Movement();
            waller2.SetActive(true);
            waller3.SetActive(true);
            flyer1.SetActive(true);
            flyer2.SetActive(true);
        }

        if(currentHealth <= 200)
        {
            if(move2)
            {
                StartCoroutine("Move2");
                move2 = false;
            }
        }
    }

    IEnumerator Move()
    {
        atk1 = false;
        shoot1 = true;
        canTakeDam = false;
        animator.SetBool("Flying", true);
        coll.enabled = false;
        flyColl.enabled = true;
        fr -= 1.5f;
        yield return new WaitForSeconds(29.7f);
        coll.enabled = true;
        flyColl.enabled = false;
        atk1 = true;
        canTakeDam = true;
        shoot1 = false;
        animator.SetBool("Flying", false);
        fr += 1.5f;
    }

    IEnumerator Move2()
    {
        waypointIndex = 0;
        atk1 = false;
        shoot2 = true;
        canTakeDam = false;
        animator.SetBool("Flying", true);
        coll.enabled = false;
        flyColl.enabled = true;
        fr -= 1.5f;
        yield return new WaitForSeconds(29.7f);
        coll.enabled = true;
        flyColl.enabled = false;
        canTakeDam = true;
        shoot2 = false;
        animator.SetBool("Flying", false);
        animator.SetBool("Tired", true);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
