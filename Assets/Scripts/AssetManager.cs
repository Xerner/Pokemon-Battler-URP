using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AssetManager : SingletonBehaviour<AssetManager>
{
    #region Pokemon stuff
    public static Dictionary<string, GameObject> PokemonPrefabs;
    public static Dictionary<string, PokemonBehaviour> Pokemon;
    public static string[] PokemonNames;
    public static Dictionary<string, Sprite> PokemonSprites;
    public static Dictionary<string, PokemonTypeSprites> PokemonTypesSprites = new Dictionary<string, PokemonTypeSprites>();
    #endregion
    private static readonly StringBuilder str = new StringBuilder();

    public Sprite[] Trainers;
    public Sprite[] TrainerBackgrounds;
    public Sprite[] ShopCardSprites; // Initialized in the editor

    private new void Start() {
        base.Start();
        //PokemonPrefabs = LoadResource<GameObject>("Prefabs/Pokemon", "*.prefab");
        //Pokemon = new Dictionary<string, Pokemon>();
        //foreach (string key in PokemonPrefabs.Keys)
        //{
        //    Pokemon.Add(PokemonPrefabs[key].name, PokemonPrefabs[key].GetComponent<Pokemon>());
        //}
        //PokemonNames = new string[PokemonPrefabs.Count];
        //PokemonPrefabs.Keys.CopyTo(PokemonNames, 0);
        //PokemonSprites = LoadResource<Sprite>("Graphics/Pokemon/Sprites", "*.gif");
        //Dictionary<string, Sprite> typeSprites = LoadResource<Sprite>("Graphics/Pokemon/Types/Sprites", "*.png");
        //Dictionary<string, Sprite> typeMiniSprites = LoadResource<Sprite>("Graphics/Pokemon/Types/Mini Sprites", "*.png");
        //Dictionary<string, Sprite> typeEffectSprites = LoadResource<Sprite>("Graphics/Pokemon/Types/Type Effect Boxes", "*.png");
        //Dictionary<string, Sprite> typeMoveSprites = LoadResource<Sprite>("Graphics/Pokemon/Types/Type Move Boxes", "*.png");

        //foreach (string key in typeSprites.Keys)
        //{
        //    //Debug.Log(key);
        //    PokemonTypesSprites.Add(key, new PokemonTypeSprites(
        //        typeSprites[key],
        //        typeMiniSprites[key],
        //        typeEffectSprites[key],
        //        typeMoveSprites[key]
        //    ));
        //}
    }

    private static Dictionary<string, T> LoadResource<T>(string resourceFolderName, string searchPattern) where T : Object
    {
        Dictionary<string, T> dict = new Dictionary<string, T>();
        str.Clear();
        str.Append("Assets/Resources/");
        str.Append(resourceFolderName);
        // TODO change to Resources.LoadAll
        foreach (string file in Directory.EnumerateFiles(str.ToString(), searchPattern))
        {
            string filename = Path.GetFileNameWithoutExtension(file);
            str.Clear();
            str.Append(resourceFolderName);
            str.Append("/");
            str.Append(filename);
            dict.Add(filename, Resources.Load<T>(str.ToString()));
        }
        return dict;
    }
}
