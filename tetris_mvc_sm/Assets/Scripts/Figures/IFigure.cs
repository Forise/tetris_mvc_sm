using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFigure
{
    FigureType FigureType { get; }
    bool[] Cells { get; }
    Vector2Int[,] Positions { get; set; }
    int Width { get; }
    int Height { get; }
    bool this[int x, int y] { get; set; }
    Color Color { get; }
    void RotateRight();
    void RotateLeft();
}
