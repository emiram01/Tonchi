using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIn2 : MonoBehaviour
{
    [SerializeField] private GameObject tonchi = null;
    [SerializeField] private Animator trans = null;
    [SerializeField] private Collider2D stopper1 = null;
    [SerializeField] private Transform spawn = null;
    [SerializeField] private AudioSource music = null;
    [SerializeField] private GameObject bed1 = null;
    [SerializeField] private GameObject bed2 = null;
    [SerializeField] private GameObject blackTonchi = null;
    public IntroLoop clip;
    private bool end = true;
    private bool playMusic = true;

    void Start()
    {
        clip = new IntroLoop(music, 0f, 0f, 21.9f);
    }

    IEnumerator Relocate()
    {
        trans.SetTrigger("Start");
        FindObjectOfType<Health>().health = 9;
        blackTonchi.SetActive(true);
        bed1.SetActive(false);
        bed2.SetActive(true);
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
    }

    void Update()
    {
        clip.checkTime();
        if(stopper1.enabled == false && end)
        {
            StartCoroutine("Relocate");
            end = false;
        }
    }
}
