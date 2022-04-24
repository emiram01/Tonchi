using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [SerializeField] private float speed = -5f;
    private Vector2 startPos;
    private bool idle = false;

    void Start()
    {
        startPos = transform.position;
    }

    public void IsIdle()
    {
        idle = true;
    }

    void Update()
    {
        if(idle == true)
        {
            float newPos = Mathf.Repeat(Time.time * speed, 50);
            transform.position = startPos + Vector2.right * newPos;
            idle = false;
        }

    }
}
