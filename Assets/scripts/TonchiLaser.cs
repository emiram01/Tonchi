using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonchiLaser : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    [SerializeField] private int damage = 20;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float destroyTime = 0f;
    [SerializeField] private float range = .1f;
    [SerializeField] private Transform player = null;
    [SerializeField] private LayerMask enemyLayers = 0;
    [SerializeField] private LayerMask enemyFlyLayers = 0;
    [SerializeField] private LayerMask enemyRollLayers = 0;
    [SerializeField] private LayerMask chestLayers = 0;
    [SerializeField] private LayerMask bossLayers = 0;
    [SerializeField] private LayerMask bossLayers2 = 0;
    [SerializeField] private LayerMask bossLayers3 = 0;
    //private float curr = 0;
    //private float nextDam = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("DestroyLaser", destroyTime);
        Flip();
    }

    void DestroyLaser()
    {
        Destroy(this.gameObject);
    }

    void Flip()
	{
        Vector3 rotation = transform.eulerAngles;
        if(player.transform.position.x > this.transform.position.x)
        {
            rotation.y = 0f;
            rb.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rotation.y = 180f;
            rb.velocity = new Vector2(speed, 0);
        }
        transform.eulerAngles = rotation;
	}

    void Update()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(this.transform.position, range, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.gameObject.GetComponent<Zerg>().TakeDamage(damage, 5, 3);
            //ScreenShake.Instance.Shake(1f, .2f);
            DestroyLaser();
        }
        Collider2D[] hitFlyEnemies = Physics2D.OverlapCircleAll(this.transform.position, range, enemyFlyLayers);
        foreach(Collider2D enemy in hitFlyEnemies)
        {
            enemy.gameObject.GetComponent<ZergFly>().TakeDamage(damage);
            //ScreenShake.Instance.Shake(1f, .2f);
            DestroyLaser();
        }
        Collider2D[] hitRollerEnemies = Physics2D.OverlapCircleAll(this.transform.position, range, enemyRollLayers);
        foreach(Collider2D enemy in hitRollerEnemies)
        {
            enemy.gameObject.GetComponent<ZergRollerDam>().TakeDamage(damage);
            //ScreenShake.Instance.Shake(1f, .2f);
            DestroyLaser();
        }
        Collider2D[] hitZergBoss = Physics2D.OverlapCircleAll(this.transform.position, range, bossLayers);
        foreach(Collider2D enemy in hitZergBoss)
        {
            enemy.gameObject.GetComponent<ZergBoss>().TakeDamage(damage);
            //ScreenShake.Instance.Shake(1f, .2f);
            DestroyLaser();
        }
        Collider2D[] hitZergBoss2 = Physics2D.OverlapCircleAll(this.transform.position, range, bossLayers2);
        foreach(Collider2D enemy in hitZergBoss2)
        {
            enemy.gameObject.GetComponent<ZergRollerBoss>().TakeDamage(14);
            //ScreenShake.Instance.Shake(1f, .2f);
            DestroyLaser();
        }
        Collider2D[] hitZergBoss3 = Physics2D.OverlapCircleAll(this.transform.position, range, bossLayers3);
        foreach(Collider2D enemy in hitZergBoss3)
        {
            enemy.gameObject.GetComponent<ZergFlyerBoss>().TakeDamage(damage);
            //ScreenShake.Instance.Shake(1f, .2f);
            DestroyLaser();
        }
        Collider2D[] hitChest = Physics2D.OverlapCircleAll(this.transform.position, range, chestLayers);
        foreach(Collider2D chest in hitChest)
        {
            chest.gameObject.GetComponent<ChestOpen>().TakeDamage(damage);
            //ScreenShake.Instance.Shake(1f, .2f);
            DestroyLaser();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Ground"))
        {
            //ScreenShake.Instance.Shake(1f, .2f);
            DestroyLaser();
        }
    }
}
