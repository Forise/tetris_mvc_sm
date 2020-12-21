using UnityEngine;

[CreateAssetMenu(menuName = "Figure Factory")]
public class FigureFactory : ScriptableObject, IFigureFactory
{
#if !TEST
    [SerializeField]
    private Figure[] _figures;
#else
    public Figure[] _figures;
    public Figure[] Figures => _figures;
#endif
    public IFigure GetRandomFigure => _figures[Random.Range(0, _figures.Length)].IFigure;
}
