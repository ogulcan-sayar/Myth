using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class YesilSkillBaseClass : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D kureRigid;
    [SerializeField]
    protected LayerMask enemy;
    [SerializeField]
    protected Light2D kureIsigi;
    public float def_innerRadius, def_outerRadius, def_intensity;
    [HideInInspector] protected float cur_innerRadius, cur_outerRadius, cur_intensity;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Use()
    {

    }
}
