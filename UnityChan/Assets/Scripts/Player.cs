using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = UnityEngine.Random;

public enum PlayerType { DUALSWORD, POLEARM, GREATSWORD, NONE }

[Serializable]
public class PlayerData
{
    public int hp; //체력
    public int curHp;
    public int hpRegeneration; //체력재생
    public int damage; //데미지
    public int defense; //방어력
    public int armorPenetration; //방어구 관통력
    public int critical; //치명타
    public float range; //사거리
    public float dps; //공격속도
    public float moveSpeed; //이동속도(뛰기)
    public ItemData item;
}

public class Player : MonoBehaviour
{
    public PlayerType playerType = PlayerType.NONE;
    
    private MeleeWeaponTrail[] _trails;
    
    public PlayerData playerData;
    
    private NavMeshAgent _agent;
    
    private Animator _animator;

    private Enemy _targetEnemy;

    private float _checkDPS;
    
    private bool _isDeath = false;
    private bool _isAttack = false;

    public bool IsDeath
    {
        get { return _isDeath; }
    }

    // Start is called before the first frame update

    private void Awake()
    {
        _trails = GetComponentsInChildren<MeleeWeaponTrail>();
        _agent = GetComponent<NavMeshAgent> ();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        int len = _trails.Length;
        for (int i = 0; i < len; i++)
        {
            _trails[i].damage = playerData.damage;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(_isDeath)
            return;
        
        if (_isAttack)
            return;
        
        _checkDPS += Time.deltaTime;
        _targetEnemy = FindEnemy();

        if(!ReferenceEquals(_targetEnemy, null))
        {
            if (!_targetEnemy.gameObject.activeSelf)
            {
                _targetEnemy = null;
                return;
            }
            
            transform.LookAt(_targetEnemy.transform);
            Vector3 targetEnemyPos = _targetEnemy.transform.position;
            float diff = Vector3.Distance(targetEnemyPos, transform.position);

            if (diff > playerData.range)
            {
                SetAnimatiorBool("Moving", true);
                Vector3 normalizeVec = (targetEnemyPos - transform.position).normalized;
                Vector3 targetVec = targetEnemyPos - (normalizeVec * (playerData.range * 0.9f));

                _agent.SetDestination(targetVec);
            }
            else
            {
                SetAnimatiorBool("Moving", false);

                if (_checkDPS >= playerData.dps)
                {
                    _checkDPS = 0;
                    Attack();
                } 
            }
        }
    }

    public void Revive()
    {
        SetAnimatiorBool("Alive", true);
    }
    
    public void GetDamage(int dmg, Action dieAct = null)
    {
        bool bigDmg = dmg >= playerData.hp * 0.1f;
        
        if (bigDmg)
            SetAnimationTrigger("Hit2");
        else
            SetAnimationTrigger("Hit1");
        
        playerData.hp -= dmg;

        if (playerData.hp <= 0)
        {
            Die(bigDmg);
            dieAct?.Invoke();
        }
    }

    private void Die(bool bigDamage)
    {
        _isDeath = true;
        
        if(bigDamage)
            SetAnimationTrigger("Die1");
        else
            SetAnimationTrigger("Die2");
        
        InGameManager.instance.SetDeathTime((int)playerType);
    }

    private void Attack()
    {
        int randomAttackAnimation = Random.Range(1, 5);
        string animationTriggerName = "Attack" + randomAttackAnimation;
        SetAnimationTrigger(animationTriggerName);
    }

    private Enemy FindEnemy()
    {
        Enemy targetEnemy = null;
        
        int len = EnemyManager.instance.survivedEnemy.Count;
        float diff = 0;
        float maxDiff = float.MaxValue;
        
        for (int i = 0; i < len; i++)
        {
            Enemy e = EnemyManager.instance.survivedEnemy[i];
            diff = (transform.position - e.transform.position).sqrMagnitude;
            if (diff < maxDiff)
            {
                maxDiff = diff;
                targetEnemy = e;
            }
        }
        
        return targetEnemy;
    }

    private void KillEnemy()
    {
        _targetEnemy = null;
    }
    
    private void SetAnimatiorBool(string animationName, bool value)
    {
        _animator.SetBool(animationName, value);
    }

    private void SetAnimationTrigger(string animationName)
    {
        _animator.SetTrigger(animationName);   
    }
    
    //공격 애니메이션 트레일
    public void LeftHandleTrailOn()
    {
        _trails[1].Emit = false;
        _trails[1].HitEnd();
        
        _trails[0].Emit = true;
        _trails[0].HitStart();
        
        _isAttack = true;
    }
    
    public void RightHandleTrailOn()
    {
        _trails[0].Emit = false;
        _trails[0].HitEnd();
        
        _trails[1].Emit = true;
        _trails[1].HitStart();
    
        _isAttack = true;
    }

    public void AllTrailOff()
    {
        int len = _trails.Length;
        for (int i = 0; i < len; i++)
        {
            _trails[i].Emit = false;
            _trails[i].HitEnd();
        }

        _isAttack = false;
    }

    public void AllTrailOn()
    {
        int len = _trails.Length;
        for (int i = 0; i < len; i++)
        {
            _trails[i].Emit = true;
            _trails[i].HitStart();        
        }

        _isAttack = true;
    }
    //공격 애니메이션 트레일
}
