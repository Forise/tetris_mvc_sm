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

    public void MoveFigureLeft()
    {
        Model.MoveFigureLeft();
    }

    public void MoveFigureRight()
    {
        Model.MoveFigureRight();
    }

    public bool RotateFigureLeft()
    {
        return false;
    }

    public bool RotateFigureRight()
    {
        return false;
    }

    public void testInstantiate()
    {
        InstantiateFigure(Model, Factory);
    }

    public bool Step()
    {
        if (Model.Figure == null)
        {
            InstantiateFigure(Model, Factory);
            if (FigureCollides(Model))
                return false;
        }

        if (!Model.MoveFigureDown())
        {
            //TetrisUtil.ImprintFigure(Model);
            //TetrisUtil.EraseCompleteLines(Model);
            Model.Figure = null;
        }

        return true;
    }

    private void InstantiateFigure(ITetrisModel model, IFigureFactory figureFactory)
    {
        Model.Figure = figureFactory.GetRandomFigure;
        for (int x = 0; x < Model.Figure.Width; x++)
        {
            for (int y = 0; y < Model.Figure.Height; y++)
            {
                Model.Figure.Positions[x, y] = new Vector2Int(x, y);
            }
        }
    }

    private bool FigureCollides(ITetrisModel model)
    {
        if (model.Figure == null)
        {
            return false;
        }

        //for (int y = 0; y < Figure.Height; y++)
        //{
        //    for (int x = 0; x < Figure.Width; x++)
        //    {
        //        if (Figure.At(x, y) && model.At(model.FigureX + x, model.FigureY + y))
        //            return true;
        //    }
        //}

        return false;
    }
}