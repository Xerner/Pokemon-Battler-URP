using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using PokeBattler.Core;

namespace PokeBattler.Unity {
    public class ImageSelector : MonoBehaviour
    {
        [Tooltip("Adjust the pixels per unit for the created sprites by modifying the Reference Pixels per unit in the Canvas Scaler")]
        [SerializeField] StaticAssets.StaticAssetTypes assetType;
        List<string> keys;
        Dictionary<string, Sprite> sprites;
        [SerializeField] string activeSprite;
        [SerializeField] Image image;
        [SerializeField] TextMeshProUGUI imageName;
        public Vector2 Pivot = new Vector2(0.5f, 0.5f);

        public string ActiveSprite { get => activeSprite; }

        void OnEnable()
        {
            image.SetNativeSize();
        }

        void Start()
        {
            sprites = StaticAssets.EnumToSpriteDict(assetType);
            keys = sprites.Keys.ToList();
            if (keys.Count == 0) throw new Exception("No assets for ImageSelector to use!");
            image.SetNativeSize();
            SetSprite(keys[0]);
        }

        public void SetSprite(string name)
        {
            if (keys.IndexOf(name) < keys.Count)
                image.sprite = sprites[name];
            else
                Debug.Log("Index out of bounds");
            image.SetNativeSize();
            activeSprite = name;
            imageName.text = name;
        }

        public Sprite GetSprite() => image.sprite;

        public void NextSprite()
        {
            SetSprite(keys[Mathf.Clamp(keys.IndexOf(activeSprite) + 1, 0, keys.Count - 1)]);
        }

        public void PreviousSprite()
        {
            SetSprite(keys[Mathf.Clamp(keys.IndexOf(activeSprite) - 1, 0, keys.Count - 1)]);
        }
    }
}
