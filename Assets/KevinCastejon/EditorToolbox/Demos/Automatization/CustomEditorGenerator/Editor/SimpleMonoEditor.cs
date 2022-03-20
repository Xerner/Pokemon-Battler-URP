using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(SimpleMono))]
public class SimpleMonoEditor : Editor
{
    private SerializedProperty _myInt;
    private SerializedProperty _myFloat;
    private SerializedProperty _myBool;

    private SimpleMono _script;

    private void OnEnable()
    {
        _myInt = serializedObject.FindProperty("_myInt");
        _myFloat = serializedObject.FindProperty("_myFloat");
        _myBool = serializedObject.FindProperty("_myBool");

        _script = (SimpleMono)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_myInt);
        EditorGUILayout.PropertyField(_myFloat);
        EditorGUILayout.PropertyField(_myBool);

        serializedObject.ApplyModifiedProperties();
    }
}
