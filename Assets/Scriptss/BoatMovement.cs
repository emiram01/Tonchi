using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private Vector3 velocity = default;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private bool boat1 = true;
    [SerializeField] private bool boat2 = true;
    private Vector2 startPos;
    private bool moving;

    void Start()
    {
        startPos = this.transform.position;
    }

    public void Dead()
    {
        this.transform.position = startPos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && rb.position.x > 195f && boat1)
        {
            moving = true;
            collision.collider.transform.SetParent(transform);
        }

        if (collision.gameObject.tag == "Player" && rb.position.x < -565f && boat2)
        {
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

    void Update()
    {
        if (moving)
            transform.position += (velocity * Time.deltaTime);

        if(rb.position.x < 195f && boat1)
            moving = false;

        if(rb.position.x > -565f && boat2)
            moving = false;
    }
}
