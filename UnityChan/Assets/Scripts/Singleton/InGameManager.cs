using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InGameManager : Singleton<InGameManager>
{
    [SerializeField] private CinemachineVirtualCamera[] cams;

    private int _curCamIdx = 0; 
    
    public CinemachineVirtualCamera curCam;

    public void SetCamera(int idx)
    {
        if (idx == _curCamIdx)
            return;

        int len = cams.Length;

        for (int i = 0; i < len; i++)
        {
            cams[i].gameObject.SetActive(false);
        }

        _curCamIdx = idx;
        curCam = cams[idx];
        cams[idx].gameObject.SetActive(true);
    }

    private void Start()
    {
        curCam = cams[0];
    }
}