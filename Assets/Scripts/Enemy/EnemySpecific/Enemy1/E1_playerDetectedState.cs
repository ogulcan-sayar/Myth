using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_playerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;

    public E1_playerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_playerDetected stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }

        else if (performLongRangeAction)
        {
            //enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.chargeState);
        }else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
