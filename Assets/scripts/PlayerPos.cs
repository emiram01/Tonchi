using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerPos : MonoBehaviour
{
    [SerializeField] Animator anim = null; 
    [SerializeField] private Animator anim2 = null;
    private GameManager gm;
    private float RestartDelay = 3f;
    private bool dead = false;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void Reland()
    {
        transform.position = gm.lastCheck;
        if(FindObjectOfType<Health>().health > 1)
        {
            anim.SetTrigger("End");
        } 
    }

    public void Respawn()
    {
        if(dead == false)
        {
            dead = true;
            Debug.Log("GAME OVER");
            anim2.SetTrigger("Start");
            Invoke("SpawnIn", RestartDelay);
        }
    }

    void SpawnIn()
    {
        gm.lastCheck = gm.spawnPoint;
        gm.Menu();
    }
}
