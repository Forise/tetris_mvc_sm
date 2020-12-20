using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Figure")]
public class Figure : ScriptableObject, IFigure
{
    #region Fields
    [SerializeField]
    private Color _color;
    [SerializeField]
    private FigureType _figureType;
    private bool[] _cells = new bool[WIDTH * HEIGHT];
    #endregion Fields

    #region Properties
    public const int WIDTH = 4;
    public const int HEIGHT = 4;
    public int Width => WIDTH;
    public int Height => HEIGHT;
    public FigureType FigureType => _figureType;
    public bool[] Cells => _cells;
    public Color Color => _color;
    public bool this[int x, int y]
    {
        get => IsCellPartOfFigure(x, y);
        set
        {
            _cells[y * WIDTH + x] = value;
        }
    }
    public Vector2Int[,] Positions { get; set; } = new Vector2Int[WIDTH, HEIGHT];
    #endregion Properties;

    public Figure()
    {
        Positions = new Vector2Int[Width, Height];
    }
    private bool IsCellPartOfFigure(int x, int y)
    {
        return _cells[y * WIDTH + x];
    }
}
