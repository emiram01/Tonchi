using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private Transform attackPoint = null;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 40;
    [SerializeField] private LayerMask enemyLayers = 0;
    [SerializeField] private LayerMask enemyFlyLayers = 0;
    [SerializeField] private LayerMask enemyRollLayers = 0;
    [SerializeField] private LayerMask chestLayers = 0;
    [SerializeField] private LayerMask bossLayers = 0;
    [SerializeField] private LayerMask bossLayers2 = 0;
    [SerializeField] private LayerMask bossLayers3 = 0;
    [SerializeField] private LayerMask final = 0;
    [SerializeField] private LayerMask finalZerg = 0;
    public bool canAtk = true;
    private bool isAttacking = false;
    private int attackCounter = 0;
    private float lastAttack = 0f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canAtk)
        {
            AttackPressed();
        }
    }

    public void AttackPressed() 
    {
	    if ((lastAttack - Time.time) > 0.5f || attackCounter == 0) 
        {
            FindObjectOfType<AudioManager>().Play("LHook");
		    Attack(1);
		    attackCounter = 1;
	    } 
        else if (attackCounter == 1) 
        {
            FindObjectOfType<AudioManager>().Play("RHook");
		    Attack(2);
		    attackCounter = 2;
	    } 
        else if (attackCounter == 2) 
        {
            FindObjectOfType<AudioManager>().Play("Kick");
		    Attack(3);
		    attackCounter = 3;
	    }
        else if (attackCounter == 3) 
        {
            FindObjectOfType<AudioManager>().Play("UpperCut");
		    Attack(4);
		    attackCounter = 0;
	    }
	    lastAttack = Time.deltaTime;
    }

    //This is probably not a good way to do attacks, will change method for future projects
    void Attack(int index)
    {
        isAttacking = true;
        if(isAttacking == true)
        {
            animator.SetTrigger("Attack" + index);
            Invoke("ResetAttack", .2f);
            animator.SetBool("IsJumping", false);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.gameObject.GetComponent<Zerg>().TakeDamage(attackDamage, 10, 5);
            }
            Collider2D[] hitFlyEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyFlyLayers);
            foreach(Collider2D enemy in hitFlyEnemies)
            {
                enemy.gameObject.GetComponent<ZergFly>().TakeDamage(attackDamage);
            }
            Collider2D[] hitRollerEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyRollLayers);
            foreach(Collider2D enemy in hitRollerEnemies)
            {
                enemy.gameObject.GetComponent<ZergRollerDam>().TakeDamage(attackDamage);
            }
            Collider2D[] hitZergBoss = Physics2D.OverlapCircleAll(this.transform.position, .7f, bossLayers);
            foreach(Collider2D enemy in hitZergBoss)
            {
                enemy.gameObject.GetComponent<ZergBoss>().TakeDamage(attackDamage / 2);
            }
            Collider2D[] hitZergBoss2 = Physics2D.OverlapCircleAll(this.transform.position, .7f, bossLayers2);
            foreach(Collider2D enemy in hitZergBoss2)
            {
                enemy.gameObject.GetComponent<ZergRollerBoss>().TakeDamage(attackDamage );
            }
            Collider2D[] hitZergBoss3 = Physics2D.OverlapCircleAll(this.transform.position, .7f, bossLayers3);
            foreach(Collider2D enemy in hitZergBoss3)
            {
                enemy.gameObject.GetComponent<ZergFlyerBoss>().TakeDamage(attackDamage);
            }
            Collider2D[] hitFinal = Physics2D.OverlapCircleAll(this.transform.position, .7f, final);
            foreach(Collider2D enemy in hitFinal)
            {
                enemy.gameObject.GetComponent<ShipControls>().hit = true;
            }
            Collider2D[] hitChest = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, chestLayers);
            foreach(Collider2D chest in hitChest)
            {
                chest.gameObject.GetComponent<ChestOpen>().TakeDamage(attackDamage);
            }
            Collider2D[] hitFinalZerg = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, finalZerg);
            foreach(Collider2D enemy in hitFinalZerg)
            {
                enemy.gameObject.GetComponent<FinalZerg>().TakeDamage(attackDamage);
            }
        }
        return;
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }    
}
