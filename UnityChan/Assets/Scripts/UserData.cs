using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public int coin;
    public int gem;
    public int curExp;
    public int needExp = 100;

    public void AddExp(int exp)
    {
        curExp += exp;

        if (curExp >= needExp)
        {
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
}
