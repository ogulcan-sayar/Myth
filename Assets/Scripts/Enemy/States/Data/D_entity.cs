using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_entity : ScriptableObject
{
    public float wallCheckDistance = .2f;
    public float ledgeCheckDistance = .4f;
    public float groundCheckDistance = .3f;

    public float macHealth = 30;
    public float damageHopSpeed = 3;

    public float maxAgroDistance = 4;
    public float minAgroDistance = 3;

    public float stunResistance = 3, stunRecoveryTime = 2;
    

    public float closeRangeActionDistance = 1;

    public LayerMask whatIsGround, whatIsPlayer;
}
