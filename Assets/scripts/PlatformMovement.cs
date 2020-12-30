using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Transform trans = null;
    [SerializeField] private Transform transB = null;
    [SerializeField] private float speed = 1f;
    //[SerializeField] private float height = 1f;    
    //[SerializeField] private GameObject coll = null;
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;
    private bool moving;

    void Start()
    {
        posA = transform.localPosition;
        posB = transB.localPosition;
        nextPos = posB;
       //Physics.IgnoreCollision(this.GetComponent<Collider>(), coll.GetComponent<Collider>());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {  
        if (collision.gameObject.tag == "Player")
        {
            //Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), coll.GetComponent<Collider2D>());
            moving = true;
            collision.collider.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
            moving = false;
        }
    }

    void Move()
    {
        trans.localPosition = Vector3.MoveTowards(trans.localPosition, nextPos, speed * Time.deltaTime);
        if(Vector3.Distance(trans.localPosition, nextPos) <= 0.1)
        {
            MoveBack();
        }
    }

    void MoveBack()
    {
        nextPos = nextPos != posA ? posA : posB;
    }

    void FixedUpdate()
    {
        if(moving)
        {
            nextPos = posB;
            Move();
        }
        else if(!moving)
        {
            nextPos = posA;
            Move();
        }
        // if(rb.position.y < height)
        // {
        //     rb.isKinematic = false;
        //     Invoke("Delete", 3f);
        //     Invoke("Respawn",5f);      
        // }
    }

    // void Delete()
    // {
    //     GetComponent<Collider2D>().enabled = false;
    //     this.enabled = false;
    //     this.gameObject.SetActive(false);
    // }

    // void Respawn()
    // {
    //     this.transform.position = posA;
    //     GetComponent<Collider2D>().enabled = true;
    //     this.enabled = true;
    //     this.gameObject.SetActive(true);
    // }
}