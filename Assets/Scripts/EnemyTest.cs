using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public int maxHealth =100;
    int currentHealth;
    public float invisibleTime;
    float timer;
    bool invisible;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (invisible)
        {
            timer += Time.deltaTime;
            if(timer >= invisibleTime)
            {
                timer = 0;
                invisible = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        Debug.Log(currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeOrbDamage(int damage, ref float currentSpeed ,float slowDownn)
    {
        if (!invisible)
        {
            currentSpeed = currentSpeed * slowDownn;
            //kureRigid.AddForce(-direction.normalized * slowDownn,ForceMode2D.Force);
            invisible =true;
            TakeDamage(damage);
        }
    }

    void Die()
    {
        animator.SetTrigger("Death");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.enabled = false;
        //Destroy(gameObject);
    }
}
