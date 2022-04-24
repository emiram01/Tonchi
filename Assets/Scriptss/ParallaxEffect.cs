using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private GameObject cam = null;
    [SerializeField] private float effect = 0f;
    private float len;
    private float startPos;

    void Start()
    {
        startPos = transform.position.x;
        len = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - effect));
        float dist = (cam.transform.position.x * effect);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        if(temp > startPos + len)
        {
            startPos += len;
        } 
        else if(temp < startPos - len)
        {
            startPos -= len;
        }
    }
}
