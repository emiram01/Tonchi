using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrail : MonoBehaviour
{
    [SerializeField] private float startSpawnTime = 2;
    [SerializeField] private float destroy = 0f;
    [SerializeField] private GameObject trail = null;
    [SerializeField] private Animator anim = null;
    private float spawnTime = 0;

    void Update()
    {
        if(anim.GetBool("IsDashing") == true)
        {
            if(spawnTime <= 0)
            {
                GameObject instance = (GameObject) Instantiate(trail, transform.position, Quaternion.identity);
                Destroy(instance, destroy);
                spawnTime = startSpawnTime;
            }
            else
            {
                spawnTime -= Time.deltaTime;
            }
        }

    }
}
