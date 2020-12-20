public abstract class CycledStateMachine : BaseSateMachine
{
    protected virtual void SetNextState()
    {
        int nextStateIndex = CurrentStateIndex + 1;
        if(nextStateIndex >= States.Length)
        {
            nextStateIndex = 0;
        }
        SetState(nextStateIndex);
    }

    protected virtual void SetPreviousState()
    {
        int nextStateIndex = CurrentStateIndex - 1;
        if (nextStateIndex < 0)
        {
            nextStateIndex = States.Length - 1;
        }
        SetState(nextStateIndex);
    }
}
