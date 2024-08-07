using UnityEditor;
using UnityEngine;
using System;

public class CommonDialogWindow : EditorWindow
{
    private string content;
    private Vector2 scrollPosition = Vector2.zero;
    private string btn1Name;
    private string btn2Name;
    private string btn3Name;
    private Action onBtn1;
    private Action onBtn2;
    private Action onBtn3;

    public static void CreateWindow(string title, string content, string btn1Name,
        string btn2Name = "", string btn3Name = "", Action onBtn1 = null, Action onBtn2 = null, Action onBtn3 = null)
    {
        CommonDialogWindow window = GetWindow<CommonDialogWindow>(true, title, true);
        window.content = content;
        window.btn1Name = btn1Name;
        window.btn2Name = btn2Name;
        window.btn3Name = btn3Name;
        window.onBtn1 = onBtn1;
        window.onBtn2 = onBtn2;
        window.onBtn3 = onBtn3;
        window.Show();
    }

    private void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        EditorGUILayout.SelectableLabel(content, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();
        if (!string.IsNullOrEmpty(btn1Name))
        {
            if (GUILayout.Button(btn1Name))
            {
                onBtn1?.Invoke();
                Close();
            }
        }
        if (!string.IsNullOrEmpty(btn2Name))
        {
            if (GUILayout.Button(btn2Name))
            {
                onBtn2?.Invoke();
                Close();
            }
        }
        if (!string.IsNullOrEmpty(btn3Name))
        {
            if (GUILayout.Button(btn3Name))
            {
                onBtn3?.Invoke();
                Close();
            }
        }
    }
}