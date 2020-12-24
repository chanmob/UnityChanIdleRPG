using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [Header("Cam")]
    [SerializeField]
    private float minFov = 5f;
    [SerializeField]
    private float maxFov = 15f;
    [SerializeField]
    private float sensitivity = 10f;
    private float fov = 10;
    
    private void Update()
    {
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        InGameManager.instance.curCam.m_Lens.FieldOfView = fov;
    }
}
