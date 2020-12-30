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
        SHOES
    }

    public Tier tier = Tier.NONE;
    
    public Sprite itemSprite;
    
    public string itemName;
    
    public int addDamage;
    public int addDefense;
    public int addHp;
    public int addCritical;
    public int addDps;
    public int addArmorPenetration;
    public int addHpRegeneration;
}
