using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public delegate void ModelChanged();
    public interface ITetrisModel
    {
        int Width { get; }
        int Height { get; }
        IFigure Figure { get; set; }
        CellState[,] Field { get; }
        event ModelChanged ModelChanged;
        event System.Action LineCleared;
        bool MoveFigureLeft();
        bool MoveFigureRight();
        bool MoveFigureDown();
        void DropFigure();
        bool RotateFigureLeft();
        bool RotateFigureRight();
        void SetFigureToField();
        void ClearCompletedLines();
#if TEST
        void NotifyChanged();
#endif
    }
}