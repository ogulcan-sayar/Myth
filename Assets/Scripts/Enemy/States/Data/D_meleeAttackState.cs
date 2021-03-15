﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]

public class D_meleeAttackState : ScriptableObject
{
    public float attackRadius = .5f;
    public float attackDamage = 10;
    public LayerMask whatIsPlayer;
}
