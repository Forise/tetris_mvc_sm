using Models;

public interface ITetrisController
{
    ITetrisModel Model { get; }
    IFigureFactory Factory { get; }

    bool MoveFigureLeft();
    bool MoveFigureRight();
    bool RotateFigureLeft();
    bool RotateFigureRight();
    bool Step();
}
