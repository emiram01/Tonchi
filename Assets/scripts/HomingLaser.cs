using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingLaser : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private int damage = 1;
    public float speed = 5;
    private float curr = 0;
    private float nextDam = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 rotation = transform.eulerAngles;
        if(player.transform.position.x < this.transform.position.x)
        {
            rotation.y = 0f;
        }
        else
        {
            rotation.y = 180f;
        }
        transform.eulerAngles = rotation;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        transform.Rotate (0,0,500*Time.deltaTime);
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
            Destroy(this.gameObject);
            ScreenShake.Instance.Shake(1.5f, .2f);
        }
    }
}
