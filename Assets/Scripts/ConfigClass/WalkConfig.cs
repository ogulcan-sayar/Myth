using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/WalkConfig")]
public class WalkConfig : ScriptableObject
{
    public float WalkingSpeed;
    public float AttackWalkingSpeed;
}
