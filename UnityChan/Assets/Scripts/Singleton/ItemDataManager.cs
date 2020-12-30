using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemDataManager : Singleton<ItemDataManager>
{
    private const string itemDataPath = "Items";
    private const string normalItemPath = "/Normal";
    private const string rareItemPath = "/Rare";
    private const string epicItemPath = "/Epic";
    private const string uniqueItemPath = "/Unique";
    private const string legendaryItemPath = "/Legendary";

    private Dictionary<string, ItemData> normalItems;
    private Dictionary<string, ItemData> rareItems;
    private Dictionary<string, ItemData> epicItems;
    private Dictionary<string, ItemData> uniqueItems;
    private Dictionary<string, ItemData> legendaryItems;

    private int normalItemCount = 0;
    private int rareItemCount = 0;
    private int epicItemCount = 0;
    private int uniqueItemCount = 0;
    private int legendaryItemCount = 0;

    protected override void OnAwake()
    {
        base.OnAwake();

        #region 아이템 로드

        normalItems = new Dictionary<string, ItemData>();

        ItemData[] normalItemDatas = Resources.LoadAll<ItemData>(itemDataPath + normalItemPath);
        int normalItemLength = normalItemDatas.Length;

        for (int i = 0; i < normalItemLength; i++)
        {
            ItemData data = normalItemDatas[i];
            string itemName = data.name;

            if (normalItems.ContainsKey(itemName) == false)
            {
                normalItems.Add(itemName, data);
            }
        }

        normalItemCount = normalItems.Count;



        rareItems = new Dictionary<string, ItemData>();

        ItemData[] rareItemDatas = Resources.LoadAll<ItemData>(itemDataPath + rareItemPath);
        int rareItemLength = rareItemDatas.Length;

        for (int i = 0; i < rareItemLength; i++)
        {
            ItemData data = rareItemDatas[i];
            string itemName = data.name;

            if (rareItems.ContainsKey(itemName) == false)
            {
                rareItems.Add(itemName, data);
            }
        }

        rareItemCount = rareItems.Count;



        epicItems = new Dictionary<string, ItemData>();

        ItemData[] epicItemDatas = Resources.LoadAll<ItemData>(itemDataPath + epicItemPath);
        int epicItemLength = epicItemDatas.Length;

        for (int i = 0; i < epicItemLength; i++)
        {
            ItemData data = epicItemDatas[i];
            string itemName = data.name;

            if (epicItems.ContainsKey(itemName) == false)
            {
                epicItems.Add(itemName, data);
            }
        }

        epicItemCount = epicItems.Count;



        uniqueItems = new Dictionary<string, ItemData>();

        ItemData[] uniqueItemDatas = Resources.LoadAll<ItemData>(itemDataPath + uniqueItemPath);
        int uniqueItemLength = uniqueItemDatas.Length;

        for (int i = 0; i < uniqueItemLength; i++)
        {
            ItemData data = uniqueItemDatas[i];
            string itemName = data.name;

            if (uniqueItems.ContainsKey(itemName) == false)
            {
                uniqueItems.Add(itemName, data);
            }
        }

        uniqueItemCount = uniqueItems.Count;



        legendaryItems = new Dictionary<string, ItemData>();

        ItemData[] legendaryItemDatas = Resources.LoadAll<ItemData>(itemDataPath + legendaryItemPath);
        int legendaryItemLength = uniqueItemDatas.Length;

        for (int i = 0; i < legendaryItemLength; i++)
        {
            ItemData data = legendaryItemDatas[i];
            string itemName = data.name;

            if (legendaryItems.ContainsKey(itemName) == false)
            {
                legendaryItems.Add(itemName, data);
            }
        }

        legendaryItemCount = legendaryItems.Count;

        #endregion

        Debug.Log("Normal : " + normalItemCount + " / Rare : " + rareItemCount + " / Epic : " + epicItemCount + " / Unique : " + uniqueItemCount + " / Legendary : " + legendaryItemCount);
    }

    public ItemData GetNormalItem(ItemData.Tier tier, string itemKey)
    {
        switch (tier)
        {
            case ItemData.Tier.NORMAL:
                if (normalItems.ContainsKey(itemKey))
                {
                    return normalItems[itemKey];
                }
                break;
            case ItemData.Tier.RARE:
                if (rareItems.ContainsKey(itemKey))
                {
                    return rareItems[itemKey];
                }
                break;
            case ItemData.Tier.EPIC:
                if (epicItems.ContainsKey(itemKey))
                {
                    return epicItems[itemKey];
                }
                break;
            case ItemData.Tier.UNIQUE:
                if (uniqueItems.ContainsKey(itemKey))
                {
                    return uniqueItems[itemKey];
                }
                break;
            case ItemData.Tier.LEGENDARY:
                if (legendaryItems.ContainsKey(itemKey))
                {
                    return legendaryItems[itemKey];
                }
                break;
        }
        return null;
    }

    public ItemData GetRandomNormalItem()
    {
        int randomIdx = Random.Range(0, normalItemCount);

        var randomItemPair = normalItems.ElementAt(randomIdx);

        string key = randomItemPair.Key;

        if (normalItems.ContainsKey(key))
        {
            return normalItems[key];
        }
        else
        {
            return null;

        }
    }

    public ItemData GetRandomRareItem()
    {
        int randomIdx = Random.Range(0, rareItemCount);

        var randomItemPair = rareItems.ElementAt(randomIdx);

        string key = randomItemPair.Key;

        if (rareItems.ContainsKey(key))
        {
            return rareItems[key];
        }
        else
        {
            return null;

        }
    }

    public ItemData GetRandomEpicItem()
    {
        int randomIdx = Random.Range(0, epicItemCount);

        var randomItemPair = epicItems.ElementAt(randomIdx);

        string key = randomItemPair.Key;

        if (epicItems.ContainsKey(key))
        {
            return epicItems[key];
        }
        else
        {
            return null;

        }
    }

    public ItemData GetRandomUniqueItem()
    {
        int randomIdx = Random.Range(0, uniqueItemCount);

        var randomItemPair = uniqueItems.ElementAt(randomIdx);

        string key = randomItemPair.Key;

        if (uniqueItems.ContainsKey(key))
        {
            return uniqueItems[key];
        }
        else
        {
            return null;

        }
    }

    public ItemData GetRandomLegendaryItem()
    {
        int randomIdx = Random.Range(0, legendaryItemCount);

        var randomItemPair = legendaryItems.ElementAt(randomIdx);

        string key = randomItemPair.Key;

        if (legendaryItems.ContainsKey(key))
        {
            return legendaryItems[key];
        }
        else
        {
            return null;

        }
    }
}