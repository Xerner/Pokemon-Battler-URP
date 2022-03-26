using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "Pokemon/Pokemon")]
public class Pokemon : ScriptableObject
{
    [Header("Shop")]
    public Sprite shopSprite;

    [Header("General")]
    [KevinCastejon.EditorToolbox.Icon("Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png")]
    public PokemonType type1;
    [KevinCastejon.EditorToolbox.Icon("Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png")]
    public PokemonType type2;
    [SerializeField] private PokemonBehaviour evolution;
    [SerializeField] private PokemonBehaviour baseEvolution;
    public int EvolutionStage;
    public int tier;

    public PokemonBehaviour Evolution { get; private set; }

    public Texture2D TypeToTexture(PokemonType type) {
        switch (type) {
            case PokemonType.Mystery:
                return  Resources. "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Bug:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Dark:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Dragon:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Electric:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Fairy:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Fighting:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Fire:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Flying:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Ghost:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Grass:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Ground:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Ice:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Normal:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Poison:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Psychic:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Rock:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Steel:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            case PokemonType.Water:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
            default:
                return "Assets/Resources/Graphics/Pokemon/Types/Sprites/Fire.png";
        }
    }

    public enum PokemonType {
        Mystery,
        Bug,
        Dark,
        Dragon,
        Electric,
        Fairy,
        Fighting,
        Fire.png",
        Flying,
        Ghost,
        Grass,
        Ground,
        Ice,
        Normal,
        Poison,
        Psychic,
        Rock,
        Steel,
        Water
    }
}


