using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InputManager.InputActionsInfo))]
//[CustomPropertyDrawer(typeof(InputActions))]
public class InputActionsInfoDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
    {
        if (InputManager.CurrentActionMap == null) 
            EditorGUI.LabelField(position, "Current Action Map", "");
        else 
        {
            string currentActionMap = InputManager.CurrentActionMap.name;
            EditorGUI.LabelField(position, "Current Action Map", currentActionMap);
        }
    }
}
