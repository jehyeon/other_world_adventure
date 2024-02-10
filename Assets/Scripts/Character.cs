using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum Camp
{
    Human
}

public enum Job
{
    SwordMan,
    Archer,
    Wizard,
    Healer
}

public enum CharacterState
{
    Idle,
    Combat,
    Dead
}

[RequireComponent(typeof(Stat), typeof(SpriteRenderer), typeof(Animator))]
public class Character : MonoBehaviour
{
    // Stat
    protected Stat stat;
    public Stat Stat { get { return stat; } }

    // State
    private StateMachine stateMachine;
    private Dictionary<CharacterState, IState> dicState =
        new Dictionary<CharacterState, IState>();

    // Renderer & Animator
    protected SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }
    protected Animator animator;
    public Animator Animator { get { return animator; } }

    // Detect
    [Header("Require")]
    [SerializeField]
    private OverlapCircle detectTarget;

    [Header("For check")]
    [SerializeField]
    private bool bIsDead = false;
    public bool IsDead { get { return bIsDead; } }

    // Context
    private CharacterContext characterContext = new CharacterContext();
    public CharacterContext Context { get { return characterContext; } }

    // Move
    private Vector3 destination;
    public Vector3 Destination { get { return destination; } }

    // Combat
    private bool bCanAttack = true;
    public bool IsCanAttack { get { return bCanAttack; } }

    protected void Awake()
    {
        stat = GetComponent<Stat>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        detectTarget = GetComponent<OverlapCircle>();

        SetDest(transform.position);
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

    /// <summary>
    /// 목적지 지정, 파티 포메이션 이동을 위해 사용
    /// </summary>
    /// <param name="characterDest"></param>
    public void SetDest(Vector3 characterDest)
    {
        destination = characterDest;
    }

    // ////////////////////////////////////////////////// //
    // Combat                                             //
    // ////////////////////////////////////////////////// //
    public void Attack(Character target)
    {
        int damage = stat.CalculAttackDamage();
        target.Attacked(damage, this);
        animator.SetTrigger("Attack");

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

    // ////////////////////////////////////////////////// //
    // Detect                                             //
    // ////////////////////////////////////////////////// //
    private void OnDetectTarget(
        object sender, OnDetectTargetEventArgs eventArgs)
    {
        stateMachine.SetState(dicState[CharacterState.Combat], characterContext);
        // OverlapCircle detectTarget에서 탐지할 때마다 add 시도
        characterContext.TryAddAttackTarget(eventArgs.TargetID, eventArgs.Target);
    }
}
