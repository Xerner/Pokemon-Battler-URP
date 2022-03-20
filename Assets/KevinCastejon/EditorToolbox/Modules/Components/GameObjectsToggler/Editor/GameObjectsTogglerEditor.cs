using UnityEditor;
using UnityEngine;

namespace KevinCastejon.EditorToolbox
{
    [CustomEditor(typeof(GameObjectsToggler))]
    public class GameObjectsTogglerEditor : Editor
    {
        private SerializedProperty _a;
        private SerializedProperty _b;
        private SerializedProperty _onToggle;
        private SerializedProperty _onActivatedA;
        private SerializedProperty _onActivatedB;

        private GameObjectsToggler _script;
        private void OnEnable()
        {

            _a = serializedObject.FindProperty("_a");
            _b = serializedObject.FindProperty("_b");
            _onToggle = serializedObject.FindProperty("_onToggle");
            _onActivatedA = serializedObject.FindProperty("_onActivatedA");
            _onActivatedB = serializedObject.FindProperty("_onActivatedB");

            _script = target as GameObjectsToggler;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            Color backupColor = GUI.contentColor;
            if (_a.objectReferenceValue != null && _b.objectReferenceValue != null)
            {
                if ((_a.objectReferenceValue as GameObject).activeSelf)
                {
                    GUI.contentColor = Color.yellow;
                    EditorGUILayout.PropertyField(_a);
                    GUI.contentColor = backupColor;
                    EditorGUILayout.PropertyField(_b);
                    (_b.objectReferenceValue as GameObject).SetActive(false);
                }
                else
                {
                    EditorGUILayout.PropertyField(_a);
                    GUI.contentColor = Color.yellow;
                    EditorGUILayout.PropertyField(_b);
                    GUI.contentColor = backupColor;
                    (_a.objectReferenceValue as GameObject).SetActive(false);
                }
            }
            else
            {
                EditorGUILayout.PropertyField(_a);
                EditorGUILayout.PropertyField(_b);
            }
            EditorGUILayout.PropertyField(_onToggle);
            EditorGUILayout.PropertyField(_onActivatedA);
            EditorGUILayout.PropertyField(_onActivatedB);
            EditorGUILayout.Space(10);
            if (_a.objectReferenceValue != null && _b.objectReferenceValue != null)
            {
                GameObject a = _a.objectReferenceValue as GameObject;
                GameObject b = _b.objectReferenceValue as GameObject;
                EditorGUILayout.LabelField("Active GameObject : " + (a.activeSelf ? a.name : b.name), EditorStyles.boldLabel);
            }
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Toggle") && _a.objectReferenceValue != null && _b.objectReferenceValue != null)
            {
                _script.Toggle();
            }
            EditorGUILayout.Space(10);
            serializedObject.ApplyModifiedProperties();
        }
    }
}