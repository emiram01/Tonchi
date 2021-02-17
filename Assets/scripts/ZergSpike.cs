using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZergSpike : MonoBehaviour
{
    [SerializeField] private int spikeDamage = 1;
    [SerializeField] private Animator animator = null;
    [SerializeField] private Collider2D coll = null;
    private GameObject target;
    private Vector2 seek;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        coll.enabled = false;
        StartCoroutine("Attack");
        // if(target != null)
        // {
        //     SeekTarget();
        // }
    }

    // private void SeekTarget()
    // {
    //     this.transform.position = target.transform.position;
    // }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("Attack", true);
        coll.enabled = true;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damage();
        }
    }

    void Damage()
    {
        FindObjectOfType<Health>().SubHealth(spikeDamage);
        FindObjectOfType<AudioManager>().Play("Slice");
    }
}
