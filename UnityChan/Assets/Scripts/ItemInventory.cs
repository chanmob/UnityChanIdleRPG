using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private List<Item> _list_Items;

    private void Awake()
    {
        _list_Items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        _list_Items.Add(item);
    }

    public void RefreshInventory()
    {
        int len = _list_Items.Count;

        for (int i = 0; i < len; i++)
        {
            
        }
    }
}
