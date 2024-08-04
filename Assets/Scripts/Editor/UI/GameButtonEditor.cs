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
    private SerializedProperty m_EnableClickAni;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_EnableClickSound = serializedObject.FindProperty("enableClickSound");
        m_ClickSoundId = serializedObject.FindProperty("clickSoundId");
        m_EnableClickAni = serializedObject.FindProperty("enableClickAni");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        serializedObject.Update();
        EditorGUILayout.PropertyField(m_EnableClickSound, new GUIContent("开启点击按钮音效"));
        EditorGUILayout.PropertyField(m_ClickSoundId, new GUIContent("按钮音效id"));
        EditorGUILayout.PropertyField(m_EnableClickAni, new GUIContent("开启点击按钮动画"));
        serializedObject.ApplyModifiedProperties();
    }
}