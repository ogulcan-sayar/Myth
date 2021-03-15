using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuhYiyiciMeleeAttack : StateMachineBehaviour
{
    private bool basildi;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        basildi = false;
        CharacterControl.instance.player.GetComponent<RuhYiyiciMove>().speed = CharacterControl.instance.walkConfig.AttackWalkingSpeed;
    }
    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public string ParameterName;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButtonDown(0) && basildi == false)
        {
            CharacterControl.instance.player.GetComponent<Animator>().SetTrigger(ParameterName);
            basildi = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //
    // }

}
