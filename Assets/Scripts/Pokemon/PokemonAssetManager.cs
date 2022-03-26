using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Pokemon;

public class PokemonAssetManager : MonoBehaviour {
    private static readonly string typesFilepath = "Assets/Resources/Graphics/Pokemon/Types/Sprites/";
    private static Dictionary<PokemonType, Texture2D> typeSprites = new Dictionary<PokemonType, Texture2D>();
    private static Dictionary<PokemonType, Texture2D> typeSpritePaths = new Dictionary<PokemonType, Texture2D>();

    //public void Start() {
    //    foreach (PokemonType type in Enum.GetValues(typeof(PokemonType))) {
    //        typeSprites.Add(type, Resources.Load<Texture2D>(typesFilepath + type.ToString() + ".png"));
    //    }
    //}

    public static Texture2D TypeToTexture(PokemonType type) {
        if (!typeSprites.ContainsKey(type))
            typeSprites.Add(type, Resources.Load<Texture2D>(typesFilepath + type.ToString() + ".png"));
        return typeSprites[type];
    }

    public static string TypeToPath(int type) => TypeToPath((PokemonType)type);

    public static string TypeToPath(PokemonType type) {
        switch (type) {
            case PokemonType.Mystery:   return typesFilepath + "Mystery.png";
            case PokemonType.Bug:       return typesFilepath + "Bug.png";
            case PokemonType.Dark:      return typesFilepath + "Dark.png";
            case PokemonType.Dragon:    return typesFilepath + "Dragon.png";
            case PokemonType.Electric:  return typesFilepath + "Electric.png";
            case PokemonType.Fairy:     return typesFilepath + "Fairy.png";
            case PokemonType.Fighting:  return typesFilepath + "Fighting.png";
            case PokemonType.Fire:      return typesFilepath + "Fire.png";
            case PokemonType.Flying:    return typesFilepath + "Flying.png";
            case PokemonType.Ghost:     return typesFilepath + "Ghost.png";
            case PokemonType.Grass:     return typesFilepath + "Grass.png";
            case PokemonType.Ground:    return typesFilepath + "Ground.png";
            case PokemonType.Ice:       return typesFilepath + "Ice.png";
            case PokemonType.Normal:    return typesFilepath + "Normal.png";
            case PokemonType.Poison:    return typesFilepath + "Poison.png";
            case PokemonType.Psychic:   return typesFilepath + "Psychic.png";
            case PokemonType.Rock:      return typesFilepath + "Rock.png";
            case PokemonType.Steel:     return typesFilepath + "Steel.png";
            case PokemonType.Water:     return typesFilepath + "Water.png";
            default:                    return typesFilepath + "Mystery.png";
        }
    }
}