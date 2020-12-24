using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : Singleton<InGameUIManager>
{
    public void SwitchCamera(int idx)
    {
        InGameManager.instance.SetCamera(idx);
    }
}
