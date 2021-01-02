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

    public void OpenMyInfo()
    {
        InGameUIManager.instance.ui_MyInfo.gameObject.SetActive(true);
    }

    public void OpenItemInventory()
    {
        
    }

    public void OpenGrowth()
    {
        
    }

    public void OpenAchievement()
    {
        
    }

    public void OpenSkillInventory()
    {
        
    }

    public void OpenStore()
    {
        
    }

    public void OpenSetting()
    {
        
    }
}