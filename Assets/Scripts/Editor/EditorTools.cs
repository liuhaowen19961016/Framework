using UnityEditor;

public class EditorTools
{
    [MenuItem("游戏工具/ClearProgressBar", priority = 0)]
    private static void ClearProgressBar()
    {
        EditorUtility.ClearProgressBar();
    }
}