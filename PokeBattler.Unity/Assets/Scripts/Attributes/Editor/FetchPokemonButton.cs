using PokeBattler.Unity;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PokemonBehaviour))]
public class FetchPokemonButton : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        PokemonBehaviour pokemonBehaviour = (PokemonBehaviour)target;
        if (GUILayout.Button("Fetch from PokemonGO API"))
            pokemonBehaviour.Initialize();
    }
}
