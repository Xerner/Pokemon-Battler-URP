using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Pokemon;

public class PokemonAssetManager : MonoBehaviour {
    private static readonly string typesFilepath = "Assets/Resources/Graphics/Pokemon/Types/Sprites/";
    private static Dictionary<EPokemonType, Texture2D> typeSprites = new Dictionary<EPokemonType, Texture2D>();
    private static Dictionary<EPokemonType, Texture2D> typeSpritePaths = new Dictionary<EPokemonType, Texture2D>();

    //public void Start() {
    //    foreach (PokemonType type in Enum.GetValues(typeof(PokemonType))) {
    //        typeSprites.Add(type, Resources.Load<Texture2D>(typesFilepath + type.ToString() + ".png"));
    //    }
    //}

    public static Texture2D TypeToTexture(EPokemonType type) {
        if (!typeSprites.ContainsKey(type))
            typeSprites.Add(type, Resources.Load<Texture2D>(typesFilepath + type.ToString() + ".png"));
        return typeSprites[type];
    }

    public static string TypeToPath(int type) => TypeToPath((EPokemonType)type);

    public static string TypeToPath(EPokemonType type) {
        switch (type) {
            case EPokemonType.Mystery:   return typesFilepath + "Mystery.png";
            case EPokemonType.Bug:       return typesFilepath + "Bug.png";
            case EPokemonType.Dark:      return typesFilepath + "Dark.png";
            case EPokemonType.Dragon:    return typesFilepath + "Dragon.png";
            case EPokemonType.Electric:  return typesFilepath + "Electric.png";
            case EPokemonType.Fairy:     return typesFilepath + "Fairy.png";
            case EPokemonType.Fighting:  return typesFilepath + "Fighting.png";
            case EPokemonType.Fire:      return typesFilepath + "Fire.png";
            case EPokemonType.Flying:    return typesFilepath + "Flying.png";
            case EPokemonType.Ghost:     return typesFilepath + "Ghost.png";
            case EPokemonType.Grass:     return typesFilepath + "Grass.png";
            case EPokemonType.Ground:    return typesFilepath + "Ground.png";
            case EPokemonType.Ice:       return typesFilepath + "Ice.png";
            case EPokemonType.Normal:    return typesFilepath + "Normal.png";
            case EPokemonType.Poison:    return typesFilepath + "Poison.png";
            case EPokemonType.Psychic:   return typesFilepath + "Psychic.png";
            case EPokemonType.Rock:      return typesFilepath + "Rock.png";
            case EPokemonType.Steel:     return typesFilepath + "Steel.png";
            case EPokemonType.Water:     return typesFilepath + "Water.png";
            default:                    return typesFilepath + "Mystery.png";
        }
    }
}