using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIn3 : MonoBehaviour
{
    [SerializeField] private GameObject tonchi = null;
    [SerializeField] private Animator trans = null;
    [SerializeField] private GameObject finalzerg = null;
    [SerializeField] private Transform spawn = null;
    [SerializeField] private AudioSource music = null;
    [SerializeField] private Transform scene1 = null;
    [SerializeField] private Transform scene2 = null;
    [SerializeField] private Transform scene3 = null;
    public IntroLoop clip;
    private bool end = true;
    private bool playMusic = true;

    void Start()
    {
        clip = new IntroLoop(music, 0f, 0f, 22.5f);
    }
    //finsih tmrw
    IEnumerator SceneManager()
    {
        yield return new WaitForSeconds(0f);
        FindObjectOfType<PlayerMovement>().canMove = false;
        yield return new WaitForSeconds(1f);
        trans.SetTrigger("Start");
        tonchi.transform.position = scene1.transform.position;
        FindObjectOfType<Health>().health = 9;
        trans.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        FindObjectOfType<FinalZerg>().clip.stop();
        if(playMusic)
        {
            clip.start();
            playMusic = false;
        }
        tonchi.transform.position = scene1.transform.position;
        yield return new WaitForSeconds(2f);
        trans.SetTrigger("End");
    }


    void Update()
    {
        clip.checkTime();
        if(finalzerg.activeInHierarchy == false && end)
        {
            StartCoroutine("SceneManager");
            end = false;
        }
    }
}
