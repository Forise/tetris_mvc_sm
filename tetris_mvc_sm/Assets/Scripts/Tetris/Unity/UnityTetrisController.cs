using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Models;

public class UnityTetrisController : StateMachineMono
{
    [SerializeField]
    private float _stepDelaySec = 0.5f;
    [SerializeField]
    private float _reduceStepDelayByClearLine = 0.01f;
    [SerializeField]
    private float _minDelay = 0.1f;
    [SerializeField]
    private UnityTetrisModel _unityTetrisModel;
    [SerializeField]
    private FigureFactory _figureFactory;
    private bool _run = false;
    private float _deltaFromStep = 0f;
    public TetrisController Controller { get; private set; }

    private void Start()
    {
        InitializeMachine();
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Controller.MoveFigureLeft();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Controller.MoveFigureRight();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Controller.RotateFigureLeft();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Controller.RotateFigureRight();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Controller.DropFigure();
        }

#if TEST
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Controller.Model.NotifyChanged();
            Controller.Step();
        }
#endif
    }

    #region State Machine
    public enum TetrisGameState
    {
        Gameplay = 0,
        Lose = 1
    }

    public override void InitializeMachine()
    {
        CreateStates(typeof(TetrisGameState));
        InitializeState((int)TetrisGameState.Gameplay, InitGameplayState, UpdateGameplyState, null);
        InitializeState((int)TetrisGameState.Lose, InitLoseState, null, null);
        SetState(0);
    }

    private void InitGameplayState()
    {
        var model = _unityTetrisModel.Model;
        model.LineCleared += () => 
        { 
            _stepDelaySec -= _reduceStepDelayByClearLine;
            _stepDelaySec = _stepDelaySec < _minDelay ? _minDelay : _stepDelaySec;
        };
        Controller = new TetrisController(model, _figureFactory);
        _run = true;
#if TEST
        Controller.testInstantiate();
#endif
    }

    private void UpdateGameplyState()
    {
        if(_run)
        {
            _deltaFromStep += Time.deltaTime;
            if(_deltaFromStep >= _stepDelaySec)
            {
                _deltaFromStep = 0f;
                if (!Controller.Step())
                {
                    _run = false;
                    SetState(1);
                }
            }
        }
    }

    private void InitLoseState()
    {
        SceneManager.LoadScene("Loser");
    }
    #endregion State Machine
}
