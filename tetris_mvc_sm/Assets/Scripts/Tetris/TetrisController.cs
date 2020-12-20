using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class TetrisController : ITetrisController
{
    public ITetrisModel Model { get; private set; }
    public IFigureFactory Factory { get; private set; }

    public void MoveFigureLeft()
    {
    }

    public void MoveFigureRight()
    {
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