using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInventory : MonoBehaviour
{
    private List<Skill> _list_Skills;

    private void Awake()
    {
        _list_Skills = new List<Skill>();
    }

    public void AddItem(Skill skill)
    {
        _list_Skills.Add(skill);
    }

    public void RefreshInventory()
    {
        int len = _list_Skills.Count;

        for (int i = 0; i < len; i++)
        {
            
        }
    }
}
