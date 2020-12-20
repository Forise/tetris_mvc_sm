using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Figure")]
public class Figure : ScriptableObject, IFigure
{
    #region Fields
    [SerializeField]
    private Color color;
    private bool[,] _cells = new bool[Width, Height];
    #endregion Fields

    #region Properties
    public const int Width = 4;

    public const int Height = 4;

    public bool[,] Cells => _cells;
    #endregion Properties;

#if UNITY_EDITOR
    public void SetCellEditor(int x, int y, bool value)
    {
        _cells[x, y] = value;
    }
#endif
}
