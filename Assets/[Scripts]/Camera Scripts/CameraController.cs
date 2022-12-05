using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    //lots of inspiration from this tutorial https://blog.gamedev.tv/rotatingcamera/

    public float StartFOV = 35;
    public float CloseFOV = 5;
    public float FarFOV = 75;
    public float changeAmount = 2.0f;
    public float CurrentFOV;

    CinemachineVirtualCamera cmVcam;
    
    // Start is called before the first frame update
    void Start()
    {
        cmVcam = GetComponent<CinemachineVirtualCamera>();
        CurrentFOV = StartFOV;
        cmVcam.m_Lens.FieldOfView = StartFOV;
    }

    public void OnScroll(InputValue value)
    {
        float direction = value.Get<float>();

        if (direction != 0)
        {
            CurrentFOV += changeAmount * -direction / Mathf.Abs(direction);
        }
        CurrentFOV = Mathf.Clamp(CurrentFOV, CloseFOV, FarFOV);

        cmVcam.m_Lens.FieldOfView = CurrentFOV;
    }
}
