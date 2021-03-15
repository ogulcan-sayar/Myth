using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuhYiyiciDodge : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dashSpeed;
    public float airDashSpeed;
    public Animator charAnim;
    private float dashTime;
    public float startDashTime;
    private int direction;

    private bool lookleft;

    public bool dodge;

    bool airDashctrl;


    public SpriteRenderer sprite;
    public float upDash , gravity;
    bool upDashOnay , upDashAction;
    void Start()
    {
        gravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterControl.instance.moving)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                lookleft = false;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                lookleft = true;
            }

            if (direction == 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && lookleft && !upDashOnay)
                {
                    direction = 1;
                }
                else if (Input.GetKeyDown(KeyCode.LeftShift) && !lookleft && !upDashOnay)
                {
                    direction = 2;
                }

            }
        }



        if (Input.GetKey(KeyCode.W))
        {
            upDashOnay = true;

            if (Input.GetKeyDown(KeyCode.LeftShift)&& gameObject.GetComponentInParent<RuhYiyiciMove>().isGround)
            {
                upDashAction = true;
            }


        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            upDashOnay = false;
        }






        if (gameObject.GetComponentInParent<RuhYiyiciMove>().isGround)
        {

            airDashctrl = true;
        }

        /*if(gameObject.GetComponent<Rigidbody2D>().velocity.y == 0f)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
        }*/
       
    }

    private void FixedUpdate()
    {
        if (direction != 0 && gameObject.GetComponentInParent<RuhYiyiciMove>().isGround) //ground dash
        {

            if (dashTime <= 0)
            {
                sprite.enabled = true;
                CharacterControl.instance.dodge = dodge = false;
                charAnim.SetBool("dodging", false);
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                sprite.enabled = false;
                dashTime -= Time.deltaTime;
                CharacterControl.instance.dodge = dodge = true;
                charAnim.SetBool("dodging", true);
                if (direction == 1)
                {
                    //rb.velocity = Vector2.left * dashSpeed;
                    rb.AddForce(Vector2.left * dashSpeed);
                }
                else if (direction == 2)
                {
                    //rb.velocity = Vector2.right * dashSpeed;
                    rb.AddForce(Vector2.right * dashSpeed);
                }

            }
        }
        else if (direction != 0 && !gameObject.GetComponentInParent<RuhYiyiciMove>().isGround && airDashctrl) // airdash
        {

            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            if (dashTime <= 0)
            {
                rb.AddForce(Vector2.up * 300);
                rb.gravityScale = 3.3f;
                CharacterControl.instance.dodge = dodge = false;
                charAnim.SetBool("dodging", false);
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
                airDashctrl = false;
            }
            else
            {
                dashTime -= Time.deltaTime;
                CharacterControl.instance.dodge = dodge = true;
                charAnim.SetBool("dodging", true);
                if (direction == 1)
                {
                    //rb.velocity = Vector2.left * dashSpeed;
                    rb.AddForce(Vector2.left * airDashSpeed);
                }
                else if (direction == 2)
                {
                    //rb.velocity = Vector2.right * dashSpeed;
                    rb.AddForce(Vector2.right * airDashSpeed);
                }
            }
        }
        else // havadayken shift yapıldığında, dodgeyu yere düşünce yapmasını engellemek için
        {
            direction = 0;
        }

        if (upDashAction && gameObject.GetComponentInParent<RuhYiyiciMove>().isGround) ///upDash
        {

            rb.gravityScale = 9f;
           // sprite.enabled = false;
            rb.AddForce(Vector2.up * upDash);

            StartCoroutine(upDashWait(0.3f));


        }

     
    }

    public bool GetDashStatus()
    {
        return dodge;
    }

    IEnumerator upDashWait (float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        rb.gravityScale = 3.3f;
        //sprite.enabled = true;
        upDashOnay = false;
        upDashAction = false;


    }
}
