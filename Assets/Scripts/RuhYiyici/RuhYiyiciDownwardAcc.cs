using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuhYiyiciDownwardAcc : StateMachineBehaviour
{
    public float fallForce;
    RaycastHit2D[] hitEnemies;
    Transform charTrans;
    public LayerMask enemyLayers;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        charTrans = CharacterControl.instance.player.transform;
        CharacterControl.instance.player.GetComponent<RuhYiyiciMove>().rb.AddForce(Vector2.down * fallForce);
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
        Physics2D.IgnoreLayerCollision(11, 12, false);
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
