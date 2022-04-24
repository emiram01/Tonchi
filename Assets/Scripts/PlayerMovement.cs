using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb = null;
    [SerializeField] private Animator transition = null;
    [SerializeField] private Animator transition2 = null;
    [SerializeField] private CharacterController2D controller = null;
    [SerializeField] private GameObject laser = null;
    [SerializeField] private GameObject background = null;
    [SerializeField] private GameObject background2 = null;
    [SerializeField] private GameObject background3 = null;
    [SerializeField] public float runSpeed = 40f;
    [SerializeField] private float dashSpeed = 0f;
    [SerializeField] private float dashTime = 0f;
    [SerializeField] private float startDashTime = 0f;
    [SerializeField] private float dash = 0f;
    [SerializeField] private Transform lasSpawn = null;
    [SerializeField] private GameObject shootimg = null;
    [SerializeField] private GameObject dashimg = null;
    [SerializeField] private GameObject dJumpimg = null;
    [SerializeField] private AudioSource music = null;
    public Animator animator = null;
    public bool isShootActive = false;
    public bool isDashActive = false;
    public bool isDJumpActive = false;
    public bool canMove = true;
    public IntroLoop clip;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    private bool dead = false;
    private GameManager gm;
    private bool playMusic = true;

    void Start()
    {
        dashTime = startDashTime;
        gm = FindObjectOfType<GameManager>();
        clip = new IntroLoop(music, 0f, 13.33f, 87f);
    }

    void Update()
    {
        clip.checkTime();
        if(canMove)
        {
            if(playMusic)
            {
                clip.start();
                playMusic = false;
            }
            if(isShootActive)
            {
                shootimg.SetActive(true);
                Shoot();
            }
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            if(crouch == false)
            {
                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            }
            if(Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            if(Input.GetButtonDown("Crouch"))
            {
                crouch = true;
                animator.SetFloat("Speed", 0);
                animator.SetBool("IsJumping", false);
            }
            else if(Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }
    }

    void Shoot()
    {
        if(crouch == true && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJumping", false);
            if(controller.m_Grounded == false)
            {
                animator.SetBool("IsShooting", true);
            }
            this.GetComponent<PlayerCombat>().enabled = false;
            GameObject las = Instantiate(laser) as GameObject;
            las.transform.position = lasSpawn.transform.position;
            FindObjectOfType<AudioManager>().Play("TonchiFireball");
        }
    }

    void Dash()
    {
        if(!controller.m_Grounded && !crouch && Input.GetKey(KeyCode.Space) && dashTime <= 0 && !animator.GetBool("IsShooting"))
        {
            FindObjectOfType<ProgressBar>().IsDashActive();
            FindObjectOfType<AudioManager>().Play("Woosh");
            animator.SetBool("IsDashing", true);
            animator.SetBool("IsJumping", false);
            StartCoroutine("DashMove");
            dashTime = startDashTime;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }
    }

    IEnumerator DashMove()
    {
        runSpeed += dashSpeed;
        yield return new WaitForSeconds(dash);
        runSpeed -= dashSpeed;
    }

    void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsDashing", false);
        animator.SetBool("IsShooting", false);
        animator.SetBool("IsDJumping", false);
    }

    void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    public void DeathCheck()
    {    
        if (dead == false)
        {
            if(FindObjectOfType<Health>().health > 1)
            {
                transition.SetTrigger("Start");
                Invoke("Reland", 1f);
            } else {
                transition2.SetTrigger("Start");
                Invoke("Die", 3f);
            }
        }
        dead = true;
    }

    void Reland()
    {
        FindObjectOfType<PlayerPos>().Reland();
        FindObjectOfType<Health>().SubHealth(1);
    }

    void Die()
    {
        gm.Menu();
    }

    void DJump()
    {
        if(controller.dJumpCheck)
        {
            animator.SetBool("IsJumping", false);  
            animator.SetBool("IsDJumping", true);
        }
    }

    void FixedUpdate()
    {
        if(isDashActive)
        {
            dashimg.SetActive(true);
            Dash();
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        if(isDJumpActive)
        {
            dashimg.GetComponent<RectTransform>().anchoredPosition = new Vector3(309, -60, 0);
            dJumpimg.SetActive(true);
            DJump();
        }
        if(rb.position.y < 60f && rb.position.x < 520)
        {
            background.SetActive(true);
            background2.SetActive(false);
            background3.SetActive(false);
        }
        else if(rb.position.x > 520)
        {
            background.SetActive(false);
            background2.SetActive(true);
            background3.SetActive(false);
        }
        else
        {
            background.SetActive(false);
            background2.SetActive(false);
            background3.SetActive(true);
        }
        dead = false;
    }    
}
