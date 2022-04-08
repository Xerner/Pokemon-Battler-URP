using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomPropertyDrawer(typeof(PokemonAttribute))]
public class PokemonDrawer : PropertyDrawer {
    bool showContent = false;
    bool showLongDescription = false;
    static float margin = 4f;
    static float height = 16f;

    private GUIStyle style;
    private GUIStyle styleBold;
    private GUIStyle styleFoldout;

    public PokemonDrawer() {
        style = new GUIStyle();
        style.normal.textColor = new Color(0.67f, 0.67f, 0.67f);
        style.wordWrap = true;
        styleBold = new GUIStyle(style);
        styleBold.fontStyle = FontStyle.Bold;
        styleFoldout = EditorStyles.foldout;
        styleFoldout.fontStyle = FontStyle.Bold;
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

        showContent = EditorGUILayout.Foldout(showContent, "Stats & Info", true, styleFoldout);
        if (showContent) {
            EditorGUILayout.LabelField("Type", pokemon.TypeToString(), style);

            EditorGUILayout.LabelField("Evolutions", pokemon.EvolutionsToString(), style);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", style);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("HP", pokemon.Hp.baseStat.ToString(), style);
            EditorGUILayout.LabelField("EV", pokemon.Hp.effort.ToString(), style);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Attack", pokemon.Attack.baseStat.ToString(), style);
            EditorGUILayout.LabelField("EV", pokemon.Attack.effort.ToString(), style);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Defense", pokemon.Defense.baseStat.ToString(), style);
            EditorGUILayout.LabelField("EV", pokemon.Defense.effort.ToString(), style);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Special Attack", pokemon.SpecialAttack.baseStat.ToString(), style);
            EditorGUILayout.LabelField("EV", pokemon.SpecialAttack?.effort.ToString(), style);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Special Defense", pokemon.SpecialDefense.baseStat.ToString(), style);
            EditorGUILayout.LabelField("EV", pokemon.SpecialDefense.effort.ToString(), style);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Speed", pokemon.Speed.baseStat.ToString(), style);
            EditorGUILayout.LabelField("EV", pokemon.Speed.effort.ToString(), style);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", style);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Ability", pokemon.Ability?.name, style);
            EditorGUILayout.LabelField("Description", pokemon.Ability?.description, style);
            showLongDescription = EditorGUILayout.Foldout(showLongDescription, "Long description", true);
            if (showLongDescription) {
                EditorGUILayout.LabelField(pokemon.Ability?.longDescription, style);
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", style);
            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}
