using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SwitchCam : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _sprite_ButtonImg;
    
    [SerializeField]
    private Image[] _Image_Button;
    
    public void SwitchCamera(int idx)
    {
        for (int i = 0; i < 3; i++)
        {
            _Image_Button[i].sprite = _sprite_ButtonImg[0];
        }
        
        _Image_Button[idx].sprite = _sprite_ButtonImg[1];

        InGameManager.instance.SetCamera(idx);
        PanelClose();
    }

    public void PanelClose()
    {
        gameObject.SetActive(false);
    }
}
