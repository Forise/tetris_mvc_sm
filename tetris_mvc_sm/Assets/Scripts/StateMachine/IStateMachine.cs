public interface IStateMachine
{
    State CurrentState { get; }
    State[] States { get; }
    int CurrentStateIndex { get; }
    void Update();
    void CreateStates(int statesCount);
    void CreateStates(System.Type statesEnum);
    void InitializeState(int stateIndex, State.StateHandler init, State.StateHandler update, State.StateHandler close);
    void InitializeMachine();
    void SetState(int index);
}