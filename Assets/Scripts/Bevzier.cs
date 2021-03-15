using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bevzier : MonoBehaviour
{

    private int routeToGo;
    private float tParam;
    private float speedModifier;
    public bool coroutineAllowed;
    private Vector2 kurePos;
    private Rigidbody2D kureRigid;
    public Transform handPos;
    public static Bevzier instance;
    public bool bevzierBitti;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        routeToGo = 0;
        tParam = 0;
        speedModifier = 2f;
        kureRigid = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }


    IEnumerator GoByTheRoute (int routeNumber)
    {
        coroutineAllowed = false;

        Vector2 p0 = PlayerRangeAttack.instance.durduguPos;
        Vector2 p1 = new Vector2(PlayerRangeAttack.instance.durduguPos.x + 3, PlayerRangeAttack.instance.durduguPos.y + 3);
        Vector2 p2 = new Vector2(handPos.transform.position.x + 3, handPos.transform.position.y + 3);
        Vector2 p3 = handPos.transform.position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            kurePos = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) *
                                 Mathf.Pow(tParam, 2) * new Vector2(handPos.transform.position.x + 3, handPos.transform.position.y + 3) +
                                 Mathf.Pow(tParam, 3) *(Vector2) handPos.transform.position;

            kureRigid.MovePosition(kurePos);

            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo += 1;

        coroutineAllowed = false;
        bevzierBitti = true;
    }

    public void startBevzier()
    {
        if (coroutineAllowed)
            StartCoroutine(GoByTheRoute(routeToGo));
    }
}
