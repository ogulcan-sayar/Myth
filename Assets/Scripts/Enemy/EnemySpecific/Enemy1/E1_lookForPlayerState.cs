﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_lookForPlayerState : LookForPlayerState
{

    private Enemy1 enemy;
    public E1_lookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_lookForPlayerState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        if (isPlayerInMinAgrorange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);

        }else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
