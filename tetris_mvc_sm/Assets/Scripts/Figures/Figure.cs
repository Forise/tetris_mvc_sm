using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Figure")]
public class Figure : ScriptableObject
{
    [SerializeField]
    private FigureSM figure;
    public IFigure IFigure => figure;

#if UNITY_EDITOR
    public FigureSM FigureSM => figure;
#endif
}
