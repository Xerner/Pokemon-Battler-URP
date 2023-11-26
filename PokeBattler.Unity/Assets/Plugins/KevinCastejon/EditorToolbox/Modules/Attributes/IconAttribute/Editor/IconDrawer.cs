using UnityEngine;
using UnityEditor;

namespace KevinCastejon.EditorToolbox
{
    [CustomPropertyDrawer(typeof(IconAttribute))]
    public class IconDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            IconAttribute icon = attribute as IconAttribute;
            float originalWidth = position.width;
            position.width = position.height;
            GUI.DrawTexture(position, EditorGUIUtility.Load(icon.path) as Texture2D);
            position.width = originalWidth - position.height - 5;
            position.x += position.height + 5;
            EditorGUI.PropertyField(position, property, label);
        }
    }
}