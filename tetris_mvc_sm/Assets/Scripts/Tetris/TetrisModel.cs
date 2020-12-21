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

        #region Properties
        public IFigure Figure { get; set; }

        public CellState[,] Field
        {
            get;
            private set;
        }

        public CellState this[int row, int col]
        {
            get
            {
                if (row < Width && col < Height && row >= 0 && col >= 0)
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
            Figure = null;
        }

        #region Move/Rotation
        public bool MoveFigureLeft()
        {
            return InternalMoveFigureHorizontal(-1);
        }

        public bool MoveFigureRight()
        {
            return InternalMoveFigureHorizontal(1);
        }

        public bool MoveFigureDown()
        {
            if (Figure == null)
            {
                return false;
            }

            bool canMove = CanFigureMove(0, 1);
            if (canMove)
            {
                for (int x = 0; x < Figure.Width; x++)
                {
                    for (int y = 0; y < Figure.Height; y++)
                    {
                        if (Figure[x, y])
                        {
                        }
                            Figure.Positions[x, y].y++;
                    }
                }
                ModelChanged?.Invoke();
                return true;
            }
            return false;
        }

        private bool InternalMoveFigureHorizontal(int offsetX = -1)
        {
            if (Figure == null)
            {
                return false;
            }
            bool canMove = CanFigureMove(offsetX, 0);
            if (canMove)
            {
                for (int x = 0; x < Figure.Width; x++)
                {
                    for (int y = 0; y < Figure.Height; y++)
                    {
                        Figure.Positions[x, y].x += offsetX;
                    }
                }
                ModelChanged?.Invoke();
                return true;
            }
            return false;
        }

        private bool CanFigureMove(int offsetX, int offsetY)
        {
            bool canMove = true;
            for (int x = 0; x < Figure.Width; x++)
            {
                for (int y = 0; y < Figure.Height; y++)
                {
                    if (Figure[x, y])
                    {
                        int xPos = Figure.Positions[x, y].x;
                        int yPos = Figure.Positions[x, y].y;
                        bool targetXIsCorrect = xPos + offsetX >= 0 && xPos + offsetX < Width;
                        bool targetYIsCorrect = yPos + offsetY >= 0 && yPos + offsetY < Height;
                        canMove = targetYIsCorrect && targetXIsCorrect && Field[xPos + offsetX, yPos] != CellState.Filled && Field[xPos, yPos + offsetY] != CellState.Filled;
                        if (canMove == false)
                        {
                            return false;
                        }
                    }
                }
                if (canMove == false)
                {
                    return false;
                }
            }
            return canMove;
        }

        private bool IsFigureInsideField()
        {
            bool insideField = true;
            for (int x = 0; x < Figure.Width; x++)
            {
                for (int y = 0; y < Figure.Height; y++)
                {
                    if (Figure[x, y])
                    {
                        int xPos = Figure.Positions[x, y].x;
                        int yPos = Figure.Positions[x, y].y;
                        bool targetXIsCorrect = xPos >= 0 && xPos < Width;
                        bool targetYIsCorrect = yPos >= 0 && yPos < Height;
                        insideField = targetYIsCorrect && targetXIsCorrect && Field[xPos, yPos] != CellState.Filled && Field[xPos, yPos] != CellState.Filled;
                        if (insideField == false)
                        {
                            return false;
                        }
                    }
                }
                if (insideField == false)
                {
                    return false;
                }
            }
            return insideField;
        }

        public bool RotateFigureLeft()
        {
            Figure.RotateLeft();
            if (IsFigureInsideField())
            {
                ModelChanged?.Invoke();
            }
            else
            {
                Figure.RotateRight();
            }
            return false;
        }

        public bool RotateFigureRight()
        {
            Figure.RotateRight();
            if (IsFigureInsideField())
            {
                ModelChanged?.Invoke();
            }
            else
            {
                Figure.RotateLeft();
            }
            return false;
        }
        #endregion Move/Rotation

        #region TEST
#if TEST
        public void NotifyChanged()
        {
            ModelChanged?.Invoke();
        }
#endif
        #endregion TEST
    }
}