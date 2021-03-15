using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KureBagiController : MonoBehaviour
{
    public static KureBagiController instance;
    [HideInInspector] public LineRenderer line;
    private PlayerRangeAttack playerRangeAttack;
    [SerializeField] private GameObject kure;
    [SerializeField] private GameObject hand;
    public float maxTransparent;
    private float currentTransparent;
    Color color;
    Gradient gradient = new Gradient();
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        line = GetComponent<LineRenderer>();
        playerRangeAttack = PlayerRangeAttack.instance;
        kure = transform.parent.gameObject;
        hand = CharacterControl.instance.hand.gameObject;
        color = line.colorGradient.colorKeys[0].color;
    }

    // Update is called once per frame
    void Update()
    {

        if (!playerRangeAttack.kureTamamlandi && playerRangeAttack.kureDuruyor)
        {
            if (!line.enabled)
            {
                
                
                line.enabled = true;
                
            }
            line.SetPosition(0, hand.transform.position);
            line.SetPosition(1, kure.transform.position);
        }
        else
        {

            line.enabled = false;
        }
    }

    public void ChangeColor(float distance, float maxMesafe)
    {
        
        currentTransparent = maxTransparent - ((maxTransparent / maxMesafe) * distance);
        //Debug.Log("Distance: " + distance + " maxMesafe: " + maxMesafe + " currentTransparent: " + currentTransparent);
        float floatTransparet = ((currentTransparent * 0.5f) / maxMesafe/10);
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0, 0.0f), new GradientAlphaKey(floatTransparet, 1.0f) }
            );
        line.colorGradient = gradient;
    }

    public void LıneRendererEnabled(bool ac)
    {
        line.enabled = ac;
    }


    public void NoktalariAyar()
    {
        line.SetPosition(0, hand.transform.position);
        line.SetPosition(1, kure.transform.position);
    }

    public void IpiGuncelle()
    {
        line.SetPosition(0, hand.transform.position);
    }
}
