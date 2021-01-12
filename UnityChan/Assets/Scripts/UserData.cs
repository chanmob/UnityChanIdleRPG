using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserData
{
    public int level = 1;
    public int coin;
    public int gem;
    public int curExp;
    public int needExp = 100;

    public void AddExp(int exp)
    {
        curExp += exp;

        if (curExp >= needExp)
        {
            level++;
            int diff = curExp - needExp;
            curExp = diff;
            needExp += (int)(needExp * 1.2f);
        }
    }

    public void AddCoin(int getCoin)
    {
        coin += getCoin;
    }

    public void AddGem(int getGem)
    {
        gem += getGem;
    }

    public void SaveData()
    {
        int len = PlayerManager.instance.players.Length;

        for(int i = 0; i < len; i++)
        {
            PlayerManager.instance.players[i].SavePlayerData();
        }

        string jsonData = JsonUtility.ToJson(this);
        string path = Application.dataPath + "/Resources/Datas/UserData.json";
        File.WriteAllText(path, jsonData);
    }
}
