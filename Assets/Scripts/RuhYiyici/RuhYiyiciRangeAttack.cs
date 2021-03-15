using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputExtender;
public class RuhYiyiciRangeAttack : MonoBehaviour
{
    [Header("RangeAttack")]
    public GameObject ruhKuresiParcasi;
    public SpriteRenderer kureSpriteRenderer;
    private Rigidbody2D ruhKuresiRigid;
    public float playerDetectedRange;
    [HideInInspector] public bool kureGidiyor, kureDuruyor, kureDonuyor, kureTamamlandi, kureHareketYapamaz;
    public CurrentRuhKuresiClass kureClass;
    private float timer;

    public Vector2 durduguPos;

    private Vector2 gizmosPos;

    public static RuhYiyiciRangeAttack instance;


    private void Start()
    {
        instance = this;
        ruhKuresiRigid = ruhKuresiParcasi.GetComponent<Rigidbody2D>();
        kureTamamlandi = true;
    }

    private void Update()
    {
        if (!kureTamamlandi)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                kureClass.currentRuhKuresi = GetComponent<Kirmizi>();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                kureClass.currentRuhKuresi = GetComponent<Yesil>();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                kureClass.currentRuhKuresi = GetComponent<Mavi>();
            }
        }
        if (CharacterControl.instance.ruhKuresiAtilabilir)
        {
            if (Input.GetMouseButtonUp(1) && kureTamamlandi && !kureGidiyor && !kureDuruyor && !kureHareketYapamaz)
            {
                kureTamamlandi = false;
                kureGidiyor = true;
                ruhKuresiParcasi.transform.position = gameObject.transform.position;
                kureSpriteRenderer.enabled = true;
                kureClass.currentRuhKuresi.MesafeHesapla();
            }
            if (Input.GetMouseButtonUp(1) && kureDuruyor && !kureGidiyor && !kureDonuyor && !kureHareketYapamaz)
            {
                Bevzier.instance.coroutineAllowed = true;
                kureDuruyor = false;
                kureDonuyor = true;
                kureGidiyor = false;
            }



        }

        /*if (kureDuruyor && !kureHareketYapamaz)
        {
            Collider2D hitEnemies = Physics2D.OverlapCircle(ruhKuresiParcasi.transform.position, playerDetectedRange, 12);
            if(hitEnemies != null)
            {
                Bevzier.instance.coroutineAllowed = true;
                kureDuruyor = false;
                kureDonuyor = true;
                kureGidiyor = false;
            }
        }*/

        if (!kureTamamlandi)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                kureClass.currentRuhKuresi.eSkill();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                kureClass.currentRuhKuresi.qSkill();
            }

        }

        if (kureGidiyor)
        {
            timer += Time.deltaTime;
            if (timer >= kureClass.currentRuhKuresi.ruhKuresiGidisSuresi)
            {
                timer = 0;
                kureGidiyor = false;
                kureDuruyor = true;
                kureDonuyor = false;
            }
        }

    }
    private void FixedUpdate()
    {
        if (kureGidiyor)
        {
            kureClass.currentRuhKuresi.TopuAtma();
        }
        else
        {
            durduguPos = ruhKuresiParcasi.transform.position;
            ruhKuresiRigid.velocity = Vector2.zero;
        }

        if (kureDonuyor)
        {
            Bevzier.instance.startBevzier();

            if (Bevzier.instance.bevzierBitti == true)
            {
                kureSpriteRenderer.enabled = false;
                kureTamamlandi = true;
                kureDonuyor = false;
                kureGidiyor = false;
                kureDuruyor = false;
                Bevzier.instance.bevzierBitti = false;
            }


        }
    }


    private void OnDrawGizmosSelected()
    {
        Vector2 point_two = new Vector2(durduguPos.x + 3, durduguPos.y + 3);
        Vector2 point_three = new Vector2(transform.position.x + 3, transform.position.y + 3);
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPos = Mathf.Pow(1 - t, 3) * durduguPos + 3 * Mathf.Pow(1 - t, 2) * t * point_two + 3 * (1 - t) *
                        Mathf.Pow(t, 2) * point_three +
                        Mathf.Pow(t, 3) * (Vector2)transform.position;

            Gizmos.DrawSphere(gizmosPos, 0.1f);
        }


        Gizmos.DrawLine(new Vector2(durduguPos.x, durduguPos.y), new Vector2(point_two.x, point_two.y));
        Gizmos.DrawLine(new Vector2(point_three.x, point_three.y), new Vector2(transform.position.x, transform.position.y));
    }

    public void GitmeZamaniSifirla()
    {
        timer = 0;
    }


}
