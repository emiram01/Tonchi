using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZergRollerBoss : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private int maxHealth = 500;
    [SerializeField] private float lookRadius = 0;
    [SerializeField] private Transform player = null;
    [SerializeField] private Collider2D dead = null;
    [SerializeField] private Collider2D dead2 = null;
    [SerializeField] private GameObject dead3 = null;
    [SerializeField] private Collider2D hurt = null;
    [SerializeField] private PlatformDoor door = null;
    [SerializeField] private GameObject UI = null;
    [SerializeField] private GameObject bar = null;
    [SerializeField] private GameObject zerg1 = null;
    [SerializeField] private GameObject zerg2 = null;
    [SerializeField] private GameObject zerg3 = null;
    [SerializeField] private GameObject zerg4 = null;
    [SerializeField] private GameObject zerg5 = null;
    [SerializeField] private GameObject zerg6 = null;
    [SerializeField] private GameObject zerg7 = null;
    [SerializeField] private GameObject zerg8 = null;

    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private float atkMoveSpeed = 0f;
    [SerializeField] private Vector2 atkMoveDir;
    [SerializeField] private float atkPlayerSpeed = 0f;
    [SerializeField] private Transform top = null;
    [SerializeField] private Transform bottom = null;
    [SerializeField] private Transform wall = null;
    [SerializeField] private float radius = 0f;
    [SerializeField] private LayerMask ground = 0;
    [SerializeField] private AudioSource bossMusic = null;

    private bool topCheck;
    private bool bottomCheck;
    private bool wallCheck;
    private bool goingUp = true;
    private bool facingLeft = true;
    private Vector2 playerPos;
    private bool idle = true;

    private int currentHealth;
    private bool isActive;
    private bool canTakeDam;
    private float curr = -5;
    private bool atk1 = true;
    private bool atk2 = true;
    private IntroLoop clip;
    private bool playMusic = true;

    void Start()
    {
        currentHealth = maxHealth;
        canTakeDam = true;
        moveDir.Normalize();
        atkMoveDir.Normalize();
        clip = new IntroLoop(bossMusic, 0f, 11.294f, 79f);
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
                isActive = false;
                animator.SetBool("IsDead", true);
                Invoke("Die", 0f);
            }
        }
    }

    void Update () 
    {
        float distance = Vector3.Distance(player.position, transform.position);
        clip.checkTime();
        if(distance <= lookRadius)
        {
            UI.SetActive(true);
            isActive = true;
            if(playMusic)
            {
                FindObjectOfType<PlayerMovement>().clip.stop();
                clip.start();
                playMusic = false;
            }
        }
        if(isActive)
        {
            topCheck = Physics2D.OverlapCircle(top.position, radius, ground);
            bottomCheck = Physics2D.OverlapCircle(bottom.position, radius, ground);
            wallCheck = Physics2D.OverlapCircle(wall.position, radius, ground);
            if(currentHealth >= 350 || currentHealth <= 150)
            {
                Idle();
                if(curr >= 0)
                {
                    StartCoroutine("AttackPlayer");
                    curr = -5;
                }
                else
                {
                    curr += Time.deltaTime;
                }
                if(currentHealth <= 390 && atk1)
                {
                    FindObjectOfType<AudioManager>().Play("Woosh");
                    zerg1.SetActive(true);
                    zerg2.SetActive(true);
                    zerg3.SetActive(true);
                    atk1 = false;
                }
                if(currentHealth <= 100 && atk2)
                {
                    FindObjectOfType<AudioManager>().Play("Woosh");
                    zerg4.SetActive(true);
                    zerg5.SetActive(true);
                    zerg6.SetActive(true);
                    zerg7.SetActive(true);
                    zerg8.SetActive(true);
                    atk2 = false;
                }
            }
            if(currentHealth <= 350 && currentHealth >= 150)
            {
                animator.SetBool("IsAttacking", true);
                animator.SetBool("IsDashing", true);
                Attack();
            }
            else
            {
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsDashing", false);
            }
            
        }
	}

    void Idle()
    {
        if(idle)
        {
            if(topCheck && goingUp)
            {
                ChangeDir();
            }
            else if(bottomCheck && !goingUp)
            {
                ChangeDir();
            }
            if(wallCheck)
            {
                if(facingLeft)
                {
                    Flip();
                }
                else if(!facingLeft)
                {
                    Flip();
                }
            }
            rb.velocity = moveSpeed * moveDir;
        }
    }

    void Attack()
    {
        if(topCheck && goingUp)
        {
            ChangeDir();
        }
        else if(bottomCheck && !goingUp)
        {
            ChangeDir();
        }
        if(wallCheck)
        {
            if(facingLeft)
            {
                Flip();
            }
            else if(!facingLeft)
            {
                Flip();
            }
        }
        rb.velocity = atkMoveSpeed * atkMoveDir;
    }

    IEnumerator AttackPlayer()
    {
        idle = false;
        bool atk = true;
        if(atk)
        {
            FindObjectOfType<AudioManager>().Play("Woosh"); 
            playerPos = player.position - transform.position;
            playerPos.Normalize();
            rb.velocity = playerPos * atkPlayerSpeed;
        }
        yield return new WaitForSeconds(.5f);
        idle = true;
        atk = false;
    }

    void ChangeDir()
    {
        goingUp = !goingUp;
        moveDir.y *= -1;
        atkMoveDir.y *= -1;
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        moveDir.x *= -1;
        atkMoveDir.x *= -1;
        transform.Rotate(0, 180, 0);
    }

    void Die()
    {  
        clip.stop();
        FindObjectOfType<PlayerMovement>().clip.start();
        dead.enabled = true;
        dead2.enabled = false;
        dead3.SetActive(false);
        hurt.enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        UI.SetActive(false);
        this.enabled = false;
        door.MoveBack();
        FindObjectOfType<PlayerMovement>().isDashActive = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(top.position, radius);
        Gizmos.DrawWireSphere(bottom.position, radius);
        Gizmos.DrawWireSphere(wall.position, radius);
        //Gizmos.DrawWireSphere(transform.position, lookRadius1);
    }
}
