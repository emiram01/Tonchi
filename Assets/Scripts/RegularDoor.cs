using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularDoor : MonoBehaviour
{
    [SerializeField] private Transform trans = null;
    [SerializeField] private Transform transB = null;
    [SerializeField] private Transform player = null;
    [SerializeField] private float posy = 0f;
    [SerializeField] private float speed = 0f;
    private Vector3 posA;
    private Vector3 posB;
    public bool canCloseL = false;
    public bool canCloseR = false;
    public bool moveBack = false;
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
        if(player.position.x < this.transform.position.x && moveBack == false && player.position.y > posy && canCloseL)
            Move();
        else if(moveBack == true)
            MoveBack();

        if(canCloseR)
            Move();
    }
}
