using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<Enemy> survivedEnemy;

    protected override void OnAwake()
    {
        base.OnAwake();
        survivedEnemy = new List<Enemy>();
    }

    private void Start()
    {
        StartCoroutine(EnemySpawnCoroutine());
    }

    private IEnumerator EnemySpawnCoroutine()
    {
        while ((true))
        {
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < 3; i++)
            {
                var e = ObjectPoolManager.instance.GetEnemy(EnemyType.SLIME);
                var ran = Random.insideUnitCircle * 10f;
                e.transform.position = new Vector3(ran.x, 0, ran.y);
                e.gameObject.SetActive(true);
                survivedEnemy.Add(e);
            }
        }
    }
    
}
