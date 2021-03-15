using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yesil_TakipEt : YesilSkillBaseClass
{
    public Transform kureTakipNoktasi;
    private bool kureTakipeGeliyor,kureGeldi;
    public GameObject kure;
    public float moveSpeed;
    private PlayerRangeAttack playerRangAttck;
    public static bool kureTakipEdiyor;
    // Update is called once per frame
    void Update()
    {
        if (PlayerRangeAttack.instance.kureTamamlandi)
        {
            kureTakipNoktasi.gameObject.SetActive(false);
            kureTakipeGeliyor = false;
            kureGeldi = false;
            Hook.instance.kureyeHookAtabilir = true;
        }

    }

    private void FixedUpdate()
    {
        if (kureTakipeGeliyor)
        {
            kureRigid.MovePosition(Vector2.MoveTowards(kure.transform.position, kureTakipNoktasi.position, moveSpeed * Time.deltaTime));
            if (Vector2.Distance(kure.transform.position, kureTakipNoktasi.position) <= 0.5f)
            {
                playerRangAttck.kureHareketYapamaz = false;
                kureTakipeGeliyor = false;
                kureGeldi = true;
                
            }
        }

        if (kureGeldi && !playerRangAttck.kureDonuyor)
        {
            kureRigid.MovePosition(Vector2.Lerp(kure.transform.position, kureTakipNoktasi.position, moveSpeed/2 * Time.deltaTime));
            
        }

        
    }
    public override void Use()
    {
        if (!kureGeldi && !Hook.instance.altBasildi)
        {
            playerRangAttck = PlayerRangeAttack.instance;
            playerRangAttck.kureTakipEdiyor = true;
            Hook.instance.kureyeHookAtabilir = false;
            kureTakipNoktasi.gameObject.SetActive(true);
            playerRangAttck.kureHareketYapamaz = true;
            kureTakipeGeliyor = true;
        }
        else if(kureGeldi && !Hook.instance.altBasildi && !playerRangAttck.kureGidiyor && !playerRangAttck.kureHareketYapamaz)
        {
            playerRangAttck.GitmeZamaniSifirla();
            Hook.instance.kureyeHookAtabilir = true;
            kureGeldi = false;
            playerRangAttck.kureGidiyor = true;
            //playerRangAttck.ruhKuresiParcasi.transform.position = gameObject.transform.position;
            playerRangAttck.kureSpriteRenderer.enabled = true;
            playerRangAttck.kureClass.currentRuhKuresi.MesafeHesapla();
        }
    }
}
