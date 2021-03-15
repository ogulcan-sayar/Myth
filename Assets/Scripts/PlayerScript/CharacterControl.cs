using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public static CharacterControl instance;
    public GameObject player;
    public GameObject hand;
    public bool moving, dodge, upoding, original_jump, double_jump , updoging_jump,kureAtildi;
    public bool ruhKuresiAtilabilir;
    public bool hookDraging;
    public bool airAttack;
    public WalkConfig walkConfig;

    [HideInInspector] public bool can_updoge_jump;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ruhKuresiAtilabilir = true;
}

    // Update is called once per frame


}
