using UnityEditor;
using UnityEngine;
using System;

public class CommonDialogWindow : EditorWindow
{
    private string content;
    private Vector2 scrollPosition = Vector2.zero;
    private string btn1Name;

    public static void CreateWindow(string title, string content, string btn1Name)
    {
        CommonDialogWindow window = GetWindow<CommonDialogWindow>(true, title, true);
        window.content = content;
        window.Show();
    }

    private void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        string[] contentArray = content.Split('\n');
        foreach (var content in contentArray)
        {
            EditorGUILayout.SelectableLabel(content, GUILayout.Height(15));
        }
        EditorGUILayout.EndScrollView();
        if (!string.IsNullOrEmpty(btn1Name))
        {
            if (GUILayout.Button(btn1Name))
            {
                Close();
            }
        }
    }
}

public class CommonDialogWindowCallbacks : ScriptableObject
{
    public Action OnBtn1;
    public Action OnBtn2;
    public Action OnBtn3;

    public void SetCallbacks(Action onBtn1, Action onBtn2, Action onBtn3)
    {
        OnBtn1 = onBtn1;
        OnBtn2 = onBtn2;
        OnBtn3 = onBtn3;
    }
}