using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFigure
{
    FigureType FigureType { get; }
    bool[,] Cells { get; }
    Vector2Int[,] Positions { get; }
    int Width { get; }
    int Height { get; }
}
