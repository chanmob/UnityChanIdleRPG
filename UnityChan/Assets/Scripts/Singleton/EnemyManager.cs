using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<Enemy> survivedEnemy;

    protected override void OnAwake()
    {
        base.OnAwake();
        survivedEnemy = new List<Enemy>();
    }
}
