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
        bool MoveFigureLeft();
        bool MoveFigureRight();
        bool MoveFigureDown();
        bool RotateFigureLeft();
        bool RotateFigureRight();
        void SetFigureToField();
        void ClearCompletedLines();
#if TEST
        void NotifyChanged();
#endif
    }
}