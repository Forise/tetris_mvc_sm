using Models;

public interface ITetrisController
{
    ITetrisModel Model { get; }
    IFigureFactory Factory { get; }

    void MoveFigureLeft();
    void MoveFigureRight();
    bool RotateFigureLeft();
    bool RotateFigureRight();
    bool Step();
}
