using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OperateEnter(CharacterContext context);
    public void OperateExit(CharacterContext context);
    public void OperateUpdate(CharacterContext context);
}

/// <summary>
/// Default, state 변화 시 Enter, Exit 동작을 위한 초기화 용도
/// </summary>
public class StateStart : IState
{
    void IState.OperateEnter(CharacterContext context) { }
    void IState.OperateExit(CharacterContext context) { }
    void IState.OperateUpdate(CharacterContext context) { }
}

public class StateIdle : IState
{
    private Character character;
    private Vector3 dir;

    public void OperateEnter(CharacterContext context)
    {
        character = context.Character;
        // Idle 상태 진입 시, 타겟 정보 초기화
        context.ClearTargetsInfo();
    }

    public void OperateExit(CharacterContext context)
    {
        MoveToDestination();
    }

    public void OperateUpdate(CharacterContext context)
    {
        //context.Character.Move();
        MoveToDestination();
    }

    /// <summary>
    /// 목적지까지 이동, 방향에 따라 렌더러 반전
    /// </summary>
    private void MoveToDestination()
    {
        dir = character.Destination - character.transform.position;

        // 방향에 따라 좌우반전
        character.SpriteRenderer.flipX = dir.x > Managers.Instance.DiffForRenderFlip
            ? false
            : true;

        if (dir.sqrMagnitude > Managers.Instance.DiffFromDest)
        {
            character.transform.position += dir.normalized * Time.deltaTime * character.Stat.MoveSpeed;
            character.Animator.SetBool("isMoved", true);
        }
        else
        {
            character.Animator.SetBool("isMoved", false);
        }
    }
}

/// <summary>
/// 전투 state
/// </summary>
public class StateCombat : IState
{
    private Character character;

    public void OperateEnter(CharacterContext context)
    {
        character = context.Character;
    }

    public void OperateExit(CharacterContext context)
    {
        // 전투 종료 시, 타겟 정보 초기화
        context.ClearTargetsInfo();
    }

    public void OperateUpdate(CharacterContext context)
    {
        Character currentTarget;
        if (!context.TryGetNextTarget(out currentTarget))
        {
            // 현재 타겟이 없고, 다음 타겟도 없는 경우, 잉여 동작
            // TODO: 파티 단위로 state 변화하거나,
            // idle 상태에서 수행하는 다른 로직이 추가될 듯
            Debug.Log("더 이상 다른 대상이 없음");
            context.Character.CantFindNextAttackTarget();

            return;
        }

        // 공격 사거리 밖 타겟이 있을 경우
        //if (context.Character.MoveToTargetForAttack(currentTarget.transform.position))
        //{
        //    if (context.Character.IsCanAttack)
        //    {
        //        // 공격
        //        // TODO: 직업에 따라 공격 스타일 분기를 어떻게 해야할 지 고민 필요
        //        context.Character.Attack(currentTarget);
        //    }
        //}
        if (MoveToTargetForAttack(currentTarget.transform.position))
        {
            if (context.Character.IsCanAttack)
            {
                // 공격
                // TODO: 직업에 따라 공격 스타일 분기를 어떻게 해야할 지 고민 필요
                context.Character.Attack(currentTarget);
            }
        }
    }

    private bool MoveToTargetForAttack(Vector3 targetPos)
    {
        Vector3 dir = targetPos - character.transform.position;

        // 공격 사거리 안인 경우
        if (dir.sqrMagnitude < character.Stat.AttackRange)
        {
            // 타겟 방향으로 더 이동하지 않음
            character.Animator.SetBool("isMoved", false);
            character.SetDest(character.transform.position);

            return true;
        }

        // 방향에 따라 좌우반전
        character.SpriteRenderer.flipX = dir.x > Managers.Instance.DiffForRenderFlip
            ? false
            : true;

        // 이동
        character.transform.position += dir.normalized * Time.deltaTime * character.Stat.MoveSpeed;
        character.Animator.SetBool("isMoved", true);

        return false;
    }
}

public class StateDead : IState
{
    public void OperateEnter(CharacterContext context)
    {
        context.ClearTargetsInfo();
    }

    public void OperateExit(CharacterContext context)
    {
        // TODO: Dead Exit 로직
    }

    public void OperateUpdate(CharacterContext context)
    {
        // TODO: Dead update 로직
    }
}