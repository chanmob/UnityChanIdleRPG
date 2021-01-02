using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Object Asset/SkillData", order = 3)]
public class SkillData : ScriptableObject
{
    public enum SkillType
    {
        
    }

    public SkillType skillType;
    
    public Sprite skillSprite;
    
    public string skillName;

    public void RegistSkill()
    {
        
    }

    public void UnRegistSkill()
    {
        
    }
}
