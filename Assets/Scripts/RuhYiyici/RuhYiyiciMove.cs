using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuhYiyiciMove : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    private float moveInput;

    public Vector2 knockbackSpeed;

    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public bool isGround;
    public LayerMask whatIsGround;
    public BoxCollider2D boxcol2d;


    private int extraJump;
    public int extraJumpValue;

    private Animator characterAnimator;
    private bool knockback;
    public bool moving;
    public bool canDoubleJump;
    public bool updogingjump, upodingJumpCtrl;
    public float ct;
    // Start is called before the first frame update
    void Start()
    {
        //extraJump = 0;
        extraJump = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckGround()) // original jump
            {
               
                Zipla();
                //extraJump = extraJumpValue;
                CharacterControl.instance.original_jump = true;
            }
            else
            {
                if (CharacterControl.instance.can_updoge_jump == true && extraJump > 0) //updoge jumping
                {// orjinal jump yapılmadan updodge yapılırsa sadece extra jumptan harcama yapıyor orijinal hakkı siliniyor.
                    if (CharacterControl.instance.original_jump == false && upodingJumpCtrl == false)
                    {
                        extraJump++;
                        upodingJumpCtrl = true;
                    }
                    Zipla();
                    CharacterControl.instance.updoging_jump = true;
                    extraJump--;
                }

                else if (extraJump > 0) //double jump
                {
                    CharacterControl.instance.double_jump = true;
                    CharacterControl.instance.original_jump = false;
                    CharacterControl.instance.updoging_jump = false;
                    Zipla();
                    extraJump--;
                }
            }

        }

        characterAnimator.SetBool("isGrounded", isGround);


    }

    void FixedUpdate()
{
    if (CheckGround())
    {

            ct += Time.deltaTime;
        extraJump = extraJumpValue;
        if (ct > 0.13f)
        {
            upodingJumpCtrl = false;
            CharacterControl.instance.original_jump = false;
            CharacterControl.instance.double_jump = false;
            CharacterControl.instance.updoging_jump = false;
            ct = 0;
            //extraJump = 0;
        }
    }

    isGround = CheckGround();
    moveInput = Input.GetAxis("Horizontal");
    CheckMove();

    rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    Flip();
    if (Mathf.Abs(moveInput) > Mathf.Epsilon)
    {
        characterAnimator.SetInteger("AnimState", 2);
    }
    else
    {
        characterAnimator.SetInteger("AnimState", 0);
    }
}

void Zipla()
{
    rb.velocity = Vector2.up * jumpforce;
    characterAnimator.SetTrigger("Jump");

}
    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0 && !knockback)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Input.GetAxis("Horizontal") < 0 && !knockback)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public bool CheckGround()
    {

        RaycastHit2D raycast = Physics2D.BoxCast(boxcol2d.bounds.center, boxcol2d.bounds.size, 0f, Vector2.down, .011f, whatIsGround);
        return raycast.collider != null;
    }

    public void CheckMove()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            CharacterControl.instance.moving = moving = false;
        }
        else
        {
            // CharacterControl.instance.original_jump = false;
            CharacterControl.instance.moving = moving = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 dir = new Vector2(collision.transform.position.x - transform.position.x, 0f);
            if (dir.x == 0)
            {
                rb.AddForce(Vector2.left * 100, ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(-dir.normalized * 100, ForceMode2D.Force);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3.3f;
        }
    }


}
