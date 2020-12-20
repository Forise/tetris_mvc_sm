using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public delegate void StateHandler();
    protected StateHandler InitCallback, UpdateCallback, ExitCallback;

    public State() { }
    public State(StateHandler init, StateHandler update, StateHandler exit)
    {
        InitCallback = init;
        UpdateCallback = update;
        ExitCallback = exit;
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
        ExitCallback?.Invoke();
    }

    public override string ToString()
    {
        return $"[State]: Init{(InitCallback != null ? InitCallback.Method.Name : "null")}, Exit{(ExitCallback != null ? ExitCallback.Method.Name : "null")}, Update{(UpdateCallback != null ? UpdateCallback.Method.Name : "null")}";
    }
    #endregion
}