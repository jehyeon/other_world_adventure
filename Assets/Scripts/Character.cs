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

    // State
    private StateMachine stateMachine;
    private Dictionary<CharacterState, IState> dicState =
        new Dictionary<CharacterState, IState>();

    // Context
    private CharacterContext characterContext = new CharacterContext();

    public int ID
    {
        set { }
        get
        {
            if (ID == 0)
            {
                ID = gameObject.GetInstanceID();
            }

            return ID;
        }
    }

    private void Awake()
    {
        if (stat == null)
        {
            stat = gameObject.AddComponent<Stat>();
        }
    }

    protected void Start()
    {
        // temp 임시 스탯 로드
        Managers.Instance.Stat.Load(stat, "test");

        // 상태
        IState idle = new StateIdle();
        IState combat = new StateIdle();
        IState dead = new StateDead();

        dicState.Add(CharacterState.Idle, idle);
        dicState.Add(CharacterState.Combat, combat);
        dicState.Add(CharacterState.Dead, dead);

        stateMachine = new StateMachine(idle);

        // Context
        characterContext.SetStat(stat);

        // 타겟 감지
        detectTarget.OnDetectTarget += OnDetectTarget;
        detectTarget.StartFindTarget();
    }

    protected void Update()
    {
        // temp
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(h, v);
        dir = dir.normalized;
        transform.position += (Vector3)dir * Time.deltaTime * stat.MoveSpeed;

        // 상태 분기
        stateMachine.OperateUpdate();
    }

    private void OnDetectTarget(
        object sender, OnDetectTargetEventArgs eventArgs)
    {
        // OverlapCircle detectTarget에서 탐지할 때마다 add 시도
        characterContext.TryAddAttackTarget(eventArgs.TargetID, eventArgs.Target);
    }
}
