using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Models;

public class UnityTetrisController : MonoBehaviour
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
    public TetrisController Controller { get; private set; }

    private void Start()
    {
        var model = _unityTetrisModel.Model;
        model.LineCleared += () => 
        { 
            _stepDelaySec -= _reduceStepDelayByClearLine;
            _stepDelaySec = _stepDelaySec < _minDelay ? _minDelay : _stepDelaySec;
        };
        Controller = new TetrisController(model, _figureFactory);
#if TEST
        Controller.testInstantiate();
#else
        StartCoroutine(Run());
#endif
    }

    private void Update()
    {
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

    IEnumerator Run()
    {
        bool run = true;
        while(run)
        {
            yield return new WaitForSeconds(_stepDelaySec);
            if (!Controller.Step())
            {
                SceneManager.LoadScene("Loser");
                run = false;
            }
        }
    }
}
