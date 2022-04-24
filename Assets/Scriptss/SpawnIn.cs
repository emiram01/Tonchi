using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIn : MonoBehaviour
{
    [SerializeField] private GameObject stopper = null;
    [SerializeField] private Collider2D stopper1 = null;
    [SerializeField] private GameObject zerg = null;
    [SerializeField] private GameObject boat = null;
    [SerializeField] private GameObject lokk = null;
    [SerializeField] private GameObject smoke = null;
    [SerializeField] private AudioSource music = null;
    [SerializeField] private GameObject cam = null;
    public IntroLoop clip;
    private bool playMusic = true;
    private  bool end = true;

    void Start()
    {
        clip = new IntroLoop(music, 0f, 2.8f, 22f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine("Scene");
        }
    }

    IEnumerator Scene()
    {
        yield return new WaitForSeconds(3f);
        stopper.SetActive(true);
        zerg.SetActive(true);
        lokk.SetActive(true);
        cam.SetActive(true);
        Destroy(boat.gameObject);
    }
    
    IEnumerator Smoke()
    {
        FindObjectOfType<PlayerMovement>().clip.stop();
        if(playMusic)
        {
            clip.start();
            playMusic = false;
        }
        yield return new WaitForSeconds(.5f);
        smoke.SetActive(true);
    }

    void Update()
    {
        clip.checkTime();
        if(stopper1.enabled == false && end)
        {
            StartCoroutine("Smoke");
            end = false;
        }
    }
}
