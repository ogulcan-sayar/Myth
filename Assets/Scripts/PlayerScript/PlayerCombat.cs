using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator playerAnimator;
    private Collider2D charCol;
    [Header("STATS")]
    public int damagePower;

    [Header("MeleeAttack")]
    public Transform attackPoint;
    public float attackRange;


    [Header("AirAttack")]
    public Transform airAttackPoint;
    public Vector2 damageArea;

    [Header("OTHERS")]
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    float nextAttackTime = 0;
    
    [HideInInspector] public bool attacking;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        charCol = GetComponent<Collider2D>();
 
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime )
        {
            if (Input.GetMouseButtonDown(0) && attacking == false && !CharacterControl.instance.dodge && !playerAnimator.GetBool("AirAttackInput"))
            {
                attacking = true;
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                
            }
        }

       

    }

    void Attack()
    {
        playerAnimator.SetTrigger("Attack");     
    }

    public void CheckEnemies(AttackConfig attackConfig)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyTest>().TakeDamage(damagePower+ attackConfig.extraDamage);
            CameraEffects.instance.CameraShake(attackConfig.shakeAmount, attackConfig.shakeLenght);
            Debug.Log("we hit" + enemy.name);
        }
        
    }

    public void AirCheckEnemies(AttackConfig attackConfig)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(airAttackPoint.position, damageArea, 0f, enemyLayers);
        Vector2 dir;
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyTest>().TakeDamage(damagePower + attackConfig.extraDamage);
            dir = new Vector2(enemy.gameObject.transform.position.x - transform.position.x,2f);
            enemy.GetComponent<Rigidbody2D>().AddForce(dir * 100);
            CameraEffects.instance.CameraShake(attackConfig.shakeAmount, attackConfig.shakeLenght);
            Debug.Log("we hit" + enemy.name);
        }
        CameraEffects.instance.CameraShake(attackConfig.shakeAmount/4, attackConfig.shakeLenght);
    }

    /*public void AirAttackFallingDetection(int ctrl)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(charCol.bounds.center, charCol.bounds.size, 0f, enemyLayers);

        if(ctrl == 1)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.isTrigger = true;
            }
        }
        else if(ctrl == 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.isTrigger = false;
            }
        }
        *
    }*/

   /* private void Damage(float[] attackDetails)
    {
        if (!PD.GetDashStatus())
        {
            int direction;
            if (attackDetails[1] < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            PC.Knockback(direction);
        }
        
    }*/




    private void OnDrawGizmosSelected()
    {
       Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireCube(airAttackPoint.position, damageArea);
    }

    
}
