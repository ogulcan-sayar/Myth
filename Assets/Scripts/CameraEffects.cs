using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public static CameraEffects instance;
    public Camera mainCam;
    float shakeAmount;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }
    }

    public void CameraShake(float amt, float length)
    {
        shakeAmount = amt;
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", length);
    }
    void DoShake()
    {
        if(shakeAmount > 0)
        {
            Vector3 campos = mainCam.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;

            campos.x += offsetX;
            campos.y += offsetY;

            mainCam.transform.position = campos;
        }
    }

    void StopShake()
    {
        CancelInvoke("DoShake");
        mainCam.transform.localPosition = Vector3.zero;
    }
}
