using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRuhKuresiClass : MonoBehaviour
{
    public static CurrentRuhKuresiClass instance;
    public Rigidbody2D ruhKuresiRigid;
    public GameObject player;
    public float ruhKuresiGidisSuresi;
    public float throwSpeed;
    protected Vector2 attackDirection;
    public float bagDırencMesafesi;
    [HideInInspector] public CurrentRuhKuresiClass currentRuhKuresi;

    
    protected float kureIleOyuncuArasindakiMesafe;

    private void Awake()
    {
        instance = this;
    }

    public virtual void TopuAtma()
    {

    }

    public virtual void MesafeHesapla()
    {

    }

    public virtual void KureIleOyuncuArasindakiMesafe()
    {

    }

    public virtual void TopuGeriDondur()
    {

    }

    public virtual void eSkill()
    {

    }
  
    public virtual void qSkill()
    {

    }
    private void Start()
    {
        player = CharacterControl.instance.player;
        ruhKuresiRigid = GetComponent<Rigidbody2D>();
        currentRuhKuresi = GetComponent<Kirmizi>();
    }



}


