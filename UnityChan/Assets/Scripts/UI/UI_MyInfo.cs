using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MyInfo : MonoBehaviour
{
    [SerializeField]
    private Text _text_Hp;
    [SerializeField]
    private Text _text_HpRegeneration;
    [SerializeField]
    private Text _text_Damage;
    [SerializeField]
    private Text _text_Defense;
    [SerializeField]
    private Text _text_ArmorPenetration;
    [SerializeField]
    private Text _text_Critical;
    [SerializeField]
    private Text _text_Range;
    [SerializeField]
    private Text _text_Dps;
    [SerializeField]
    private Text _text_MoveSpeed;

    
    [SerializeField]
    private Image _image_Portrait;
    [SerializeField]
    private Image _image_Weapon;
    [SerializeField]
    private Image _image_Head;
    [SerializeField]
    private Image _image_Armor;
    [SerializeField]
    private Image _image_Boots;
    [SerializeField]
    private Image _image_Skill1;
    [SerializeField]
    private Image _image_Skill2;
    [SerializeField]
    private Image _image_Skill3;

    private void OnEnable()
    {
        PlayerData playerData = InGameManager.instance.CurPlayer.playerData;

        switch (playerData.playerType)
        {
            case PlayerType.DUALSWORD:
                _image_Portrait.sprite = InGameUIManager.instance.ui_InGameMainUI.characterSprite[0];
                break;
            case PlayerType.POLEARM:
                _image_Portrait.sprite = InGameUIManager.instance.ui_InGameMainUI.characterSprite[1];
                break;
            case PlayerType.GREATSWORD:
                _image_Portrait.sprite = InGameUIManager.instance.ui_InGameMainUI.characterSprite[2];
                break;
        }
        
        _text_Hp.text = "체력 : " + playerData.hp;
        _text_HpRegeneration.text = "체력 재생 : " + playerData.hpRegeneration;
        _text_Damage.text = "데미지 : " + playerData.damage;
        _text_Defense.text = "방어력 : " + playerData.defense;
        _text_ArmorPenetration.text = "방어구 관통력 : " + playerData.armorPenetration;
        _text_Critical.text = "치명타 확률 : " + playerData.critical;
        _text_Range.text = "사거리 : " + playerData.range;
        _text_Dps.text = "공격 속도 : " + playerData.dps;
        _text_MoveSpeed.text = "이동 속도 : " + playerData.moveSpeed;

        if (playerData.item_Weapon != null)
        {
            _image_Weapon.enabled = true;
            _image_Weapon.sprite = playerData.item_Weapon.itemSprite;
        }
        else
        {
            _image_Weapon.enabled = false;
        }
        
        if (playerData.item_Head != null)
        {
            _image_Head.enabled = true;
            _image_Head.sprite = playerData.item_Head.itemSprite;
        }
        else
        {
            _image_Head.enabled = false;
        }

        if (playerData.item_Armor != null)
        {
            _image_Armor.enabled = true;
            _image_Armor.sprite = playerData.item_Armor.itemSprite;
        }
        else
        {
            _image_Armor.enabled = false;
        }

        if (playerData.item_Boots != null)
        {
            _image_Boots.enabled = true;
            _image_Boots.sprite = playerData.item_Boots.itemSprite;
        }
        else
        {
            _image_Boots.enabled = false;
        }

        if (playerData.skill_first != null)
        {
            _image_Skill1.enabled = true;
            _image_Skill1.sprite = playerData.skill_first.skillSprite;
        }
        else
        {
            _image_Skill1.enabled = false;
        }

        if (playerData.skill_second != null)
        {
            _image_Skill2.enabled = true;
            _image_Skill2.sprite = playerData.skill_second.skillSprite;
        }
        else
        {
            _image_Skill2.enabled = false;
        }

        if (playerData.skill_third != null)
        {
            _image_Skill3.enabled = true;
            _image_Skill3.sprite = playerData.skill_third.skillSprite;
        }
        else
        {
            _image_Skill3.enabled = false;
        }
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
