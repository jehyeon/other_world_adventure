using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    // Stat
    protected Stat currentStat;

    // State
    public IState currentState;

    public StateMachine(IState defaultState)
    {
        currentState = defaultState;
    }

    public void SetState(IState state, CharacterContext context)
    {
        if (currentState == state)
        {
            return;
        }

        currentState.OperateExit(context);

        currentState = state;

        currentState.OperateEnter(context);
    }

    public void OperateUpdate()
    {
        currentState.OperateUpdate();
    }
}
