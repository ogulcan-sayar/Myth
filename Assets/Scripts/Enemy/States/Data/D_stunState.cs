using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]

public class D_stunState : ScriptableObject
{
    public float stunTime = 3;
    public float stunKnockbackTime = .2f;
    public float stunKnockbackSpeed = 20;

    public Vector2 stunKnockBackAngle;
}
