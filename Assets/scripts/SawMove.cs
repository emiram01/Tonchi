using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMove : MonoBehaviour
{
    [SerializeField] private Transform trans = null;
    [SerializeField] private Transform transB = null;
    [SerializeField] private float speed = 0f;
    [SerializeField] private bool flip = true;
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;

    void Start()
    {
        posA = trans.localPosition;
        posB = transB.localPosition;
        nextPos = posB;
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

    void Flip()
	{
        Vector3 rotation = trans.transform.eulerAngles;
        if(nextPos == posA)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }
        trans.transform.eulerAngles = rotation;
	}

    void FixedUpdate()
    {
        Move();
        if(flip == true)
        {
            Flip();
        }
    }
}
