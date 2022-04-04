using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PokemonBehaviour))]
public class FetchPokemonButton : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        PokemonBehaviour pokemonBehaviour = (PokemonBehaviour)target;
        if (GUILayout.Button("Fetch from Pokemon API"))
            pokemonBehaviour.Initialize();
    }
}