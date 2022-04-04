using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomPropertyDrawer(typeof(PokemonAttribute))]
public class PokemonDrawer : PropertyDrawer
{
    static float margin = 4f;
    static float height = 16f;

    private GUIStyle style;

    public PokemonDrawer() {
        style = new GUIStyle();
        style.normal.textColor = new Color(0.67f, 0.67f, 0.67f);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return (height*9)+(margin*2);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        Pokemon pokemon = (Pokemon)property.objectReferenceValue;
        Rect nextYPos = new Rect(position.x, position.y, position.width, height);
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.ObjectField(nextYPos, property); nextYPos.y += height + margin;
        if (pokemon == null) return;
        EditorGUI.LabelField(nextYPos, "Name", pokemon.name, style); nextYPos.y += height + margin;
        EditorGUI.LabelField(nextYPos, "HP", pokemon.hp.baseStat.ToString(), style); nextYPos.y += height + margin;
        EditorGUI.LabelField(nextYPos, "Attack", pokemon.attack.baseStat.ToString(), style); nextYPos.y += height + margin;
        EditorGUI.LabelField(nextYPos, "Defense", pokemon.defense.baseStat.ToString(), style); nextYPos.y += height + margin;
        EditorGUI.LabelField(nextYPos, "Special Attack", pokemon.specialAttack.baseStat.ToString(), style); nextYPos.y += height + margin;
        EditorGUI.LabelField(nextYPos, "Special Defense", pokemon.specialDefense.baseStat.ToString(), style); nextYPos.y += height + margin;
        EditorGUI.LabelField(nextYPos, "Speed", pokemon.speed.baseStat.ToString(), style);
        EditorGUI.EndProperty();
    }
}
