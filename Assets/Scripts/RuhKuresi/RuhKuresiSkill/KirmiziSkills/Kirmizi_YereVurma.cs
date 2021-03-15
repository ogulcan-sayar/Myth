using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirmizi_YereVurma : KirmiziSkillBaseClass
{
    public GameObject kure;
    public CircleCollider2D circCol2d;
    public LayerMask ground;
    public float fallForce;
    public Vector2 damageArea;
    [HideInInspector] public bool kureAirAttack;
    private bool isGround,yapti;
    private int carptirmaHakki=1;
    private void FixedUpdate()
    {

        if (kureAirAttack)
        {
            kureRigid.AddForce(Vector2.down * fallForce);
            if (CheckGround())
            {
                
                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(kure.transform.position, damageArea, 0f, enemy);
                Vector2 dir;
                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<EnemyTest>().TakeDamage(20);
                    dir = new Vector2(enemy.gameObject.transform.position.x - kure.transform.position.x, 2f);
                    enemy.GetComponent<Rigidbody2D>().AddForce(dir * 100);
                    CameraEffects.instance.CameraShake(0.2f, 0.1f);
                    Debug.Log("we hit" + enemy.name);
                    
                }
                CameraEffects.instance.CameraShake(0.2f / 4, 0.1f);
                kureAirAttack = false;
                yapti = true;
                if (carptirmaHakki == 1)
                {
                    PlayerRangeAttack.instance.kureGidiyor = false;
                    PlayerRangeAttack.instance.kureDuruyor = true;
                    PlayerRangeAttack.instance.GitmeZamaniSifirla();
                }
                PlayerRangeAttack.instance.kureHareketYapamaz = false;
            }
        }

        if (PlayerRangeAttack.instance.kureTamamlandi)
        {
            carptirmaHakki = 1;
            yapti = false;
        }
        
    }

    public override void Use()
    {
        if (!PlayerRangeAttack.instance.kureDonuyor && carptirmaHakki > 0)
        {
            PlayerRangeAttack.instance.kureHareketYapamaz = true;
            carptirmaHakki--;
            if (carptirmaHakki > 0)
            {
                PlayerRangeAttack.instance.kureGidiyor = true;
                PlayerRangeAttack.instance.kureDuruyor = false;
                yapti = false;
            }
            else
            {
                PlayerRangeAttack.instance.GitmeZamaniSifirla();
                PlayerRangeAttack.instance.kureGidiyor = false;
                PlayerRangeAttack.instance.kureDuruyor = true;
            }
            if (!yapti)
            {
                kureAirAttack = true;
            }
        }
    }

    bool CheckGround()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(circCol2d.bounds.center, circCol2d.bounds.size, 0f, Vector2.down, .8f, ground);
        return raycast.collider != null;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(kure.transform.position, damageArea);
    }
}
