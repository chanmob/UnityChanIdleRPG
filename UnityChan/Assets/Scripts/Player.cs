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
    public PlayerType playerType = PlayerType.NONE;

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
    public bool isDeath;
    public ItemData item_Weapon; //무기
    public ItemData item_Head; //방어구(머리)
    public ItemData item_Armor; //방어구(갑옷)
    public ItemData item_Boots; //방어구(신발)
    public SkillData skill_first; //스킬1
    public SkillData skill_second; //스킬2
    public SkillData skill_third; //스킬3
}

public class Player : MonoBehaviour
{
    //public PlayerType playerType = PlayerType.NONE;
    
    private MeleeWeaponTrail[] _trails;
    
    public PlayerData playerData;
    
    private NavMeshAgent _agent;
    
    private Animator _animator;

    private Enemy _targetEnemy;

    private Action reviveAct;

    private float _checkDPS;
    private float _attackSpeed;
    
    private bool _isAttack = false;

    public bool IsDeath
    {
        get { return playerData.isDeath; }
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
        SetDamageOnWeapon(false);

        SetAttackSpeed();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(IsDeath)
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

                if (_checkDPS >= _attackSpeed)
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
        
        playerData.curHp -= dmg;

        if (playerData.curHp <= 0)
        {
            Die(bigDmg);
            dieAct?.Invoke();
        }
    }

    private void SetDamageOnWeapon(bool critical = false)
    {
        int len = _trails.Length;
        for (int i = 0; i < len; i++)
        {
            if (critical)
                _trails[i].damage = playerData.damage * 2;
            else
                _trails[i].damage = playerData.damage;
        }
    }

    private void Die(bool bigDamage)
    {      
        if(bigDamage)
            SetAnimationTrigger("Die1");
        else
            SetAnimationTrigger("Die2");
        
        InGameManager.instance.SetDeathTime((int)playerData.playerType);
        reviveAct = ResetPlayer;

        playerData.isDeath = true;
    }

    private void ResetPlayer()
    {
        playerData.isDeath = false;

        _targetEnemy = null;
        _checkDPS = 0;
        playerData.curHp = playerData.hp;
        SetAnimatiorBool("Alive", false);
    }

    private void Attack()
    {
        int cri = Random.Range(1, 101);
        if(playerData.critical >= cri)
        {
            SetDamageOnWeapon(true);
            SetAnimationTrigger("Attack4");
        }

        SetDamageOnWeapon(false);
        int randomAttackAnimation = Random.Range(1, 4);
        string animationTriggerName = "Attack" + randomAttackAnimation;
        SetAnimationTrigger(animationTriggerName);
    }

    private Enemy FindEnemy()
    {
        Enemy targetEnemy = null;
        
        int len = EnemyManager.instance.survivedEnemy.Count;
        float maxDiff = float.MaxValue;
        
        for (int i = 0; i < len; i++)
        {
            Enemy e = EnemyManager.instance.survivedEnemy[i];
            float diff = (transform.position - e.transform.position).sqrMagnitude;
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

    private void SetAttackSpeed()
    {
        _attackSpeed = 1f / playerData.dps;

        if (playerData.dps > 1)
        {
            if (playerData.playerType == PlayerType.DUALSWORD)
            {
                _animator.SetFloat("AttackSpeed", playerData.dps * 2);
            }
            else
            {
                _animator.SetFloat("AttackSpeed", playerData.dps);
            }
        }
        else
        {
            if (playerData.playerType == PlayerType.DUALSWORD)
            {
                _animator.SetFloat("AttackSpeed", 2);
            }
            else
            {
                _animator.SetFloat("AttackSpeed", 1);
            }
        }
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
        
        if(reviveAct != null)
        {
            reviveAct.Invoke();
            reviveAct = null;
        }
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
