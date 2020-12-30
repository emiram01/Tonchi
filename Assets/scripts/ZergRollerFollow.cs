using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZergRollerFollow : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float lookRadius = 0f;
    [SerializeField] private bool isBoss = false;
    [SerializeField] private Transform boss = null;
    private Transform target = null;
    private Vector2 spawn;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if(isBoss)
        {
            transform.position = boss.position;
        }
        spawn = this.transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius)
        {
             SeekTarget();
        }
    }

    private void SeekTarget()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
    }

    public void Dead()
    {
        this.transform.position = spawn;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
