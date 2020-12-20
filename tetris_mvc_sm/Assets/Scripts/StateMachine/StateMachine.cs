using UnityEngine;

public abstract class StateMachine
{
    #region Fields
    private int _currentStateIndex;
    private State _currentState;
    private State[] _states;
    #endregion

    #region Properties
    protected State CurrentState
    {
        get { return _currentState; }
        private set
        {
            _currentState = value;
            if (_currentState != null)
                _currentState.Init();
            else
                Debug.LogWarning("[" + GetType() + "] " + "New state is null");
        }
    }

    protected int CurrentStateIndex
    {
        get { return _currentStateIndex; }
        private set
        {
            if (_currentState != null)
                _currentState.Close();
            _currentStateIndex = value;
            CurrentState = _states[_currentStateIndex];
        }
    }
    #endregion Properties

    #region Methods
    protected virtual void SetState(int index)
    {
        if (index >= 0 && index < _states.Length)
            CurrentStateIndex = index;
    }

    protected void Update()
    {
        if (CurrentState != null)
            CurrentState.Update();
    }

    protected void CreateStates(int statesCount)
    {
        _states = new State[statesCount];
    }

    protected void CreateStates(System.Type statesEnum)
    {
        _states = new State[System.Enum.GetValues(statesEnum).Length];
    }

    protected void InitializeState(int stateIndex, State.StateHandler init, State.StateHandler update, State.StateHandler close)
    {
        if (stateIndex >= _states.Length)
            Debug.LogError($"[{this.GetType()}] InitializeState: index of state {stateIndex} is out of range!");
        _states[stateIndex] = new State(init, update, close);
    }

    protected abstract void InitializeMachine();
    #endregion Methods
}