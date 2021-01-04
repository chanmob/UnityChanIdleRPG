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
        InGameUIManager.instance.ui_ItemInventory.gameObject.SetActive(true);
    }

    public void OpenGrowth()
    {
        InGameUIManager.instance.ui_Growth.gameObject.SetActive(true);
    }

    public void OpenAchievement()
    {
        InGameUIManager.instance.ui_Achievement.gameObject.SetActive(true);
    }

    public void OpenSkillInventory()
    {
        InGameUIManager.instance.ui_SkillInventory.gameObject.SetActive(true);
    }

    public void OpenShop()
    {
        InGameUIManager.instance.ui_Store.gameObject.SetActive(true);
    }

    public void OpenSetting()
    {
        InGameUIManager.instance.ui_Setting.gameObject.SetActive(true);
    }
}