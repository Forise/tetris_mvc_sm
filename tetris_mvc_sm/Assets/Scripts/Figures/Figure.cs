using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Figure")]
public class Figure : ScriptableObject, IFigure
{
    #region Fields
    [SerializeField]
    private Color color;
    [SerializeField]
    private FigureType _figureType;
    private bool[,] _cells = new bool[WIDTH, HEIGHT];
    private Vector2Int[,] _positions = new Vector2Int[WIDTH, HEIGHT];
    #endregion Fields

    #region Properties
    public const int WIDTH = 4;
    public const int HEIGHT = 4;
    public int Width => WIDTH;
    public int Height => HEIGHT;
    public FigureType FigureType => _figureType;
    public bool[,] Cells => _cells;
    public Vector2Int[,] Positions => _positions;
    #endregion Properties;

    public Figure()
    {
        _positions = new Vector2Int[Width, Height];
    }

    public void MoveLeft()
    {

    }

#if UNITY_EDITOR
    public void SetCellEditor(int x, int y, bool value)
    {
        _cells[x, y] = value;
    }
#endif
}
