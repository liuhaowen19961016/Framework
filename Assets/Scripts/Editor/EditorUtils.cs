using System;

public static class EditorUtils
{
    /// <summary>
    /// 显示对话框
    /// </summary>
    public static void ShowDialogWindow(string title, string content, string btn1Name,
        string btn2Name = "", string btn3Name = "", Action onBtn1 = null, Action onBtn2 = null, Action onBtn3 = null)
    {
        CommonDialogWindow.CreateWindow(title, content, btn1Name, btn2Name, btn3Name, onBtn1, onBtn2, onBtn3);
    }
}