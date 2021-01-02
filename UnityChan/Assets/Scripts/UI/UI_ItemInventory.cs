using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ItemInventory : MonoBehaviour
{
    private List<Item> _list_Items;

    [SerializeField]
    private Transform _content;

    [SerializeField]
    private GameObject _itemPrefab;
    
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
        int childCnt = _content.childCount;
        int len = _list_Items.Count;

        if (childCnt < len)
        {
            for (int i = 0; i < (len - childCnt); i++)
            {
                GameObject newItem = Instantiate(_itemPrefab);
                newItem.transform.SetParent(_content, false);
            }
        }

        for (int i = 0; i < len; i++)
        {
            
        }
    }
    
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}