using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private Animator intro = null;
    [SerializeField] private GameObject black = null;
    [SerializeField] private AudioSource introMusic = null;
    public static GameManager instance;
    public Vector2 spawnPoint;
    public Vector2 lastCheck;
    public int currHealth;
    private bool canSkip = true;

    void Awake()
    {
        anim.SetTrigger("End");
        StartCoroutine("Intro");
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Intro()
    {
        intro.SetTrigger("Scroll");
        introMusic.Play();
        FindObjectOfType<PlayerCombat>().canAtk = false;
        FindObjectOfType<PlayerMovement>().canMove = false;
        yield return new WaitForSeconds(28f);
        if(canSkip)
        {
            black.SetActive(false);
            anim.SetTrigger("End");
            FindObjectOfType<PlayerCombat>().canAtk = true;
            FindObjectOfType<PlayerMovement>().canMove = true;
            canSkip = false;
        }
    }

    IEnumerator canHit()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<PlayerCombat>().canAtk = true;
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Destroy(gameObject);
    } 

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canSkip)
        {
            introMusic.Stop();
            intro.gameObject.SetActive(false);
            black.SetActive(false);
            anim.SetTrigger("End");
            StartCoroutine("canHit");
            FindObjectOfType<PlayerMovement>().canMove = true;
            canSkip = false;
        }
    }
}
