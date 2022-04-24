using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIn3 : MonoBehaviour
{
    [SerializeField] private GameObject tonchi = null;
    [SerializeField] private GameObject[] tonchiUI = null;
    [SerializeField] private Animator trans = null;
    [SerializeField] private GameObject finalzerg = null;
    [SerializeField] private AudioSource music = null;
    [SerializeField] private Transform scene1 = null;
    [SerializeField] private Transform scene2 = null;
    [SerializeField] private Transform scene3 = null;
    [SerializeField] private GameObject[] zergs1 = null;
    [SerializeField] private GameObject[] zergflyers1 = null;
    [SerializeField] private GameObject[] zergs2 = null;
    [SerializeField] private GameObject[] zergflyers2 = null;
    [SerializeField] private GameObject bTonchi = null;
    [SerializeField] private Collider2D dialogue = null;
    [SerializeField] private Animator credits = null;
    [SerializeField] private GameObject cam = null;
    public IntroLoop clip;
    private GameManager gm;
    private bool end = true;
    private bool end2 = true;
    private bool playMusic = true;

    void Start()
    {
        cam.SetActive(false);
        clip = new IntroLoop(music, 0f, 0f, 22.5f);
        gm = FindObjectOfType<GameManager>();
    }

    IEnumerator SceneManager()
    {
        yield return new WaitForSeconds(0f);
        cam.SetActive(true);
        FindObjectOfType<FinalZerg>().clip.stop();
        trans.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        if(playMusic)
        {
            clip.start();
            playMusic = false;
        }
        //flip player
        Vector3 theScale = tonchi.transform.localScale;
		theScale.x = Mathf.Abs(theScale.x);
		tonchi.transform.localScale = theScale;

        yield return new WaitForSeconds(1f);
        for(int i = 0; i <= tonchiUI.Length - 1; i++)
            tonchiUI[i].SetActive(false);
        FindObjectOfType<PlayerMovement>().isDashActive = false;
        FindObjectOfType<PlayerMovement>().isDJumpActive = false;
        tonchi.transform.position = scene1.transform.position;
        FindObjectOfType<Health>().health = 9;

        //scene1
        yield return new WaitForSeconds(1.5f);
        trans.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        for(int i = 0; i <= zergs1.Length - 1; i++)
            zergs1[i].GetComponent<Zerg>().TakeDamage(200, 0, 0);

        for(int i = 0; i <= zergflyers1.Length - 1; i++)
            zergflyers1[i].GetComponent<ZergFly>().TakeDamage(200);

        yield return new WaitForSeconds(1f);
        trans.SetTrigger("Start");

        //scene2
        yield return new WaitForSeconds(1.5f);

        tonchi.transform.position = scene2.transform.position;

        yield return new WaitForSeconds(2f);
        trans.SetTrigger("End");

        yield return new WaitForSeconds(1f);
        for(int i = 0; i <= zergs2.Length - 1; i++)
            zergs2[i].GetComponent<Zerg>().TakeDamage(200, 0, 0);

        for(int i = 0; i <= zergflyers2.Length - 1; i++)
            zergflyers2[i].GetComponent<ZergFly>().TakeDamage(200);

        yield return new WaitForSeconds(1.5f);
        trans.SetTrigger("Start");

        //scene3
        yield return new WaitForSeconds(1.5f);
        tonchi.transform.position = scene3.transform.position;
        for(int i = 0; i <= tonchiUI.Length - 1; i++)
            tonchiUI[i].SetActive(true);
        FindObjectOfType<PlayerMovement>().isDashActive = true;
        FindObjectOfType<PlayerMovement>().isDJumpActive = true;

        yield return new WaitForSeconds(2.5f);
        trans.SetTrigger("End");

        yield return new WaitForSeconds(4.5f);
        bTonchi.GetComponent<ZergFlyerRotate>().enabled = true;
    }

    IEnumerator Credits()
    {
        trans.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        credits.SetTrigger("Scroll");
        yield return new WaitForSeconds(28f);
        gm.Menu();
    }

    void Update()
    {
        clip.checkTime();
        if(finalzerg.GetComponent<FinalZerg>().isDead && end)
        {
            StartCoroutine("SceneManager");
            end = false;
        }
        if(!dialogue.enabled && end2)
        {
            StartCoroutine("Credits");
            end2 = false;
        }

        //keep player still
        if(finalzerg.GetComponent<FinalZerg>().isDead)
        {
            tonchi.GetComponent<Animator>().SetFloat("Speed", 0f);
            tonchi.GetComponent<Animator>().SetBool("IsCrouching", false);
        }
    }
}
