using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/AttackConfig")]
public class AttackConfig : ScriptableObject
{
    [Header("Attack Stats")]
    public int extraDamage;

    [Header("Camera Shake")]
    public float shakeAmount;
    public float shakeLenght;
}
