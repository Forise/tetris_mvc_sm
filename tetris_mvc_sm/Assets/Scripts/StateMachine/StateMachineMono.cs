using UnityEngine;
public abstract class StateMachineMono : MonoBehaviour, IStateMachine
{
    #region Fields
    private int currentStateIndex = 0;
    private State currentState;
    private State[] _states;
    #endregion Field

    #region Properties
    public State[] States => _states;
    public State CurrentState
    {
        get { return currentState; }
        private set
        {
            currentState = value;
            if (currentState != null)
                currentState.Init();
            else
                Debug.LogWarning("[" + GetType() + "] " + "New state is null");
        }
    }

    public int CurrentStateIndex
    {
        get { return currentStateIndex; }
        private set
        {
            if (currentState != null)
                currentState.Close();
            currentStateIndex = value;
            CurrentState = _states[currentStateIndex];
        }
    }
    #endregion Properties

    #region Methods
    public virtual void SetState(int index)
    {
        if (index >= 0 && index < _states.Length)
        {
            CurrentStateIndex = index;
        }
    }

    public virtual void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }

    public void CreateStates(int statesCount)
    {
        _states = new State[statesCount];
    }

    public void CreateStates(System.Type statesEnum)
    {
        _states = new State[System.Enum.GetValues(statesEnum).Length];
    }

    /// <summary>
    /// Method InitializeState sets the Init, Update, Close handlers.
    /// </summary>
    /// <param name="stateIndex">Index of state. Convenient use the enum member.</param>
    /// <param name="init">The handler of init state.</param>
    /// <param name="update">The handler of update state.</param>
    /// <param name="close">The handler of close state.</param>
    public void InitializeState(int stateIndex, State.StateHandler init, State.StateHandler update, State.StateHandler close)
    {
        if (stateIndex >= _states.Length)
            Debug.LogError($"[{this.GetType()}] {System.Reflection.MethodInfo.GetCurrentMethod().Name} index of state {stateIndex} is out of range!");
        _states[stateIndex] = new State(init, update, close);
    }

    public abstract void InitializeMachine();
    #endregion Methods
}