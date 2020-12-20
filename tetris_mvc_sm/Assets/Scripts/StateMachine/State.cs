public class State
{
    public delegate void StateHandler();
    protected StateHandler InitCallback, UpdateCallback, CloseCallback;

    public State() { }
    public State(StateHandler init, StateHandler update, StateHandler close)
    {
        InitCallback = init;
        UpdateCallback = update;
        CloseCallback = close;
    }
    #region Methods
    public virtual void Init()
    {
        InitCallback?.Invoke();
    }

    public virtual void Update()
    {
        UpdateCallback?.Invoke();
    }

    public virtual void Close()
    {
        CloseCallback?.Invoke();
    }

    public override string ToString()
    {
        return $"[State]: Init{(InitCallback != null ? InitCallback.Method.Name : "null")}, Close{(CloseCallback != null ? CloseCallback.Method.Name : "null")}, Update{(UpdateCallback != null ? UpdateCallback.Method.Name : "null")}";
    }
    #endregion
}