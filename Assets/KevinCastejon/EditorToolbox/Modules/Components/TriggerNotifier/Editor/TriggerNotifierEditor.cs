using UnityEditor;
using UnityEngine;

namespace KevinCastejon.EditorToolbox
{
    [CustomEditor(typeof(TriggerNotifier))]
    public class TriggerNotifierEditor : Editor
    {
        private SerializedProperty _onEnter;
        private SerializedProperty _onStay;
        private SerializedProperty _onExit;
        private SerializedProperty _colliders;
        private SerializedProperty _activeMaterial;
        private SerializedProperty _inactiveMaterial;

        private TriggerNotifier _script;

        private bool _eventFolded = true;

        private void OnEnable()
        {
            _onEnter = serializedObject.FindProperty("_onEnter");
            _onStay = serializedObject.FindProperty("_onStay");
            _onExit = serializedObject.FindProperty("_onExit");
            _colliders = serializedObject.FindProperty("_colliders");
            _activeMaterial = serializedObject.FindProperty("_activeMaterial");
            _inactiveMaterial = serializedObject.FindProperty("_inactiveMaterial");

            _script = target as TriggerNotifier;
        }

        private bool isOneColliderTrigger()
        {
            Collider[] ownColliders = _script.GetComponents<Collider>();
            foreach (Collider col in ownColliders)
            {
                if (col.enabled && col.isTrigger)
                {
                    return true;
                }
            }
            return false;
        }

        public override void OnInspectorGUI()
        {
            if (!isOneColliderTrigger())
            {
                EditorGUILayout.HelpBox("TriggerDetector needs at least one enabled collider with 'IsTrigger' checked", MessageType.Error);
                return;
            }
            serializedObject.Update();
            EditorGUILayout.PropertyField(_activeMaterial, new GUIContent("Active Material", "The material applied to the renderer when at least one object is colliding"));
            EditorGUILayout.PropertyField(_inactiveMaterial, new GUIContent("Inactive Material", "The material applied to the renderer when no object is colliding"));
            _eventFolded = EditorGUILayout.BeginFoldoutHeaderGroup(_eventFolded, "Trigger events");
            if (_eventFolded)
            {
                EditorGUILayout.PropertyField(_onEnter);
                EditorGUILayout.PropertyField(_onStay);
                EditorGUILayout.PropertyField(_onExit);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            if (_script.isActiveAndEnabled)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(_colliders, new GUIContent("Triggering colliders", "List of objects currently colliding"));
                EditorGUI.EndDisabledGroup();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}