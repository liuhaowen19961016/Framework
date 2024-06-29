using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(GameButton), true)]
[CanEditMultipleObjects]
public class GameButtonEditor : SelectableEditor
{
    private SerializedProperty m_EnableClickSound;
    private SerializedProperty m_ClickSoundId;
    private SerializedProperty m_EnableCheckInvalid;
    private SerializedProperty m_EnableCheckLongPress;
    private SerializedProperty m_EnableCheckDoubleClick;
    private SerializedProperty m_InvalidTime;
    private SerializedProperty m_LongPressTime;
    private SerializedProperty m_DoubleClickTime;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_EnableClickSound = serializedObject.FindProperty("enableClickSound");
        m_ClickSoundId = serializedObject.FindProperty("clickSoundId");
        m_EnableCheckInvalid = serializedObject.FindProperty("enableCheckInvalid");
        m_EnableCheckLongPress = serializedObject.FindProperty("enableCheckLongPress");
        m_EnableCheckDoubleClick = serializedObject.FindProperty("enableCheckDoubleClick");
        m_InvalidTime = serializedObject.FindProperty("invalidTime");
        m_LongPressTime = serializedObject.FindProperty("longPressTime");
        m_DoubleClickTime = serializedObject.FindProperty("doubleClickTime");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        serializedObject.Update();
        EditorGUILayout.PropertyField(m_EnableClickSound, new GUIContent("开启点击按钮音效"));
        EditorGUILayout.PropertyField(m_ClickSoundId, new GUIContent("按钮音效id"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(m_EnableCheckInvalid, new GUIContent("开启无效检测"));
        EditorGUILayout.PropertyField(m_EnableCheckLongPress, new GUIContent("开启长按检测"));
        EditorGUILayout.PropertyField(m_EnableCheckDoubleClick, new GUIContent("开启连击检测"));
        EditorGUILayout.PropertyField(m_InvalidTime, new GUIContent("视为无效的时间"));
        EditorGUILayout.PropertyField(m_LongPressTime, new GUIContent("视为长按的时间"));
        EditorGUILayout.PropertyField(m_DoubleClickTime, new GUIContent("视为连击的时间"));
        serializedObject.ApplyModifiedProperties();
    }
}