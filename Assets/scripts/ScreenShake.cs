using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance 
    {
        get; private set;
    }
    private float shakeTime;
    private CinemachineVirtualCamera cmVC = null;

    void Awake()
    {
        Instance = this;
        cmVC = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if(shakeTime <= 0)
            {
                CinemachineBasicMultiChannelPerlin cmBMCP = cmVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cmBMCP.m_AmplitudeGain = 0f;
            }
        }
    }
    
    public void Shake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cmBMCP = cmVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cmBMCP.m_AmplitudeGain = intensity;
        shakeTime = time;
    }
}
