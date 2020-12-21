public interface IFigureFactory
{
    IFigure GetRandomFigure { get; }
#if TEST
    Figure[] Figures { get; }
#endif
}
