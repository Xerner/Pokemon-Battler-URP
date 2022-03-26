using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SimpleClass))]
public class SimpleClassDrawer : PropertyDrawer
{
    private float _fieldsCount = 4f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {

        return base.GetPropertyHeight(property, label) * _fieldsCount;

    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        Rect rect = new Rect(position);
        rect.height /= _fieldsCount;
        SerializedProperty _myBool = property.FindPropertyRelative("_myBool");
        SerializedProperty _myInt = property.FindPropertyRelative("_myInt");
        SerializedProperty _myFloat = property.FindPropertyRelative("_myFloat");
        EditorGUI.LabelField(rect, label);
        rect.y += rect.height;
        EditorGUI.indentLevel++;
        EditorGUI.PropertyField(rect, _myBool);
        rect.y += rect.height;
        EditorGUI.PropertyField(rect, _myInt);
        rect.y += rect.height;
        EditorGUI.PropertyField(rect, _myFloat);
        rect.y += rect.height;
        EditorGUI.indentLevel--;

    }
}
