using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCredits : MonoBehaviour
{
    [SerializeField] private Animator trans = null;
    [SerializeField] private Animator credits = null;
    [SerializeField] private Collider2D stopper1 = null;
    private GameManager gm;
    private bool end  = true;
    
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    IEnumerator actuallyDone()
    {
        trans.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<PlayerMovement>().canMove = false;
        FindObjectOfType<PlayerCombat>().canAtk = false;
        credits.SetTrigger("Scroll");
        yield return new WaitForSeconds(21.5f);
        gm.Menu();
    }

    void Update()
    {
        if(stopper1.enabled == false && end)
        {
            StartCoroutine("actuallyDone");
            end = false;
        }
    }
}
