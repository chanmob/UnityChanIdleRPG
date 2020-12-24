using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [Header("Slime")]
    //Slime
    [SerializeField]
    private Enemy slimeEnemyPrefab;
    private Stack<Enemy> _stack_SlimeEnemy;
    //Slime
    
    [Header("Turtle Shell")]
    //TurtleShell
    [SerializeField]
    private Enemy turtleShellEnemyPrefab;
    private Stack<Enemy> _stack_TurtleShellEnemy;
    //TurtleShell
    
    [SerializeField]
    private Transform _enemyParent;

    protected override void OnAwake()
    {
        base.OnAwake();

        _stack_SlimeEnemy = new Stack<Enemy>();
        _stack_TurtleShellEnemy = new Stack<Enemy>();
    }

    public Enemy GetEnemy(EnemyType type)
    {
        Enemy newEnemy = null;
        int len = 0;
        
        switch (type)
        {
            case EnemyType.SLIME:
                len = _stack_SlimeEnemy.Count;
                
                if(len == 0)
                    MakeEnemy(type, 1);

                newEnemy = _stack_SlimeEnemy.Pop();
                break;
            case EnemyType.TURTLESHELL:
                len = _stack_TurtleShellEnemy.Count;
                
                if(len == 0)
                    MakeEnemy(type, 1);

                newEnemy = _stack_SlimeEnemy.Pop();
                break;
        }
        
        return newEnemy;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        switch (enemy.enemyType)
        {
            case EnemyType.SLIME:
                _stack_SlimeEnemy.Push(enemy);
                break;
            case EnemyType.TURTLESHELL:
                _stack_TurtleShellEnemy.Push(enemy);
                break;
        }
        
        if(enemy.gameObject.activeSelf)
            enemy.gameObject.SetActive(false);
    }
    private void MakeEnemy(EnemyType type, int count = 0)
    {
        Enemy newEnemy = null;

        for (int i = 0; i < count; i++)
        {
            switch (type)
            {
                case EnemyType.SLIME:
                    newEnemy = Instantiate(slimeEnemyPrefab);
                    _stack_SlimeEnemy.Push(newEnemy);
                    break;
                case EnemyType.TURTLESHELL:
                    newEnemy = Instantiate(turtleShellEnemyPrefab);
                    _stack_SlimeEnemy.Push(newEnemy);
                    break;
            }
            
            newEnemy.transform.SetParent(_enemyParent);
            newEnemy.gameObject.SetActive(false);
        }
    }
}
