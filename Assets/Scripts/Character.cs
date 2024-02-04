using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum CharacterState
{
    Idle,
    Combat,
    Dead
}

public class Character : MonoBehaviour
{
    [Header("Need to assign")]
    [SerializeField]
    private OverlapCircle detectTarget;

    // Stat
    protected Stat stat;
    public Stat Stat { get { return stat; } }

    // State
    private StateMachine stateMachine;
    private Dictionary<CharacterState, IState> dicState =
        new Dictionary<CharacterState, IState>();

    [Header("For check")]
    [SerializeField]
    private bool bIsDead = false;
    public bool IsDead { get { return bIsDead; } }

    // Context
    private CharacterContext characterContext = new CharacterContext();
    public CharacterContext Context { get { return characterContext; } }
    // Combat
    private bool bCanAttack = true;
    public bool IsCanAttack { get { return bCanAttack; } }

    private void Awake()
    {
        if (stat == null)
        {
            stat = gameObject.AddComponent<Stat>();
        }
    }

    protected virtual void Start()
    {
        // Context
        characterContext.SetCharacter(this);

        // 상태
        IState start = new StateStart();
        IState idle = new StateIdle();
        IState combat = new StateCombat();
        IState dead = new StateDead();

        dicState.Add(CharacterState.Idle, idle);
        dicState.Add(CharacterState.Combat, combat);
        dicState.Add(CharacterState.Dead, dead);

        stateMachine = new StateMachine(start);
        stateMachine.SetState(idle, characterContext);

        // 타겟 감지
        detectTarget.OnDetectTarget += OnDetectTarget;
        detectTarget.StartFindTarget();
    }

    protected virtual void Update()
    {
        // 상태 분기
        stateMachine.OperateUpdate(characterContext);
    }

    private void OnDetectTarget(
        object sender, OnDetectTargetEventArgs eventArgs)
    {
        stateMachine.SetState(dicState[CharacterState.Combat], characterContext);
        // OverlapCircle detectTarget에서 탐지할 때마다 add 시도
        characterContext.TryAddAttackTarget(eventArgs.TargetID, eventArgs.Target);
    }

    // Combat
    public void Attack(Character target)
    {
        int damage = stat.CalculAttackDamage();
        target.Attacked(damage, this);

        StartCoroutine("WaitForAttackSpeed", stat.AttackSpeed);
    }

    public void Attacked(int damage, Character attackTarget)
    {
        characterContext.TryAddAttackedTarget(attackTarget.GetInstanceID(), attackTarget);
        if (stat.Attacked(damage))
        {
            Die();
        }
    }

    public void CantFindNextAttackTarget()
    {
        stateMachine.SetState(dicState[CharacterState.Idle], characterContext);
    }

    private void Die()
    {
        // 날 공격하고 있던 대상의 공격 대상에서 지움
        List<Character> attackedTargetIDs = characterContext.GetAttackedTargets();
        int myID = GetInstanceID();
        foreach (Character attacked in attackedTargetIDs)
        {
            attacked.Context.RemoveAttackTarget(myID);
        }

        bIsDead = true;
        stateMachine.SetState(dicState[CharacterState.Dead], characterContext);

        // temp
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        Destroy(detectTarget);

        // 타겟 감지 off
        detectTarget.OnDetectTarget -= OnDetectTarget;
        detectTarget.StopFindTarget();
    }

    IEnumerator WaitForAttackSpeed(float attackSpeed)
    {
        bCanAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        bCanAttack = true;
    }
}
