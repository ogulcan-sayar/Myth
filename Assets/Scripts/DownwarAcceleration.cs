using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownwarAcceleration : StateMachineBehaviour
{
    public float fallForce;
    private Rigidbody2D charRigid;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Hook.instance.airPush = false;
        charRigid = CharacterControl.instance.player.GetComponent<Rigidbody2D>();
        charRigid.AddForce(Vector2.down * fallForce);
        CharacterControl.instance.airAttack = true;
        Physics2D.IgnoreLayerCollision(11, 12); // enemy ve player collision engellenmesi
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Physics2D.IgnoreLayerCollision(11, 12,false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
