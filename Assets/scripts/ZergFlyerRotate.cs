using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZergFlyerRotate : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    // private float someScale;

    // void Start () 
    //  {
    //      someScale = transform.localScale.x;
    //  }
     
     void FixedUpdate () 
     {
         Flip();
     }

    void Flip()
	{
        Vector3 rotation = transform.eulerAngles;
        if(target.transform.position.x > this.transform.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }
        transform.eulerAngles = rotation;
	}
}
