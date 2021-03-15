using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upodge : MonoBehaviour
{
    Rigidbody2D charRigid;
    Vector3 targetPoint;
    RaycastHit2D hit;
    public LayerMask hookable;
    private float Yvelocity,Xvelocity;
    public float hookDistance;
    private float xMenzil,zaman;
    bool Yctrl;
    public bool upoding;

    Vector2 dir,dirX;
    void Start()
    {
        upoding = false;
        Yctrl = false;
        charRigid = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(1) && !upoding && !CharacterControl.instance.dodge)
        {
            targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPoint.z = 0;
            dir = targetPoint - transform.position;
            hit = Physics2D.Raycast(transform.position, dir, hookDistance, hookable);
            if (hit.collider != null)
            {
                Yvelocity = Mathf.Sqrt(hit.point.y /2 * charRigid.gravityScale);
                zaman =  Yvelocity / charRigid.gravityScale;
                //xMenzil = hit.point.x - transform.position.x;
                //Xvelocity = 2 * xMenzil / zaman;
                dirX = new Vector2(dir.x, 0);

                CharacterControl.instance.upoding = upoding = true;
                CharacterControl.instance.can_updoge_jump = true;
                Yctrl = true;
            }
        }
        else if (upoding)
        {
            zaman -= Time.deltaTime;
            if(zaman < 0)
            {
                if (gameObject.GetComponentInParent<Move>().isGround)
                {
                    CharacterControl.instance.upoding = upoding = false;
                    CharacterControl.instance.can_updoge_jump = false;
                }
            }
        }

    }
    private void FixedUpdate()
    {
        if (upoding) 
        {
             if (Yctrl)
             {
                //if(CharacterControl.instance)
                 charRigid.velocity = new Vector2(0, Yvelocity*5f);
                 Yctrl = false;
             }
            //charRigid.velocity = new Vector2(Xvelocity/2 + charRigid.velocity.x, charRigid.velocity.y);
            charRigid.AddForce(dirX * 50);

        }
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, targetPoint);
    }
}
