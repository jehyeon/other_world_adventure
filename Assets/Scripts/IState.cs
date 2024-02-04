using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OperateEnter(CharacterContext context);
    public void OperateExit(CharacterContext context);
    public void OperateUpdate(CharacterContext context);
}

public class StateStart : IState
{
    void IState.OperateEnter(CharacterContext context)
    {
    }

    void IState.OperateExit(CharacterContext context)
    {
    }

    void IState.OperateUpdate(CharacterContext context)
    {
    }
}

public class StateIdle : IState
{
    public void OperateEnter(CharacterContext context)
    {
        // Idle 상태 진입 시, 타겟 정보 초기화
        context.ClearTargetsInfo();
    }

    public void OperateExit(CharacterContext context)
    {
    }

    public void OperateUpdate(CharacterContext context)
    {
        // TODO: Idle update
        //Debug.Log(character.transform.position);
    }
}

public class StateCombat : IState
{
    public void OperateEnter(CharacterContext context)
    {
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

        Vector3 dir = currentTarget.transform.position - context.Character.transform.position;

        // 공격 사거리 밖 타겟이 있을 경우
        if (dir.sqrMagnitude > context.Character.Stat.AttackRange)
        {
            // 공격 사거리까지 이동
            context.Character.transform.position +=
                dir.normalized * Time.deltaTime * context.Character.Stat.MoveSpeed;
        }
        else
        {
            if (context.Character.IsCanAttack)
            {
                // 공격
                // TODO: 직업에 따라 공격 스타일 분기를 어떻게 해야할 지 고민 필요
                context.Character.Attack(currentTarget);
            }
        }
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
