using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using JsonModel;

[CustomPropertyDrawer(typeof(GetPokemonAttribute))]
public class GetPokemonDrawer : PropertyDrawer
{
    private string userInput = "";

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return 40f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        Rect labelPos = new Rect(position.x, position.y, position.width, 16f);
        Rect textPos = new Rect(position.x+ position.width * 0.4f, position.y, position.width * 0.6f, 16f);
        Rect buttonPos = new Rect(position.x, position.y + labelPos.height + 4, position.width, 16f);

        EditorGUI.BeginProperty(position, label, property);
        //EditorGUI.LabelField(labelPos, label, new GUIContent(property.stringValue));
        userInput = EditorGUI.TextField(labelPos, "Pokemon ID or Name", userInput);
        if (GUI.Button(buttonPos, "Fetch Pokemon API")) {
            GetPokemonAttribute attr = (GetPokemonAttribute)attribute;
            setPokemonProperty(userInput);
        }
        EditorGUI.EndProperty();
    }

    private void setPokemonProperty(string userInput) {
        if (userInput.All(char.IsDigit) || Pokemon.GetValidPokemonName(userInput) != "") {
            WebRequests.Get<string>(
                $"https://pokeapi.co/api/v2/pokemon/{userInput}/", 
                (error) => Debug.LogError(error), 
                (json) => Debug.Log(PokemonJsonModel.FromJson(json).name)
            );
        } else {
            Debug.LogError("Invalid pokemon ID or name given: " + userInput);
        }
    }
}
