using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class TetrisController : ITetrisController
{
    public ITetrisModel Model { get; private set; }
    public IFigureFactory Factory { get; private set; }

    public TetrisController(ITetrisModel model, IFigureFactory figureFactory)
    {
        Model = model;
        Factory = figureFactory;
    }

    public bool MoveFigureLeft()
    {

        return false;
    }

    public bool MoveFigureRight()
    {
        return false;
    }

    public bool RotateFigureLeft()
    {
        return false;
    }

    public bool RotateFigureRight()
    {
        return false;
    }

    public bool Step()
    {
        return false;
    }
}