using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.AddForce(Physics.gravity * rb.mass);
        }
    }
}
