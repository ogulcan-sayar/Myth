﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected D_stunState stateData;

    protected bool isStunTimeOver, isGrounded, isMovementStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;

    public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_stunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = entity.CheckGround();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
        isMovementStopped = false;
        entity.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockBackAngle, entity.lastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }

        if (isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            entity.SetVelocity(0);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
