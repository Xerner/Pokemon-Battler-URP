using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Pokemon;

public class PokemonAssetManager : MonoBehaviour {
    private static readonly string typesFilepath = "Assets/Resources/Graphics/Pokemon/Types/Sprites/";
    private static Dictionary<Pokemon.EPokemonType, Texture2D> typeSprites = new Dictionary<Pokemon.EPokemonType, Texture2D>();
    private static Dictionary<Pokemon.EPokemonType, Texture2D> typeSpritePaths = new Dictionary<Pokemon.EPokemonType, Texture2D>();

    //public void Start() {
    //    foreach (PokemonType type in Enum.GetValues(typeof(PokemonType))) {
    //        typeSprites.Add(type, Resources.Load<Texture2D>(typesFilepath + type.ToString() + ".png"));
    //    }
    //}

    public static Texture2D TypeToTexture(Pokemon.EPokemonType type) {
        if (!typeSprites.ContainsKey(type))
            typeSprites.Add(type, Resources.Load<Texture2D>(typesFilepath + type.ToString() + ".png"));
        return typeSprites[type];
    }

    public static string TypeToPath(int type) => TypeToPath((Pokemon.EPokemonType)type);

    public static string TypeToPath(Pokemon.EPokemonType type) {
        switch (type) {
            case Pokemon.EPokemonType.Mystery:   return typesFilepath + "Mystery.png";
            case Pokemon.EPokemonType.Bug:       return typesFilepath + "Bug.png";
            case Pokemon.EPokemonType.Dark:      return typesFilepath + "Dark.png";
            case Pokemon.EPokemonType.Dragon:    return typesFilepath + "Dragon.png";
            case Pokemon.EPokemonType.Electric:  return typesFilepath + "Electric.png";
            case Pokemon.EPokemonType.Fairy:     return typesFilepath + "Fairy.png";
            case Pokemon.EPokemonType.Fighting:  return typesFilepath + "Fighting.png";
            case Pokemon.EPokemonType.Fire:      return typesFilepath + "Fire.png";
            case Pokemon.EPokemonType.Flying:    return typesFilepath + "Flying.png";
            case Pokemon.EPokemonType.Ghost:     return typesFilepath + "Ghost.png";
            case Pokemon.EPokemonType.Grass:     return typesFilepath + "Grass.png";
            case Pokemon.EPokemonType.Ground:    return typesFilepath + "Ground.png";
            case Pokemon.EPokemonType.Ice:       return typesFilepath + "Ice.png";
            case Pokemon.EPokemonType.Normal:    return typesFilepath + "Normal.png";
            case Pokemon.EPokemonType.Poison:    return typesFilepath + "Poison.png";
            case Pokemon.EPokemonType.Psychic:   return typesFilepath + "Psychic.png";
            case Pokemon.EPokemonType.Rock:      return typesFilepath + "Rock.png";
            case Pokemon.EPokemonType.Steel:     return typesFilepath + "Steel.png";
            case Pokemon.EPokemonType.Water:     return typesFilepath + "Water.png";
            default:                    return typesFilepath + "Mystery.png";
        }
    }
}