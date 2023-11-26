using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PokemonTypeIconAttribute))]
public class PokemonTypeIconDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        //float originalWidth = position.width;
        //float iconWidth = position.height * (64 / 28); // the width/height ratio of the images
        //position.width = iconWidth;
        ////GUI.DrawTexture(position, EditorGUIUtility.Load(PokemonAssetManager.TypeToPath(property.intValue)) as Texture2D);
        //position.width = originalWidth - position.height - 5;
        //position.x += iconWidth + 5;
        //EditorGUI.PropertyField(position, property, label);
    }
}
