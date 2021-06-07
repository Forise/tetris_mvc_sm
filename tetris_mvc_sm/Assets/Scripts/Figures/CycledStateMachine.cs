//has to be not in Figures folder﻿
public abstract class CycledStateMachine : BaseSateMachine
{
    protected virtual void SetNextState()
    {
        int nextStateIndex = CurrentStateIndex + 1;
        SetState(nextStateIndex >= States.Length ? nextStateIndex : 0);
    }

    protected virtual void SetPreviousState()
    {
        int nextStateIndex = CurrentStateIndex - 1;
        SetState(nextStateIndex < 0 ? States.Length - 1 : nextStateIndex);
    }
}
