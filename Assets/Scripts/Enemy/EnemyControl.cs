using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private enum State
    {
        Walk,
        KnockBack,
        Dead
    }
    private State currentstate;
    private bool groundDetected, wallDetected, triggered;
    private int facingDirection, damageDirection;
    private float[] attackDetails = new float[2];
    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Vector2 movement;
    private Animator aliveAnim;

    public Transform groundCheck, wallCheck;
    public LayerMask whatIsGround, whatIsPlayer;
    public float groundCheckDistance,
        wallCheckDistance, 
        moveSpeed, 
        maxHealth, 
        currenthealth, 
        knockBackStartTime, 
        knockBackDuration, 
        petrolTime, 
        timer, 
        distanceToTrigger;
    public Vector2 knockBackSpeed;

    void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        facingDirection = 1;
        aliveAnim = alive.GetComponent<Animator>();
        //currentLimit = limit2;
        timer = petrolTime;
        currenthealth = maxHealth;
        

    }
    void Update()
    {
        switch (currentstate)
        {
            case State.Walk:
                UpdateWalkingState();
                break;
            case State.KnockBack:
                UpdateKnockBackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }

        timer -= Time.deltaTime;

        if(Vector2.Distance(transform.position, CharacterControl.instance.player.transform.position) < distanceToTrigger )
        {
            triggered = true;
        }

    }
    private void BeginWalkingState()
    {

    }
    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

       // CheckTouchDamage();

        if(!groundDetected || wallDetected && !triggered)
        {
            Flip();
            timer = petrolTime;
        }
        else if(!triggered)
        {
            movement.Set(moveSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;

            if(timer < 0)
            {
                Flip();
                timer = petrolTime;
            }
        }else if (triggered)
        {
        //    Vector2.MoveTowards(transform.position);
        }
    }
    private void ExitWalkingState()
    {

    }





    private void BeginKnockBackState()
    {
        knockBackStartTime = Time.time;
        movement.Set(knockBackSpeed.x * damageDirection, knockBackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }
    private void UpdateKnockBackState()
    {
        if(Time.time >= knockBackStartTime + knockBackDuration)
        {
            SwitchState(State.Walk);
        }
    }
    private void ExitKnockBackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }





    private void BeginDeadState()
    {
        Destroy(gameObject);
    }
    private void UpdateDeadState()
    {

    }
    private void ExitDeadState()
    {

    }



    private void Damage(float[] attackDetails)
    {
        currenthealth -= attackDetails[0];

        if(attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        //Hit Particle
        if(currenthealth > 0)
        {
            SwitchState(State.KnockBack);
        }else if(currenthealth <= 0)
        {
            SwitchState(State.Dead);
        }
    }

  /*  private void CheckTouchDamage()
    {
        if(Time.time >= lastTouchDamageTime + touchDamageCoolDown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);
            
            if( hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }*/

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0, 180, 0);
    }
    private void SwitchState(State state)
    {
        switch (currentstate)
        {
            case State.Walk:
                ExitWalkingState();
                break;
            case State.KnockBack:
                ExitKnockBackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walk:
                BeginWalkingState();
                break;
            case State.KnockBack:
                BeginKnockBackState();
                break;
            case State.Dead:
                BeginDeadState();
                break;
        }

        currentstate = state;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

       /* Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2)), 
            botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2)), 
            topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2)), 
            topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);*/
    }
}
