using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Animator animator = null; 
    [SerializeField] private GameObject[] objects = null;
    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private Transform spawnPoint2 = null;
    [SerializeField] private Transform spawnPoint3 = null;
    [SerializeField] private Collider2D top = null;
    private bool open = false;
    private int currHealth;

    void Start()
    {
        currHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        if(open == false && currHealth <= 0)
        {
            Open();
            open = true;
        }
    }

    void Open()
    {
        FindObjectOfType<AudioManager>().Play("DoorOpen");
        animator.SetBool("Open", true);
        this.enabled = false;
        top.GetComponent<Collider2D>().enabled = false;
        GameObject heal = Instantiate(objects[objects.Length - 1], spawnPoint.position, spawnPoint.rotation) as GameObject;
        GameObject heal2 = Instantiate(objects[objects.Length - 2], spawnPoint2.position, spawnPoint2.rotation) as GameObject;
        if(objects.Length == 3)
        {
            GameObject heal3 = Instantiate(objects[objects.Length - 3], spawnPoint3.position, spawnPoint3.rotation) as GameObject;
        }
    }
}
