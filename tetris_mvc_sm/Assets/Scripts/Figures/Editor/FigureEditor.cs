using UnityEditor;

[CustomEditor(typeof(Figure))]
public class FigureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var figure = (Figure)serializedObject.targetObject;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Cells");

        using (var vert = new EditorGUILayout.VerticalScope())
        {
            for (int y = 0; y < FigureSM.HEIGHT; y++)
            {
                using (var horz = new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PrefixLabel(y.ToString());
                    for (int x = 0; x < FigureSM.WIDTH; x++)
                    {
                        bool oldValue = figure.IFigure[x, y];
                        bool newValue = EditorGUILayout.Toggle(oldValue);
                        if (newValue != oldValue)
                        {
                            Undo.RecordObject(figure, "Figure Settings Changed");
                            figure.IFigure[x, y] = newValue;
                            EditorUtility.SetDirty(figure);
                        }
                    }
                }
            }
        }

        var statesType = typeof(FigureSM.RotationState);
        var states = System.Enum.GetValues(statesType);

        int indexer = 0;
        foreach (var state in states)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("State", state.ToString());

            using (var vert = new EditorGUILayout.VerticalScope())
            {
                for (int y = 0; y < FigureSM.HEIGHT; y++)
                {
                    using (var horz = new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.PrefixLabel(y.ToString());
                        for (int x = 0; x < FigureSM.WIDTH; x++)
                        {
                            bool oldValue = false;
                            switch (indexer)
                            {
                                case 0:
                                    oldValue = figure.FigureSM.IsCellPartOfFigureRot0(x, y);
                                    break;
                                case 1:
                                    oldValue = figure.FigureSM.IsCellPartOfFigureRot90(x, y);
                                    break;
                                case 2:
                                    oldValue = figure.FigureSM.IsCellPartOfFigureRot180(x, y);
                                    break;
                                case 3:
                                    oldValue = figure.FigureSM.IsCellPartOfFigureRot270(x, y);
                                    break;
                            }
                            bool newValue = EditorGUILayout.Toggle(oldValue);
                            if (newValue != oldValue)
                            {
                                Undo.RecordObject(figure, "Figure Settings Changed");
                                switch (indexer)
                                {
                                    case 0:
                                        figure.FigureSM.SetCellRot0(x, y, newValue);
                                        break;
                                    case 1:
                                        figure.FigureSM.SetCellRot90(x, y, newValue);
                                        break;
                                    case 2:
                                        figure.FigureSM.SetCellRot180(x, y, newValue);
                                        break;
                                    case 3:
                                        figure.FigureSM.SetCellRot270(x, y, newValue);
                                        break;
                                }
                                EditorUtility.SetDirty(figure);
                            }
                        }
                    }
                }
            }
            indexer++;
        }
    }
}