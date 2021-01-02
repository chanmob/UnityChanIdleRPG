using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : Singleton<InGameUIManager>
{
    public UI_Achievement ui_Achievement;
    public UI_Growth ui_Growth;
    public UI_Setting ui_Setting;
    public UI_Store ui_Store;
    public UI_ItemInventory ui_ItemInventory;
    public UI_MyInfo ui_MyInfo;
    public UI_SkillInventory ui_SkillInventory;
    public UI_SwitchCam ui_SwitchCam;
    public UI_InGameMainUI ui_InGameMainUI;
}