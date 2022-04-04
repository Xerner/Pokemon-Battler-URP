using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomPropertyDrawer(typeof(PokemonAttribute))]
public class PokemonDrawer : PropertyDrawer {
    static float margin = 4f;
    static float height = 16f;

    private GUIStyle style;

    public PokemonDrawer() {
        style = new GUIStyle();
        style.normal.textColor = new Color(0.67f, 0.67f, 0.67f);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return (height * 9) + (margin * 2);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        Pokemon pokemon = (Pokemon)property.objectReferenceValue;
        //EditorGUI.BeginProperty(position, label, property);
        Rect nextYPos = new Rect(position.x, position.y, position.width, height);
        EditorGUI.ObjectField(nextYPos, property); 
        if (pokemon == null) return;
        nextYPos = new Rect(nextYPos.x, nextYPos.y += height + margin, nextYPos.width, height);
        EditorGUI.LabelField(nextYPos, "Name", pokemon.name, style);

        #region Stats

        nextYPos = new Rect(nextYPos.x, nextYPos.y += height + margin, position.width / 2, height);
        EditorGUI.LabelField(nextYPos, "HP", pokemon.hp.baseStat.ToString(), style);
        nextYPos = new Rect(nextYPos.x + position.width / 2, nextYPos.y, position.width / 2, height);
        EditorGUI.LabelField(nextYPos, "EV", pokemon.hp.effort.ToString(), style);
        
        nextYPos = new Rect(position.x, nextYPos.y += height + margin, position.width, height);
        EditorGUI.LabelField(nextYPos, "Attack", pokemon.attack.baseStat.ToString(), style);
        nextYPos = new Rect(nextYPos.x + position.width / 2, nextYPos.y, position.width / 2, height);
        EditorGUI.LabelField(nextYPos, "EV", pokemon.attack.effort.ToString(), style);

        nextYPos = new Rect(position.x, nextYPos.y += height + margin, position.width, height);
        EditorGUI.LabelField(nextYPos, "Defense", pokemon.defense.baseStat.ToString(), style);
        nextYPos = new Rect(nextYPos.x + position.width / 2, nextYPos.y, position.width / 2, height);
        EditorGUI.LabelField(nextYPos, "EV", pokemon.defense.effort.ToString(), style);

        nextYPos = new Rect(position.x, nextYPos.y += height + margin, position.width, height);
        EditorGUI.LabelField(nextYPos, "Special Attack", pokemon.specialAttack.baseStat.ToString(), style);
        nextYPos = new Rect(nextYPos.x + position.width / 2, nextYPos.y, position.width / 2, height);
        EditorGUI.LabelField(nextYPos, "EV", pokemon.specialAttack.effort.ToString(), style);

        nextYPos = new Rect(position.x, nextYPos.y += height + margin, position.width, height);
        EditorGUI.LabelField(nextYPos, "Special Defense", pokemon.specialDefense.baseStat.ToString(), style);
        nextYPos = new Rect(nextYPos.x + position.width / 2, nextYPos.y, position.width / 2, height);
        EditorGUI.LabelField(nextYPos, "EV", pokemon.specialDefense.effort.ToString(), style);

        nextYPos = new Rect(position.x, nextYPos.y += height + margin, position.width, height);
        EditorGUI.LabelField(nextYPos, "Speed", pokemon.speed.baseStat.ToString(), style);
        nextYPos = new Rect(nextYPos.x + position.width / 2, nextYPos.y, position.width / 2, height);
        EditorGUI.LabelField(nextYPos, "EV", pokemon.speed.effort.ToString(), style);

        #endregion

        //EditorGUI.EndProperty();
    }
}
