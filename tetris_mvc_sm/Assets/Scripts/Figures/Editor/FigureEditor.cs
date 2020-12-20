using UnityEditor;

[CustomEditor(typeof(Figure))]
public class FigureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var figure = (Figure)serializedObject.targetObject;

        using (var verticalScope = new EditorGUILayout.VerticalScope())
        {
            for (int y = 0; y < Figure.HEIGHT; y++)
            {
                using (var horizontalScope = new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PrefixLabel(y.ToString());
                    for (int x = 0; x < Figure.WIDTH; x++)
                    {
                        bool oldState = figure.Cells[x, y];
                        bool newState = EditorGUILayout.Toggle(oldState);
                        if (newState != oldState)
                        {
                            Undo.RecordObject(figure, "ChangeFigure");
                            figure.SetCellEditor(x, y, newState);
                            EditorUtility.SetDirty(figure);
                        }
                    }
                }
            }
        }
    }
}