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

    private float[] _playerDeathTime = new float[3];

    private float _playTime;

    private Player player;
    
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
        UI_InGameMainUI mainUI = InGameUIManager.instance.ui_InGameMainUI;
        mainUI.image_Sprite.sprite = mainUI.characterSprite[idx];
        player = PlayerManager.instance.players[idx];
    }

    public void SetDeathTime(int idx)
    {
        _playerDeathTime[idx] = _playTime + 10f;
    }

    private void Start()
    {
        curCam = cams[0];
        player = PlayerManager.instance.players[0];
    }

    private void Update()
    {
        _playTime += Time.deltaTime;
        
        if (_playerDeathTime[0] >= _playTime)
        {
            PlayerManager.instance.players[0].Revive();
        }

        if (_playerDeathTime[1] >= _playTime)
        {
            PlayerManager.instance.players[1].Revive();
        }

        if (_playerDeathTime[2] >= _playTime)
        {
            PlayerManager.instance.players[2].Revive();
        }
    }
}