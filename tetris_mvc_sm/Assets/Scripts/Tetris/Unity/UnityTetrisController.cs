﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Models;

public class UnityTetrisController : MonoBehaviour
{
    [SerializeField]
    private UnityTetrisModel _unityTetrisModel;
    [SerializeField]
    private FigureFactory _figureFactory;
    public TetrisController Controller { get; private set; }

    private void Start()
    {
        var model = _unityTetrisModel.Model;
        Controller = new TetrisController(model, _figureFactory);
        StartCoroutine(Run());
        //Controller.testInstantiate();
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
    }

    IEnumerator Run()
    {
        bool run = true;
        while(run)
        {
            yield return new WaitForSeconds(0.25f);
            if (!Controller.Step())
            {
                //SceneManager.LoadScene("GameOver");
                run = false;
            }
        }
    }
}
