using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomPropertyDrawer(typeof(PokemonAttribute))]
public class PokemonDrawer : PropertyDrawer {
    bool showContent = false;
    static float margin = 4f;
    static float height = 16f;

    private GUIStyle style;
    private GUIStyle style2;

    public PokemonDrawer() {
        style = new GUIStyle();
        style.normal.textColor = new Color(0.67f, 0.67f, 0.67f);
        style2 = EditorStyles.foldout;
        style2.fontStyle = FontStyle.Bold;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return (height * 2) + (margin * 2);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        Pokemon pokemon = (Pokemon)property.objectReferenceValue;
        Rect nextYPos = new Rect(position.x, position.y, position.width, height);
        EditorGUI.ObjectField(nextYPos, property);
        if (pokemon == null) return;
        nextYPos = new Rect(nextYPos.x, nextYPos.y += height + margin, nextYPos.width, height);
        EditorGUI.LabelField(nextYPos, "Name", pokemon.name, style);

        #region Stats

        showContent = EditorGUILayout.Foldout(showContent, "Stats & Info", true, style2);
        if (showContent) {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("HP", pokemon.hp.baseStat.ToString(), style);
            EditorGUILayout.LabelField("EV", pokemon.hp.effort.ToString(), style);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Attack", pokemon.attack.baseStat.ToString(), style);
            EditorGUILayout.LabelField("EV", pokemon.attack.effort.ToString(), style);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Defense", pokemon.defense.baseStat.ToString(), style);
            EditorGUILayout.LabelField("EV", pokemon.defense.effort.ToString(), style);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Special Attack", pokemon.specialAttack.baseStat.ToString(), style);
            EditorGUILayout.LabelField("EV", pokemon.specialAttack.effort.ToString(), style);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Special Defense", pokemon.specialDefense.baseStat.ToString(), style);
            EditorGUILayout.LabelField("EV", pokemon.specialDefense.effort.ToString(), style);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Speed", pokemon.speed.baseStat.ToString(), style);
            EditorGUILayout.LabelField("EV", pokemon.speed.effort.ToString(), style);
            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}
