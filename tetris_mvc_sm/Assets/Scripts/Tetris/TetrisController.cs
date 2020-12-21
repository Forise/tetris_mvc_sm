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

    public void RotateFigureLeft()
    {
        Model.RotateFigureLeft();
    }

    public void RotateFigureRight()
    {
        Model.RotateFigureRight();
    }

    public void DropFigure()
    {
        Model.DropFigure();
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
            Model.SetFigureToField();
            Model.ClearCompletedLines();
            Model.Figure = null;
        }

        return true;
    }

    private void InstantiateFigure(ITetrisModel model, IFigureFactory figureFactory)
    {
#if !TEST
        Model.Figure = figureFactory.GetRandomFigure;
        for (int x = 0; x < Model.Figure.Width; x++)
        {
            for (int y = 0; y < Model.Figure.Height; y++)
            {
                Model.Figure.Positions[x, y] = new Vector2Int(x+2, y);
            }
        }
#else
        Figure figure = null;
        foreach(var f in figureFactory.Figures)
        {
            if(f.FigureSM.FigureType == FigureType.Z)
            {
                figure = f;
                break;
            }
        }
        Model.Figure = figure.IFigure;
        for (int x = 0; x < Model.Figure.Width; x++)
        {
            for (int y = 0; y < Model.Figure.Height; y++)
            {
                Model.Figure.Positions[x, y] = new Vector2Int(x, y);
            }
        }
        Model.NotifyChanged();
#endif
    }

    private bool FigureCollides(ITetrisModel model)
    {
        if (model.Figure == null)
        {
            return false;
        }

        for (int y = 0; y < Model.Figure.Height; y++)
        {
            for (int x = 0; x < Model.Figure.Width; x++)
            {
                if (Model.Figure.IsCellPartOfFigure(x, y) && model.Field[Model.Figure.Positions[x, y].x, Model.Figure.Positions[x, y].y] != CellState.Empty)
                    return true;
            }
        }

        return false;
    }
}