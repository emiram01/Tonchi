using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDoor : MonoBehaviour
{
    [SerializeField] private Transform trans = null;
    [SerializeField] private Transform transB = null;
    [SerializeField] private Transform player = null;
    [SerializeField] private float speed = 0f;
    private bool moveBack = false; 
    private Vector3 posA;
    private Vector3 posB;
    private bool close = true;

    void Start()
    {
        posA = trans.localPosition;
        posB = transB.localPosition;
    }
    
    void Move()
    {
        if(close)
        {
            FindObjectOfType<AudioManager>().Play("DoorClose");
            close = false;
        }
        trans.localPosition = Vector3.MoveTowards(trans.localPosition, posB, speed * Time.deltaTime);
    }

    public void MoveBack()
    {
        moveBack = true;
        trans.localPosition = Vector3.MoveTowards(trans.localPosition, posA, speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if(player.position.x > this.transform.position.x && moveBack == false)
        {
            Move();
        }
        else if(moveBack == true)
        {
            MoveBack();
        }
    }
}
