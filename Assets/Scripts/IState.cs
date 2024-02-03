using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OperateEnter(CharacterContext context);
    public void OperateExit(CharacterContext context);
    public void OperateUpdate();
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

    public void OperateUpdate()
    {
        // TODO: Idle update
    }
}

public class StateCombat : IState
{
    protected Stat currentStat;

    public void OperateEnter(CharacterContext context)
    {
        currentStat = context.CurrentStat;
    }

    public void OperateExit(CharacterContext context)
    {
        // 전투 종료 시, 타겟 정보 초기화
        context.ClearTargetsInfo();
        currentStat = null;
    }

    public void OperateUpdate()
    {
        // TODO: 전투 로직 추가
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

    public void OperateUpdate()
    {
        // TODO: Dead update 로직
    }
}
