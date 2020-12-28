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
        UNIQUE,
        EPIC,
        LEGENDARY,
        NONE
    }

    public Tier tier = Tier.NONE;
    public Sprite itemSprite;
    public int addDamage;
    public int addDefense;
    public int addHp;
    public int addCritical;
    public int addDps;
    public int addArmorPenetration;
    public int addHpRegeneration;
}
