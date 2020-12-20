using UnityEngine;

[CreateAssetMenu(menuName = "Figure Factory")]
public class FigureFactory : ScriptableObject, IFigureFactory
{
    [SerializeField]
    private Figure[] _figures;

    public IFigure GetRandomFigure => _figures[Random.Range(0, _figures.Length)].IFigure;
}
