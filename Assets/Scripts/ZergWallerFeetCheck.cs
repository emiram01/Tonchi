using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZergWallerFeetCheck : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private Rigidbody2D rb = null;

    void OnCollisionExit2D(Collision2D collision)
    {  
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("IsDead", true);
            Invoke("Die", .5f);
        }
    }

    void Die()
    {
        rb.isKinematic = false;
        Destroy(gameObject, 5f);
    }
}
