using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageSelector : MonoBehaviour
{
    [Tooltip("Adjust the pixels per unit for the created sprites by modifying the Reference Pixels per unit in the Canvas Scaler")]
    [SerializeField] private Texture2D[] images;
    [SerializeField] private int activeIndex;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI imageName;
    public Vector2 Pivot = new Vector2(0.5f, 0.5f);
    private Sprite[] sprites;
    
    public int ActiveIndex { get => activeIndex; }

    private void OnEnable()
    {
        image.SetNativeSize();
    }

    private void Start()
    {
        if (images.Length == 0) throw new Exception("Trainer Texture2D array is empty. It should be filled with all the Trainer images");
        sprites = new Sprite[images.Length];
        for (int i = 0; i < images.Length; i++)
        {
            Texture2D trainer = images[i];
            sprites[i] = Sprite.Create(trainer, new Rect(0.0f, 0.0f, trainer.width, trainer.height), Pivot, 1f);
        }
        image.SetNativeSize();
        SetSprite(ActiveIndex);
    }

    public void SetSprite(int index)
    {
        if (index < sprites.Length) image.sprite = sprites[index];
        else Debug.Log("Index out of bounds");
        image.SetNativeSize();
        activeIndex = index;
        imageName.text = images[index].name;
    }

    public Sprite GetSprite() => image.sprite;

    public void NextSprite()
    {
        SetSprite(Mathf.Clamp(ActiveIndex + 1, 0, sprites.Length - 1));
    }

    public void PreviousSprite()
    {
        SetSprite(Mathf.Clamp(ActiveIndex - 1, 0, sprites.Length - 1));
    }
}
