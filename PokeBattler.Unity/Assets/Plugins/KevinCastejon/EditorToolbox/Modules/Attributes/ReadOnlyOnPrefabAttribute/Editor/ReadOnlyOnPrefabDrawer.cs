using UnityEngine;
using UnityEditor;


namespace KevinCastejon.EditorToolbox
{
    [CustomPropertyDrawer(typeof(ReadOnlyOnPrefabAttribute))]
    public class ReadOnlyOnPrefabDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ReadOnlyOnPrefabAttribute att = (ReadOnlyOnPrefabAttribute)attribute;
            bool rdOnly = att.invert ? UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null : UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null;
            if (rdOnly)
            {
                EditorGUI.BeginDisabledGroup(true);
            }

            EditorGUI.PropertyField(position, property, label);

            if (rdOnly)
            {
                EditorGUI.EndDisabledGroup();
            }
        }
    }
}