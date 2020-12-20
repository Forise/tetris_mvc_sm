using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnityTetrisView : MonoBehaviour
{
    private ITetrisModel _model;
    [SerializeField]
    private UnityTetrisModel _unityTetrisModel;

    public Tilemap mainMap;
    public Tilemap figureMap;
    public Tile emptyTile;
    public Tile fullTile;
    public Tile figureTile;

    private void Start()
    {
        _model = _unityTetrisModel.Model;
        _model.ModelChanged += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        _model.ModelChanged -= Refresh;
    }

    private void Refresh()
    {
        UpdateField();
        UpdateFigure();
    }

    private void UpdateField()
    {
        int w = _model.Width;
        int h = _model.Height;

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                if(_model.Field[x, y] == CellState.Empty)
                {
                    mainMap.SetTile(new Vector3Int(x, h - y - 1, 0), emptyTile);
                }
                else if(_model.Field[x, y] == CellState.Filled)
                {
                    mainMap.SetTile(new Vector3Int(x, h - y - 1, 0), fullTile);
                }
            }
        }
    }

    private void UpdateFigure()
    {
        figureMap.ClearAllTiles();

        if (_model.Figure == null)
        {
            return;
        }

        int w = _model.Figure.Width;
        int h = _model.Figure.Height;

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                if (_model.Figure[x, y])
                {
                    figureTile.color = _model.Figure.Color;
                    figureMap.SetTile(new Vector3Int(_model.Figure.Positions[x,y].x, _model.Height - (_model.Figure.Positions[x, y].y) - 1, 0), figureTile);
                }
            }
        }
    }
}
