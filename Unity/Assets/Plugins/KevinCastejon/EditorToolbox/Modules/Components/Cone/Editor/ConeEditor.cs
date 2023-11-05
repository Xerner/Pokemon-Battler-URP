using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KevinCastejon.EditorToolbox
{
    [CustomEditor(typeof(Cone))]
    public class ConeEditor : Editor
    {
        private SerializedProperty _pivotAtTop;
        private SerializedProperty _orientation;
        private SerializedProperty _invertDirection;
        private SerializedProperty _isTrigger;
        private SerializedProperty _material;
        private SerializedProperty _coneSides;
        private SerializedProperty _proportionalRadius;
        private SerializedProperty _coneRadius;
        private SerializedProperty _coneHeight;

        private Cone _script;

        private void OnEnable()
        {
            _pivotAtTop = serializedObject.FindProperty("_pivotAtTop");
            _orientation = serializedObject.FindProperty("_orientation");
            _invertDirection = serializedObject.FindProperty("_invertDirection");
            _isTrigger = serializedObject.FindProperty("_isTrigger");
            _material = serializedObject.FindProperty("_material");
            _coneSides = serializedObject.FindProperty("_coneSides");
            _proportionalRadius = serializedObject.FindProperty("_proportionalRadius");
            _coneRadius = serializedObject.FindProperty("_coneRadius");
            _coneHeight = serializedObject.FindProperty("_coneHeight");

            _script = (Cone)target;
            if (!_script.IsConeGenerated)
            {
                _script.MakeCone();
            }
        }

        public override void OnInspectorGUI()
        {
            bool changed;
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_pivotAtTop);
            EditorGUILayout.PropertyField(_orientation);
            EditorGUILayout.PropertyField(_invertDirection);
            EditorGUILayout.PropertyField(_isTrigger);
            EditorGUILayout.PropertyField(_material);
            EditorGUILayout.PropertyField(_coneSides);
            EditorGUILayout.PropertyField(_proportionalRadius);
            EditorGUILayout.PropertyField(_coneRadius);
            EditorGUILayout.PropertyField(_coneHeight);
            changed = EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();
            if (changed)
            {
                _script.MakeCone();
            }
        }
    }
}
