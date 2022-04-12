using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StaticAssets
{
    #region Sprite dictionaries
    // Pokemon
    public static Dictionary<string, Sprite> typeSprites = LoadResource<Sprite>("Graphics/Pokemon/Types/Sprites", "*.png");
    public static Dictionary<string, Sprite> typeMiniSprites = LoadResource<Sprite>("Graphics/Pokemon/Types/Mini Sprites", "*.png");
    public static Dictionary<string, Sprite> typeEffectSprites = LoadResource<Sprite>("Graphics/Pokemon/Types/Type Effect Boxes", "*.png");
    public static Dictionary<string, Sprite> typeMoveSprites = LoadResource<Sprite>("Graphics/Pokemon/Types/Type Move Boxes", "*.png");
    // Trainer
    public static Dictionary<string, Sprite> Trainers = LoadResource<Sprite>("Graphics/Trainers/Sprites", "*.png");
    public static Dictionary<string, Sprite> TrainerBackgrounds = LoadResource<Sprite>("Graphics/Trainers/Backgrounds", "*.png");
    public static Dictionary<string, Sprite> ShopCardSprites = LoadResource<Sprite>("Graphics/Shop Cards", "*.png");
    #endregion

    static Dictionary<string, T> LoadResource<T>(string resourceFolderName, string searchPattern) where T : UnityEngine.Object {
        Dictionary<string, T> dict = new Dictionary<string, T>();
        // maybe TODO change to Resources.LoadAll
        foreach (string file in Directory.EnumerateFiles("Assets/Resources/"+ resourceFolderName, searchPattern))
        {
            string filename = Path.GetFileNameWithoutExtension(file);
            dict.Add(filename, Resources.Load<T>(resourceFolderName + "/" +filename));
        }
        return dict;
    }

    public static Dictionary<string, Sprite> EnumToSpriteDict(StaticAssetTypes assets) {
        switch (assets) {
            case StaticAssetTypes.TypeSprites:
                return typeSprites;
            case StaticAssetTypes.TypeMiniSprites:
                return typeMiniSprites;
            case StaticAssetTypes.TypeEffectSprites:
                return typeEffectSprites;
            case StaticAssetTypes.TypeMoveSprites:
                return typeMoveSprites;
            case StaticAssetTypes.Trainers:
                return Trainers;
            case StaticAssetTypes.TrainerBackgrounds:
                return TrainerBackgrounds;
            case StaticAssetTypes.ShopCardSprites:
                return ShopCardSprites;
            default:
                return null;
        }
    }

    /// <summary>
    /// Used to set resource source from Unity Editor. Example can be seen in ImageSelector onStart()
    /// </summary>
    public enum StaticAssetTypes {
        TypeSprites,
        TypeMiniSprites,
        TypeEffectSprites,
        TypeMoveSprites,
        Trainers,
        TrainerBackgrounds,
        ShopCardSprites
    }
}
