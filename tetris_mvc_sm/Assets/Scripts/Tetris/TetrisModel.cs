using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class TetrisModel : ITetrisModel
    {
        public event ModelChanged ModelChanged;
        public event System.Action LineCleared;

        public int Width => 10;

        public int Height => 20;

        private IFigure _figure;

        #region Properties
        public IFigure Figure 
        {
            get => _figure;
            set
            {
                _figure = value;
                ModelChanged?.Invoke();
            }
        }

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

        public void SetFigureToField()
        {
            if (Figure == null)
            {
                return;
            }

            int w = Figure.Width;
            int h = Figure.Height;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (Figure.IsCellPartOfFigure(x, y))
                    {
                        Vector2Int figurePos = Figure.Positions[x, y];
                        Field[figurePos.x, figurePos.y] = CellState.Filled;
                    }
                }
            }
        }

        public void ClearCompletedLines()
        {
            for (int y = Height - 1; y >= 0; y--)
            {
                if (IsLineCompleted(y))
                {
                    MoveLineDown(y);
                    ++y;
                    LineCleared?.Invoke();
                }
            }
        }

        private bool IsLineCompleted(int y)
        {
            for (int x = 0; x < Width; x++)
            {
                if (Field[x, y] == CellState.Empty)
                {
                    return false;
                }
            }
            return true;
        }

        private void MoveLineDown(int targetY)
        {
            int w = Width;

            for (int y = targetY; y > 0; y--)
            {
                for (int x = 0; x < w; x++)
                {
                    Field[x, y] = Field[x, y - 1];
                }
            }

            for (int x = 0; x < w; x++)
            {
                Field[x, 0] = CellState.Empty;
            }
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

        public void DropFigure()
        {
            while (CanFigureMove(0, 1))
            {
                MoveFigureDown();
            }
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
            if (Figure == null)
            {
                return false;
            }
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
            if (Figure == null)
            {
                return false;
            }
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
            if (Figure == null)
            {
                return false;
            }
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
            if(Figure == null)
            {
                return false;
            }
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