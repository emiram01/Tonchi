using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatMovement : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Transform trans = null;
    [SerializeField] private Transform transB = null;
    [SerializeField] private float speed = 1f;
    //[SerializeField] private float height = 1f;
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;
    private bool moving;

    void Start()
    {
        posA = transform.localPosition;
        posB = transB.localPosition;
        nextPos = posB;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            //moving = true;
            collision.collider.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            collision.collider.transform.SetParent(null);
            //moving = false;
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
        //if(moving)
        //{
            Move();
        //}
        // if(rb.position.y < height)
        // {
        //     rb.isKinematic = false;
        //     Invoke("Delete", 2f);
        // }
    }

    // void Delete()
    // {
    //     Destroy(gameObject);
    // }
}
