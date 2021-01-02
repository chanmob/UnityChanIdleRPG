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
    
    private UserData _userData;

    public Player CurPlayer
    {
        get;
        private set;
    }

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
        CurPlayer = PlayerManager.instance.players[idx];
    }

    public void SetDeathTime(int idx)
    {
        _playerDeathTime[idx] = _playTime + 10f;
    }

    private void Start()
    {
        _userData = new UserData();
        curCam = cams[0];
        CurPlayer = PlayerManager.instance.players[0];
    }

    private void Update()
    {
        _playTime += Time.deltaTime;

        if (PlayerManager.instance.players[0].IsDeath && _playerDeathTime[0] >= _playTime)
        {
            PlayerManager.instance.players[0].Revive();
        }
        
        if (PlayerManager.instance.players[1].IsDeath && _playerDeathTime[1] >= _playTime)
        {
            PlayerManager.instance.players[1].Revive();
        }
        
        if (PlayerManager.instance.players[2].IsDeath && _playerDeathTime[2] >= _playTime)
        {
            PlayerManager.instance.players[2].Revive();
        }

        InGameUIManager.instance.ui_InGameMainUI.slider_HP.value = CurPlayer.playerData.curHp / (float)CurPlayer.playerData.hp;
        float exp = _userData.curExp / (float)_userData.needExp;
        InGameUIManager.instance.ui_InGameMainUI.slider_Exp.value = exp;
        InGameUIManager.instance.ui_InGameMainUI.text_Exp.text = (exp * 100).ToString("0.00") + "%";
        InGameUIManager.instance.ui_InGameMainUI.text_Coin.text = _userData.coin.ToString();
        InGameUIManager.instance.ui_InGameMainUI.text_Gem.text = _userData.gem.ToString();
    }
}