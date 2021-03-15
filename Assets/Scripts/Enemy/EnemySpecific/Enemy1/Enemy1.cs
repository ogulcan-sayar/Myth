using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_idleState idleState { get; private set; }
    public E1_moveState moveState { get; private set; }

    public E1_playerDetectedState playerDetectedState { get; private set; }

    public E1_chargeState chargeState { get; private set; }

    public E1_lookForPlayerState lookForPlayerState { get; private set; }

    public E1_meleeAttackState meleeAttackState { get; private set; }

    public E1_stunState stunState { get; private set; }

    public D_moveState moveStateData;
    public D_idleState idleStateData;
    public D_playerDetected playerDetectedData;
    public D_ChargeState chargeStateData;
    public D_lookForPlayerState lookForPlayerData;
    public D_meleeAttackState meleeAttackStateData;
    public Transform meleeAttackPosition;
    public D_stunState stunStateData;

    public override void Start()
    {
        base.Start();

        moveState = new E1_moveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_idleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_playerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new E1_chargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new E1_lookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerData, this);
        meleeAttackState = new E1_meleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new E1_stunState(this, stateMachine, "stun", stunStateData, this);  

        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
    }
}
