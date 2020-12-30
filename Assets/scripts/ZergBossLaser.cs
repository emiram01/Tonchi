using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZergBossLaser : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private int damage = 1;
    [SerializeField] private Rigidbody2D rb = null;
    private GameObject target;
    private float curr = 0;
    private float nextDam = 1;
    private Vector2 seek;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if(target != null)
        {
             SeekTarget();
        }
    }

    void FixedUpdate()
    {
        transform.Rotate (0,0,500*Time.deltaTime);
    }

    private void SeekTarget()
    {
        seek = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(seek.x, seek.y);
        Vector3 rotation = transform.eulerAngles;
        Vector2 dir = target.transform.position - transform.position;
        if(target.transform.position.x < this.transform.position.x)
        {
            rotation.y = 0f;
        }
        else
        {
            rotation.y = 180f;
        }
        transform.eulerAngles = rotation;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
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
        if (col.CompareTag("Ground"))
        {
            //ScreenShake.Instance.Shake(1f, .2f);
            Destroy(this.gameObject);
        }
    }
}
