using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FigureSM : CycledStateMachine, IFigure
{
    #region Fields
    [SerializeField]
    private Color _color;
    [SerializeField]
    private FigureType _figureType;
    private bool[] _cells = new bool[WIDTH * HEIGHT];

    private bool[] _cellsRotation0 = new bool[WIDTH * HEIGHT];
    private bool[] _cellsRotation90 = new bool[WIDTH * HEIGHT];
    private bool[] _cellsRotation180 = new bool[WIDTH * HEIGHT];
    private bool[] _cellsRotation270 = new bool[WIDTH * HEIGHT];
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

    public FigureSM()
    {
        Positions = new Vector2Int[Width, Height];
    }

    private bool IsCellPartOfFigure(int x, int y)
    {
        return _cells[y * WIDTH + x];
    }

    public void RotateRight()
    {
        SetNextState();
    }
    public void RotateLeft()
    {
        SetPreviousState();
    }

    #region State Machine
    protected override void InitializeMachine()
    {
        CreateStates(typeof(RotationState));
        InitializeState((int)RotationState.Rot0, () => { _cells = _cellsRotation0; }, null, null);
        InitializeState((int)RotationState.Rot90, () => { _cells = _cellsRotation90; }, null, null);
        InitializeState((int)RotationState.Rot180, () => { _cells = _cellsRotation180; }, null, null);
        InitializeState((int)RotationState.Rot270, () => { _cells = _cellsRotation270; }, null, null);
        SetState((int)RotationState.Rot0);
    }

    public enum RotationState
    {
        Rot0,
        Rot90,
        Rot180,
        Rot270
    }
    #endregion State Machine

    #region EDITOR
#if UNITY_EDITOR
    public bool IsCellPartOfFigureRot0(int x, int y)
    {
        return _cellsRotation0[y * WIDTH + x];
    }

    public void SetCellRot0(int x, int y, bool value)
    {
        _cellsRotation0[y * WIDTH + x] = value;
    }

    public bool IsCellPartOfFigureRot90(int x, int y)
    {
        return _cellsRotation90[y * WIDTH + x];
    }

    public void SetCellRot90(int x, int y, bool value)
    {
        _cellsRotation90[y * WIDTH + x] = value;
    }

    public bool IsCellPartOfFigureRot180(int x, int y)
    {
        return _cellsRotation180[y * WIDTH + x];
    }

    public void SetCellRot180(int x, int y, bool value)
    {
        _cellsRotation180[y * WIDTH + x] = value;
    }

    public bool IsCellPartOfFigureRot270(int x, int y)
    {
        return _cellsRotation270[y * WIDTH + x];
    }

    public void SetCellRot270(int x, int y, bool value)
    {
        _cellsRotation270[y * WIDTH + x] = value;
    }
#endif
    #endregion EDITOR
}
