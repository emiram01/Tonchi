using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZergActually2 : MonoBehaviour
{
    [SerializeField] private GameObject tonchi = null;
    [SerializeField] private Animator trans = null;
    [SerializeField] private Collider2D stopper1 = null;
    [SerializeField] private Transform spawn = null;
    [SerializeField] private GameObject col = null;
    [SerializeField] private AudioSource music = null;
    private bool end = true;
    public IntroLoop clip;
    private bool playMusic = true;
    
    void Start()
    {
        col.SetActive(false);
        clip = new IntroLoop(music, 0f, 0f, 21.9f);
    }

    IEnumerator almostDone()
    {
        trans.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        FindObjectOfType<SpawnIn>().clip.stop();
        if(playMusic)
        {
            clip.start();
            playMusic = false;
        }
        tonchi.transform.position = spawn.position;
        yield return new WaitForSeconds(2f);
        trans.SetTrigger("End");
        col.SetActive(true);
    }

    void Update()
    {
        clip.checkTime();
        if(stopper1.enabled == false && end)
        {
            StartCoroutine("almostDone");
            end = false;
        }
    }
}
