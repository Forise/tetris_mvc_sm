using Models;

public interface ITetrisController
{
    ITetrisModel Model { get; }
    IFigureFactory Factory { get; }
    bool Step();
}
