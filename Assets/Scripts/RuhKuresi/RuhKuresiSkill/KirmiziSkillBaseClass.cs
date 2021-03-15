using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirmiziSkillBaseClass : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D kureRigid;
    [SerializeField]
    protected LayerMask enemy;
    
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
