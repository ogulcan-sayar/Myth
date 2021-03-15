using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yesil_IsikArttir : YesilSkillBaseClass
{
    public float targ_outerRadius;
    public float lightUpSpeed;
    private bool isigiArttir, isikArtti, isigiDusur;
    public static bool isikArttirilabilir;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isigiArttir)
        {
            kureIsigi.pointLightOuterRadius += lightUpSpeed * Time.deltaTime;
            if(kureIsigi.pointLightOuterRadius > targ_outerRadius)
            {
                kureIsigi.pointLightOuterRadius = targ_outerRadius;
                isigiArttir = false;
                isikArtti = true;
            }
        }
        else if (isigiDusur)
        {
            kureIsigi.pointLightOuterRadius -= lightUpSpeed * Time.deltaTime;
            if (kureIsigi.pointLightOuterRadius < def_outerRadius)
            {
                kureIsigi.pointLightOuterRadius = def_outerRadius;
                isigiDusur = false;
                isikArtti = false;
            }
        }

        if (PlayerRangeAttack.instance.kureTamamlandi && (isikArtti || isigiDusur || isigiArttir))
        {
            isikArtti = false;
            isigiArttir = false;
            isigiDusur = false;
            kureIsigi.pointLightOuterRadius = def_outerRadius;
        }
    }

    public override void Use()
    {
        if (!Yesil_IsikPatlat.isikPatlamasi)
        {
            if (!isikArtti)
            {
                isigiArttir = true;
            }
            else
            {
                isigiDusur = true;
            }
        }
    }
}
