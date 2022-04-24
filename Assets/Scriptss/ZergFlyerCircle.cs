using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZergFlyerCircle : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private Transform target = null;
     
    private Vector3 zAxis = new Vector3(0, 0, -1);
 
    void FixedUpdate () 
    {
         transform.RotateAround(target.position, zAxis, speed); 
    }
}
