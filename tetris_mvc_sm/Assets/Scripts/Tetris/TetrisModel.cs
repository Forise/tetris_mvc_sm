using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class TetrisModel : ITetrisModel
    {
        public event ModelChanged ModelChanged;

        public int Width => 10;

        public int Height => 20;

        private IFigure _figure;

        #region Properties
        public IFigure Figure => _figure;

        public CellState[,] Field
        {
            get;
            private set;
        }

        public CellState this[int row, int col]
        {
            get
            {
                if(row < Width && col < Height && row >= 0 && col >= 0)
                {
                    return Field[row, col];
                }
                else
                {
                    Debug.LogError($"[FieldModel] Indexer out of range [row:{row} / WIDTH:{Width}] [col:{col} / HEIGHT:{Height}]");
                    return CellState.Error;
                }
            }

            private set
            {
                if (row < Width && col < Height && row >= 0 && col >= 0)
                {
                    Field[row, col] = value;
                    ModelChanged?.Invoke();
                }
                else
                {
                    Debug.LogError($"[FieldModel] Indexer out of range [row:{row} / WIDTH:{Width}] [col:{col} / HEIGHT:{Height}]");
                }
            }
        }
        #endregion Properties

        public TetrisModel()
        {
            Field = new CellState[Width, Height];
            _figure = null;
        }

        public bool MoveFigureLeft()
        {
            return InternalMoveFigureLeft();
        }
        
        public bool MoveFigureRight()
        {
            return InternalMoveFigureLeft(-1);
        }

        private bool InternalMoveFigureLeft(int offset = 1)
        {
            bool canMove = false;
            for (int x = 0; x < Figure.Width; x++)
            {
                for (int y = 0; y < Figure.Height; y++)
                {
                    int xPos = Figure.Positions[x, y].x;
                    int yPos = Figure.Positions[x, y].y;
                    bool targetXIsCorrect = xPos - offset >= 0 && xPos - offset < Width;
                    canMove = targetXIsCorrect && Field[xPos - offset, yPos] != CellState.Filled;
                    if(canMove == false)
                    {
                        break;
                    }
                }
                if (canMove == false)
                {
                    break;
                }
            }
            if (canMove)
            {
                for (int x = 0; x < Figure.Width; x++)
                {
                    for (int y = 0; y < Figure.Height; y++)
                    {
                        Figure.Positions[x, y].x -= offset;
                    }
                }
            }
            return false;
        }
    }
}