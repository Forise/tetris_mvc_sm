using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a lot of members need to split for several interfaces
public interface IFigure
{
    FigureType FigureType { get; }
    bool[] Cells { get; set; }
    Vector2Int[,] Positions { get; set; }
    int Width { get; }
    int Height { get; }
    bool this[int x, int y] { get; set; }
    Color Color { get; }
    void RotateRight();
    void RotateLeft();
    bool IsCellPartOfFigure(int x, int y);
}
