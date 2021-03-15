using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirmizi_Patlama : KirmiziSkillBaseClass
{
    
    private float range =1f;
    [SerializeField]
    protected GameObject kure;

    public override void Use()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kure.transform.position, range, enemy);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyTest>().TakeDamage(10);
            // ruhKuresiRigid.velocity -= -attackDirection.normalized * slowDown * Time.deltaTime;
            CameraEffects.instance.CameraShake(0.05f, 0.1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        
    }
}
