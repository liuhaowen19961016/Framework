using UnityEditor;

public class EditorTools
{
    [MenuItem(EditorConst.ClearProgressBar, priority = 0)]
    private static void ClearProgressBar()
    {
        EditorUtility.ClearProgressBar();
    }
}