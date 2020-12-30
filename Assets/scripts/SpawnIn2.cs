using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIn2 : MonoBehaviour
{
    [SerializeField] private GameObject tonchi = null;
    //[SerializeField] private GameObject finalZerg = null;
    [SerializeField] private Animator trans = null;
    [SerializeField] private Collider2D stopper1 = null;
    [SerializeField] private Transform spawn = null;
    [SerializeField] private GameObject shootimg = null;
    [SerializeField] private GameObject dashimg = null;
    [SerializeField] private GameObject dJumpimg = null;
    [SerializeField] private AudioSource music = null;
    //private GameManager gm;
    //private int hearts;
    public IntroLoop clip;
    private bool end = true;
    private bool playMusic = true;

    void Start()
    {
        clip = new IntroLoop(music, 0f, 0f, 22.5f);
    }

    void Update()
    {
        clip.checkTime();
        if(stopper1.enabled == false && end)
        {
            StartCoroutine("Dead");
            end = false;
        }
    }

    IEnumerator Dead()
    {
        trans.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        FindObjectOfType<SpawnIn>().clip.stop();
        if(playMusic)
        {
            clip.start();
            playMusic = false;
        }
        dashimg.SetActive(false);
        shootimg.SetActive(false);
        dJumpimg.SetActive(false);
        tonchi.GetComponent<PlayerMovement>().isDashActive = false;
        tonchi.GetComponent<PlayerMovement>().isDJumpActive = false;
        tonchi.GetComponent<PlayerMovement>().isShootActive = false;
        tonchi.GetComponent<CharacterController2D>().isDJumpActive = false;
        tonchi.transform.position = spawn.position;
        //hearts = 9;
        yield return new WaitForSeconds(2f);
        trans.SetTrigger("End");
    }
}
