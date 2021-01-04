using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public enum EnemyType {SLIME, TURTLESHELL, NONE}

[Serializable]
public class EnemyData
{
    public int hp; //체력
    public int damage; //데미지
    public int defense; //방어력
    public int armorPenetration; //방어구 관통력
    public float chaseRange;
    public float range; //사거리
    public float dps; //공격속도
    public float moveSpeed; //이동속도(뛰기)
    public float walkSpeed; //이동속도(걷기)
}


public class Enemy : MonoBehaviour
{
    public EnemyType enemyType = EnemyType.NONE;

    public EnemyData enemyData;
    
    private NavMeshAgent _agent;
    
    private Animator _animator;

    [SerializeField]
    private Player _targetPlayer = null;
    
    [SerializeField]
    private float _checkEnemyDPS = 0;
    [SerializeField]
    private float _wanderRadius;
    [SerializeField]
    private float _wanderTimer;
    private float _checkEnemyWander = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _targetPlayer = FindPlayer();
        
        if (ReferenceEquals(_targetPlayer, null))
        {
            SetAnimatiorBool("Battle", false);
            
            //_targetPlayer = FindPlayer();
            
            if (_agent.velocity != Vector3.zero)
            {
                SetAnimatiorBool("Run", false);
                SetAnimatiorBool("Walk", true);
            }
            else
            {
                SetAnimatiorBool("Walk", false);
            }
            
            _checkEnemyWander += Time.deltaTime;

            if (_checkEnemyWander >= _wanderTimer)
            {
                _agent.speed = enemyData.walkSpeed;
                _checkEnemyWander = 0;
                Vector3 newPos = RandomNavSphere(transform.position, _wanderRadius, -1);
                _agent.SetDestination(newPos);
            }
        }
        else
        {
            transform.LookAt(_targetPlayer.transform);
            SetAnimatiorBool("Battle", true);
            
            Vector3 targetPlayerPos = _targetPlayer.transform.position;
            float diff = Vector3.Distance(targetPlayerPos, transform.position);

            if (diff > enemyData.range)
            {
                if (_agent.velocity != Vector3.zero)
                {
                    SetAnimatiorBool("Walk", false);
                    SetAnimatiorBool("Run", true);
                }

                _agent.speed = enemyData.moveSpeed;
                
                Vector3 normalizeVec = (targetPlayerPos - transform.position).normalized;
                Vector3 targetVec = targetPlayerPos - (normalizeVec * (enemyData.range * 0.9f));

                _agent.SetDestination(targetVec);
            }
            else
            {
                SetAnimatiorBool("Run", false);

                _checkEnemyDPS += Time.deltaTime;

                if (_checkEnemyDPS >= enemyData.dps)
                {
                    _checkEnemyDPS = 0;
                    Attack();
                }   
            }
        }
    }

    public void GetDamage(int dmg, Action dieAct = null)
    {
        SetAnimationTrigger("Hit");
        enemyData.hp -= dmg;

        if (enemyData.hp <= 0)
        {
            Die();
            dieAct?.Invoke();
        }
    }

    private void Die()
    {
        EnemyManager.instance.survivedEnemy.Remove(this);
        ObjectPoolManager.instance.ReturnEnemy(this);
    }
    
    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
    
    private Player FindPlayer()
    {
        Player target = null;
        
        float maxDiff = float.MaxValue;

        for (int i = (int) PlayerType.DUALSWORD; i < (int) PlayerType.NONE; i++)
        {
            Player p = PlayerManager.instance.players[i];

            if (p.IsDeath)
            {
                break;
            }

            float diff = Vector3.Distance(p.transform.position, transform.position);
            if (diff < maxDiff)
            {
                maxDiff = diff;
                target = p;
            }
        }

        if (maxDiff <= enemyData.chaseRange)
        {
            return target;
        }
        
        return null;
    }

    private void Attack()
    {
        int randomAttackAnimation = UnityEngine.Random.Range(1, 3);
        string animationAniamtioName = "Attack" + randomAttackAnimation;

        SetAnimationTrigger(animationAniamtioName);
        _targetPlayer.GetDamage(enemyData.damage);
    }

    private void SetAnimatiorBool(string animationName, bool value)
    {
        _animator.SetBool(animationName, value);
    }

    private void SetAnimationTrigger(string animationName)
    {
        _animator.SetTrigger(animationName);   
    }
}
