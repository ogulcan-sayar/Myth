using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mavi : CurrentRuhKuresiClass
{

    public override void eSkill()
    {

    }

    public override void qSkill()
    {

    }

    public override void TopuAtma()
    {
        ruhKuresiRigid.velocity = attackDirection.normalized * throwSpeed * Time.deltaTime;
    }
    public override void MesafeHesapla()
    {
        attackDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }


}
