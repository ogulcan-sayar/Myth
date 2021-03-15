using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Yesil : CurrentRuhKuresiClass
{
    [Header("Slots")]
    public YesilSkillBaseClass e_SkillSlot;
    public YesilSkillBaseClass q_SkillSlot;
    public Light2D kureIsigi;
    public float def_innerRadius, def_outerRadius, def_intensity;
    [HideInInspector] public float cur_innerRadius, cur_outerRadius, cur_intensity;
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
        ruhKuresiRigid.velocity = attackDirection.normalized * throwSpeed * Time.deltaTime;
        IsikAcKapa(true);
    }
    public override void MesafeHesapla()
    {
        attackDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    public override void KureIleOyuncuArasindakiMesafe()
    {
        kureIleOyuncuArasindakiMesafe = Vector2.Distance(transform.position, player.transform.position);
        KureBagiController.instance.ChangeColor(kureIleOyuncuArasindakiMesafe, this.bagDırencMesafesi);
        if (kureIleOyuncuArasindakiMesafe > this.bagDırencMesafesi)
        {
            
            Bevzier.instance.coroutineAllowed = true;
            PlayerRangeAttack.instance.kureDuruyor = false;
            PlayerRangeAttack.instance.kureDonuyor = true;
            PlayerRangeAttack.instance.kureGidiyor = false;
            PlayerRangeAttack.instance.GitmeZamaniSifirla();
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
            IsikAcKapa(false);
            ResetLight();
        }

    }

    public void IsikAcKapa(bool ac)
    {
        kureIsigi.enabled = ac;
    }

    public void ResetLight()
    {
      kureIsigi.pointLightInnerRadius = def_innerRadius;
      kureIsigi.pointLightOuterRadius =  def_outerRadius;
      kureIsigi.intensity =  def_intensity;
    }
}
