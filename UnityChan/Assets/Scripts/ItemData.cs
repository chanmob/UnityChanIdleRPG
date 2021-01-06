using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object Asset/ItemData", order = 2)]
public class ItemData : ScriptableObject
{
    public enum Tier
    {
        NORMAL,
        RARE,
        EPIC,
        UNIQUE,
        LEGENDARY,
        NONE
    }

    public enum Part
    {
        HEAD,
        WEAPON,
        ARMOR,
        SHOES,
        NONE
    }

    public enum WeaponType
    {
        DUALSWORD,
        POLEARM,
        GREATSWORD,
        NONE
    }

    public Tier tier = Tier.NONE;
    public Part part = Part.NONE;
    public WeaponType weaponType = WeaponType.NONE;

    public Sprite itemSprite;
    
    public string itemName;
    
    public int addDamage;
    public int addDefense;
    public int addHp;
    public int addCritical;
    public int addDps;
    public int addArmorPenetration;
    public int addHpRegeneration;

    public void ItemOn(PlayerData playerData)
    {
        playerData.hp += addHp;
        playerData.hpRegeneration += addHpRegeneration;
        playerData.damage += addDamage;
        playerData.defense += addDefense;
        playerData.critical += addCritical;
        playerData.dps += addDps;
        playerData.armorPenetration += addArmorPenetration;
    }

    public void ItemOff(PlayerData playerData)
    {
        playerData.hp -= addHp;
        playerData.hpRegeneration -= addHpRegeneration;
        playerData.damage -= addDamage;
        playerData.defense -= addDefense;
        playerData.critical -= addCritical;
        playerData.dps -= addDps;
        playerData.armorPenetration -= addArmorPenetration;
    }
}
