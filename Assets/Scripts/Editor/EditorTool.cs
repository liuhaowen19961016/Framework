using UnityEditor;

public class EditorTool
{
    [MenuItem(EditorConst.ClearProgressBar, priority = 0)]
    private static void ClearProgressBar()
    {
        EditorUtility.ClearProgressBar();
    }
}