using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGameMainUI : MonoBehaviour
{
    public Image image_Sprite;

    public Text text_Coin;
    public Text text_Gem;
    public Text text_Exp;
    
    public Slider slider_HP;
    public Slider slider_Exp;

    public Sprite[] characterSprite;

    public void SwitchCamOn()
    {
        InGameUIManager.instance.ui_SwitchCam.gameObject.SetActive(true);
    }
}