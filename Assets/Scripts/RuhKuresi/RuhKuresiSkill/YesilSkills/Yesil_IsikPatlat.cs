using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yesil_IsikPatlat : YesilSkillBaseClass
{
    private float curr_intensity;
    public float hafifArttirilmis_Intensity, patlama_Intensity;
    public float patlamaSpeed,isikArttirmaSpeed,isikAzalmaSpeed,range;
    private bool patlat, isikArtti, isikDustu,eskiHalineGetir,patladi;
    public static bool isikPatlamasi;
    public int explosionDamage;

    private void Update()
    {
        if (patlat && PlayerRangeAttack.instance.kureDuruyor && !isikArtti)
        {
            kureIsigi.intensity += isikArttirmaSpeed * Time.deltaTime;
            if (kureIsigi.intensity > hafifArttirilmis_Intensity)
            {
                kureIsigi.intensity = hafifArttirilmis_Intensity;
                isikArtti = true;
            }

        }

        if(patlat && isikArtti && PlayerRangeAttack.instance.kureDuruyor && !isikDustu)
        {
            kureIsigi.intensity -= isikAzalmaSpeed * Time.deltaTime;
            if (kureIsigi.intensity < 0)
            {
                kureIsigi.intensity = 0;

                isikDustu = true;
            }
        }

        if(patlat && isikDustu && PlayerRangeAttack.instance.kureDuruyor&& !patladi)
        {
            kureIsigi.intensity += patlamaSpeed * Time.deltaTime;
            if (kureIsigi.intensity > patlama_Intensity)
            {
                kureIsigi.intensity = patlama_Intensity;
                patladi = true;
                range = kureIsigi.pointLightOuterRadius / 3.75f;
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kureRigid.gameObject.transform.position, range, enemy);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<EnemyTest>().TakeDamage(explosionDamage);
                    CameraEffects.instance.CameraShake(0.2f, 0.1f);
                    Debug.Log("we hit" + enemy.name);
                }
                eskiHalineGetir = true;
            }
        }

        if (eskiHalineGetir && patladi)
        {
            kureIsigi.intensity -= isikAzalmaSpeed * Time.deltaTime;
            if (kureIsigi.intensity < curr_intensity)
            {
                patladi = false;
                kureIsigi.intensity = curr_intensity;
                eskiHalineGetir = false;
                patlat = false;
                isikArtti = false;
                isikDustu = false;
                PlayerRangeAttack.instance.kureHareketYapamaz = false;
                isikPatlamasi = false;
            }
        }
    }

    public override void Use()
    {
        if (!patlat && PlayerRangeAttack.instance.kureDuruyor && !PlayerRangeAttack.instance.kureTakipEdiyor)
        {
            PlayerRangeAttack.instance.kureHareketYapamaz = true;
            patlat = true;
            isikPatlamasi = true;
            curr_intensity = kureIsigi.intensity;
        }  
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(kureRigid.gameObject.transform.position, range);
    }
}
