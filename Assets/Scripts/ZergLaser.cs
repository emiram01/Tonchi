using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZergLaser : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    [SerializeField] private int damage = 1;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Transform player = null;
    [SerializeField] private float destroyTime = 0f;
    private float curr = 0;
    private float nextDam = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("DestroyLaser", destroyTime);
        Flip();
    }

    void DestroyLaser()
    {
        Destroy(this.gameObject);
    }

    void Flip()
	{
        Vector3 rotation = transform.eulerAngles;
        if(player.transform.position.x < this.transform.position.x)
        {
            rotation.y = 0f;
            rb.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rotation.y = 180f;
            rb.velocity = new Vector2(speed, 0);
        }
        transform.eulerAngles = rotation;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if(curr <= 0)
            {
                FindObjectOfType<Health>().SubHealth(damage);
                curr = nextDam;
                Destroy(this.gameObject);
            }
            else
            {
                curr -= 1;
            }
        }
        if(col.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
