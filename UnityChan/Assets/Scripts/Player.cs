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
    public int hpRegeneration; //체력재생
    public int damage; //데미지
    public int defense; //방어력
    public int armorPenetration; //방어구 관통력
    public int critical; //치명타
    public float range; //사거리
    public float dps; //공격속도
    public float moveSpeed; //이동속도(뛰기)
    public float walkSpeed; //이동속도(걷기)
}

public class Player : MonoBehaviour
{
    private MeleeWeaponTrail[] _trails;
    
    public PlayerData playerData;
    
    private NavMeshAgent _agent;
    private Animator _animtor;
    
    public float wanderRadius;
    public float wanderTimer;

    private float timer;

    // Start is called before the first frame update

    private void Awake()
    {
        _trails = GetComponentsInChildren<MeleeWeaponTrail>();
        _agent = GetComponent<NavMeshAgent> ();
        _animtor = GetComponent<Animator>();
    }

    void Start()
    {

        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _animtor.SetTrigger("Attack1");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _animtor.SetTrigger("Attack2");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _animtor.SetTrigger("Attack3");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            _animtor.SetTrigger("Attack4");
        }
        
        
        
        if (_agent.velocity != Vector3.zero)
        {
            _animtor.SetBool("Moving", true);
        }
        else
        {
            _animtor.SetBool("Moving", false);
        }
        
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Attack();
            // Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            // _agent.SetDestination(newPos);
             timer = 0;
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }

    private void Attack()
    {
        int randomAttackAnimation = Random.Range(1, 5);
        string animationTriggerName = "Attack" + randomAttackAnimation.ToString();
        _animtor.SetTrigger(animationTriggerName);
    }
    
    //공격 애니메이션 트레일
    public void LeftHandleTrailOn()
    {
        _trails[1].Emit = false;
        _trails[1].HitEnd();
        
        _trails[0].Emit = true;
        _trails[0].HitStart();
    }
    
    public void RightHandleTrailOn()
    {
        _trails[0].Emit = false;
        _trails[0].HitEnd();
        
        _trails[1].Emit = true;
        _trails[1].HitStart();
    }

    public void AllTrailOff()
    {
        int len = _trails.Length;
        for (int i = 0; i < len; i++)
        {
            _trails[i].Emit = false;
            _trails[i].HitEnd();
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
    }
    //공격 애니메이션 트레일
}
