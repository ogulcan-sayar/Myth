using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirmizi : CurrentRuhKuresiClass
{
    [Header("Slots")]
    public KirmiziSkillBaseClass e_SkillSlot;
    public KirmiziSkillBaseClass q_SkillSlot;
    [Header("Other")]
    public float attackRange;
    public LayerMask enemyLayers;
    public float slowDown;
    private float currentThrowSpeed;
    


    public override void eSkill()
    {
        e_SkillSlot.Use();
    }

    public override void qSkill()
    {
        q_SkillSlot.Use();
    }


    public override void TopuAtma()
    {
        ruhKuresiRigid.velocity = attackDirection.normalized * currentThrowSpeed * Time.deltaTime;
        HasarVer();
    }
    public override void MesafeHesapla()
    {
        attackDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        currentThrowSpeed = throwSpeed;
    }

    public override void KureIleOyuncuArasindakiMesafe()
    {
        kureIleOyuncuArasindakiMesafe = Vector2.Distance(transform.position, player.transform.position);
        KureBagiController.instance.ChangeColor(kureIleOyuncuArasindakiMesafe, this.bagDırencMesafesi);
        if(kureIleOyuncuArasindakiMesafe > this.bagDırencMesafesi)
        {
            Bevzier.instance.coroutineAllowed = true;
            PlayerRangeAttack.instance.kureDuruyor = false;
            PlayerRangeAttack.instance.kureDonuyor = true;
            PlayerRangeAttack.instance.kureGidiyor = false;
        }
    }

    public override void TopuGeriDondur()
    {
        Bevzier.instance.startBevzier();
        if (Bevzier.instance.bevzierBitti == true)
        {
            PlayerRangeAttack.instance.kureSpriteRenderer.enabled = false;
            PlayerRangeAttack.instance.kureTamamlandi = true;
            PlayerRangeAttack.instance.kureDonuyor = false;
            PlayerRangeAttack.instance.kureGidiyor = false;
            PlayerRangeAttack.instance.kureDuruyor = false;
            Bevzier.instance.bevzierBitti = false;
        }

    }

    public void HasarVer()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(gameObject.transform.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyTest>().TakeOrbDamage(10, ref currentThrowSpeed, slowDown);
           // ruhKuresiRigid.velocity -= -attackDirection.normalized * slowDown * Time.deltaTime;
            //CameraEffects.instance.CameraShake(attackConfig.shakeAmount, attackConfig.shakeLenght);
        }
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
    }
}

