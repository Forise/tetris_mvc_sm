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
        IFigure Figure { get; }
        CellState[,] Field { get; }
        event ModelChanged ModelChanged;
    }
}