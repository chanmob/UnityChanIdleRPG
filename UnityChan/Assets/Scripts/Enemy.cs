using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

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
    
    private Animator _animator;

    private float _checkEnemyDPS = 0;
    private float _checkEnemyWander = 0;
    public float wanderRadius;
    public float wanderTimer;

    private NavMeshAgent _agent;
    
    [SerializeField]
    private Player _targetPlayer = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(_targetPlayer == null)
            _targetPlayer = FindPlayer();
        
        if (_targetPlayer != null)
        {
            Vector3 targetPlayerPos = _targetPlayer.transform.position;
            float diff = Vector3.Distance(targetPlayerPos, transform.position);

            if (diff > enemyData.range)
            {
                Vector3 normalizeVec = (_targetPlayer.transform.position - transform.position).normalized;
                Vector3 targetVec = targetPlayerPos - (normalizeVec * enemyData.range);
                
                _agent.SetDestination(targetVec);
            }
            else
            {
                _checkEnemyDPS += Time.deltaTime;

                if (_checkEnemyDPS >= enemyData.dps)
                {
                    _checkEnemyDPS = 0;
                }   
            }
        }
        
        else
        {
            if (_agent.velocity != Vector3.zero)
            {
                SetAnimatiorBool("Walk", true);
            }
            else
            {
                SetAnimatiorBool("Walk", false);
            }
            
            _checkEnemyWander += Time.deltaTime;

            if (_checkEnemyWander >= wanderTimer)
            {
                _agent.speed = 1;
                _checkEnemyWander = 0;
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                _agent.SetDestination(newPos);
            }
        }
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
        for (int i = (int) PlayerType.DUALSWORD; i < (int) PlayerType.NONE; i++)
        {
            if (Vector3.Distance(PlayerManager.instance.players[i].transform.position, transform.position) <=
                enemyData.chaseRange)
            {
                return PlayerManager.instance.players[i];
            }
        }
        
        return null;
    }
    
    
    public void SetAnimatiorBool(string animationName, bool value)
    {
        _animator.SetBool(animationName, value);
    }

    public void SetAnimationTrigger(string animationName)
    {
        _animator.SetTrigger(animationName);   
    }
}
