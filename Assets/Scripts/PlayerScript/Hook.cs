using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputExtender;

public class Hook : MonoBehaviour
{
    public static Hook instance;
    Rigidbody2D charRigid;
    Transform charTrans;
    DistanceJoint2D hook;
    public LineRenderer line;
    Vector3 targetPoint;
    RaycastHit2D hit;
    public GameObject hookPoint;
    public  LayerMask hookable;
    public float step = .2f;

    public bool hookDragging;
    private bool soulHooking,justOneTimeAir,found,hooking;
    public bool airPush;
    Vector2 dir,faceDir;
    private bool duvaraHooking;
    public bool kureyeHookAtabilir;

    [Header("Kure")]
    public LayerMask kureLayer;
    private GameObject kure;
    public bool altBasildi;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        charRigid = gameObject.GetComponentInParent<Rigidbody2D>();
        charTrans = gameObject.transform.parent.GetComponent<Transform>();
        hook = gameObject.transform.parent.gameObject.GetComponent<DistanceJoint2D>();
        hook.enabled = false;
        line.enabled = false;
        if(CurrentRuhKuresiClass.instance.gameObject != null)
        {
            kure = CurrentRuhKuresiClass.instance.gameObject;
        }
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && !duvaraHooking && PlayerRangeAttack.instance.kureDuruyor && kureyeHookAtabilir)
        {
            altBasildi = true;
        }


        if (altBasildi && !duvaraHooking && PlayerRangeAttack.instance.kureDuruyor && kureyeHookAtabilir)
        {
            KureyeHookAt();
            KuredekiHookaKendiniCek();

            if (Input.GetKeyUp(KeyCode.LeftAlt) && !hooking)
            {
                altBasildi = false;
                StartCoroutine(Bekle());
            }
        }
        else
        {
            duvaraHooking = true;
            DuvaraHookAt();
            DuvardakiHookaKendiniCek();

            if (Input.GetMouseButtonUp(1) && !hooking)
            {
                StartCoroutine(Bekle());
            }
        }

        if (gameObject.GetComponentInParent<Move>().isGround)
        {
            justOneTimeAir = false;
            airPush = false;
            hookDragging = false;
            soulHooking = false;
            duvaraHooking = false;
        }


        
    }


    private void FixedUpdate()
    {
        if (airPush) // dodge yaparken bozuluyor // direkt yön değiştirdiği zaman bozuluyor // hiçbir şekilde bozulmuyor
        {
            if (CharacterControl.instance.dodge)
            {
                airPush = false;
            }
            charRigid.AddForce(dir.normalized * 200);
        }
    }


    public void ReleaseHook(LineRenderer whichRope)
    {
        CharacterControl.instance.hookDraging = hookDragging = false;
        hook.enabled = false;
        whichRope.enabled = false;
        found = false;
        hooking = false;
    }


    IEnumerator Bekle() //aynı anda küre atılmasını engellemek için
    {
        yield return new WaitForSeconds(0.01f);
        CharacterControl.instance.ruhKuresiAtilabilir = true;
    }
    // ALT tuşu için hook
    public void KureyeHookAt()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && !soulHooking) // alta basildigi zaman
        {
            FlipCharacter(kure.transform.position);
            CharacterControl.instance.ruhKuresiAtilabilir = false;
            KureBagiController.instance.LıneRendererEnabled(true);
            hook.enabled = true;
            hookPoint.transform.position = kure.transform.position;
            hook.connectedBody = hookPoint.GetComponent<Rigidbody2D>();
            hook.distance = Vector2.Distance(transform.position, kure.transform.position);
            KureBagiController.instance.NoktalariAyar();
            dir = new Vector2(kure.transform.position.x - transform.position.x, 0);
            found = true;
        }
    }

    public void KuredekiHookaKendiniCek()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && !Input.GetKeyDown(KeyCode.Space) && found && PlayerRangeAttack.instance.kureDuruyor) // kendini hooka doğru çekerken
        {
            CharacterControl.instance.hookDraging = hookDragging = true;
            hooking = true;
            if (hook.distance > .1f)
            {
                KureBagiController.instance.IpiGuncelle();
                hook.distance -= step * Time.deltaTime;
                soulHooking = true;
            }
            else // hookun en zirve noktasına geldiği zaman
            {
                ReleaseHook(KureBagiController.instance.line);
                faceDir = new Vector2(-charTrans.lossyScale.x, 0f);
                //harRigid.AddForce(dir * 100f);
                charRigid.velocity = new Vector2(0, 10f);
                airPush = true;
                justOneTimeAir = true;
            }
        }
        else if (hookDragging && !justOneTimeAir && PlayerRangeAttack.instance.kureDuruyor) // kendini hooka doğru çekerken bıraktıysa
        {
            ReleaseHook(KureBagiController.instance.line);
            faceDir = new Vector2(-charTrans.lossyScale.x, 0f);
            //harRigid.AddForce(dir * 100f);
            charRigid.velocity = new Vector2(0, 10f);
            airPush = true;
            justOneTimeAir = true;
        }

    }
    /////////////////////////

    public void DuvaraHookAt()
    {
        if (Mouse_Extender.isLongClick(1, 0.5f) && !soulHooking) // ilk basıldığı zaman hook yerini bulurken
        {
            CharacterControl.instance.ruhKuresiAtilabilir = false;
            targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPoint.z = 0;
            hit = Physics2D.Raycast(transform.position, targetPoint - transform.position, Mathf.Infinity, hookable);

            if (hit.collider != null)
            {
                FlipCharacter(targetPoint);
                line.enabled = true;
                hook.enabled = true;
                hookPoint.transform.position = hit.point;
                hook.connectedBody = hookPoint.GetComponent<Rigidbody2D>();
                hook.distance = Vector2.Distance(transform.position, hit.point);
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);
                dir = new Vector2(targetPoint.x - transform.position.x, 0);
                found = true;
            }

        }
    }

    public void DuvardakiHookaKendiniCek()
    {
        if (Input.GetMouseButton(1) && !Input.GetKeyDown(KeyCode.Space) && found) // kendini hooka doğru çekerken
        {
            CharacterControl.instance.hookDraging = hookDragging = true;
            hooking = true;
            if (hook.distance > 1f)
            {
                line.SetPosition(0, transform.position);
                hook.distance -= step * Time.deltaTime;
                soulHooking = true;
            }
            else // hookun en zirve noktasına geldiği zaman
            {
                ReleaseHook(line);

            }
        }
        else if (hookDragging && !justOneTimeAir) // kendini hooka doğru çekerken bıraktıysa
        {
            ReleaseHook(line);
            faceDir = new Vector2(-charTrans.lossyScale.x, 0f);
            //harRigid.AddForce(dir * 100f);
            charRigid.velocity = new Vector2(0, 10f);
            airPush = true;
            justOneTimeAir = true;
        }
    }

    public void FlipCharacter(Vector2 target)
    {
        if(target.x - charTrans.position.x < 0)
        {
            charTrans.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            Debug.Log(charTrans.name);
            charTrans.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }
}
