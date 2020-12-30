using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZergActually3 : MonoBehaviour
{
    [SerializeField] private Collider2D stopper1 = null;
    [SerializeField] private GameObject zerg = null;
    [SerializeField] private GameObject boat = null;
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector3 nextPos = default;
    private bool end = true;
    private bool move = false;

    IEnumerator done()
    {
        Vector3 rotation = zerg.transform.eulerAngles;
        yield return new WaitForSeconds(.7f);
        rotation.y = 180f;
        zerg.transform.eulerAngles = rotation;
        move = true;
    }

    void FixedUpdate()
    {
        if(move)
        {
            boat.transform.position = Vector3.MoveTowards(boat.transform.position, nextPos, speed * Time.deltaTime);
        }
    }

    void Update()
    {
        if(stopper1.enabled == false && end)
        {
            StartCoroutine("done");
            end = false;
        }
    }
}
