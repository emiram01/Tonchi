using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private Collider2D stopper = null;
    [SerializeField] private TextMeshProUGUI display = null;
    [SerializeField] private string[] sentences = null;
    [SerializeField] private float typingSpeed = 0f;
    [SerializeField] private GameObject space = null;
    [SerializeField] private PlayerMovement movement = null;
    [SerializeField] private GameObject face = null;
    //[SerializeField] private GameObject previous = null;
    [SerializeField] private bool canDestroy = true;
    private int index = 0;
    private bool doneTyping;
    private bool typing = true;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && typing)
        {
            typing = false;
            face.SetActive(true);
            StartCoroutine("Type");
        }
    }

    IEnumerator Type()
    {
        movement.canMove = false;
        FindObjectOfType<PlayerCombat>().canAtk = false;
        movement.animator.SetBool("IsJumping", false);
        movement.animator.SetFloat("Speed", 0f);
        movement.animator.SetBool("IsDashing", false);
        movement.animator.SetBool("IsShooting", false);
        movement.animator.SetBool("IsDJumping", false);
        foreach(char letter in sentences[index].ToCharArray())
        {
            display.text += letter;
            FindObjectOfType<AudioManager>().Play("Chat");
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitUntil(()=> doneTyping);
        doneTyping = false;
        space.SetActive(false);
        FindObjectOfType<PlayerCombat>().canAtk = true;
        movement.canMove = true;
        face.SetActive(false);
        //previous.SetActive(false);
        if(canDestroy)
        {
            stopper.enabled = false;
        }
        else
        {
            typing = true;
        }
    }

    void NextSentence()
    {
        space.SetActive(false);
        if(index < sentences.Length - 1)
        {
            index++;
            display.text = "";
            StartCoroutine("Type");
        }
        else
        {
            display.text = "";
            doneTyping = true;
            space.SetActive(false);
        }
    }

    void Update()
    {
        if(display.text == sentences[index])
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                NextSentence();
            }  
            space.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            space.SetActive(false);
        }  
    }
}
