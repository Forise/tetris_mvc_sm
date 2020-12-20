using UnityEditor;

[CustomEditor(typeof(Figure))]
public class FigureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var figure = (Figure)serializedObject.targetObject;

        using (var vert = new EditorGUILayout.VerticalScope())
        {
            for (int y = 0; y < Figure.HEIGHT; y++)
            {
                using (var horz = new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PrefixLabel(y.ToString());
                    for (int x = 0; x < Figure.WIDTH; x++)
                    {
                        bool oldValue = figure[x, y];
                        bool newValue = EditorGUILayout.Toggle(oldValue);
                        if (newValue != oldValue)
                        {
                            Undo.RecordObject(figure, "Figure Settings Changed");
                            figure[x, y] = newValue;
                            EditorUtility.SetDirty(figure);
                        }
                    }
                }
            }
        }
    }
}