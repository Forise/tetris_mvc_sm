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
    }
}