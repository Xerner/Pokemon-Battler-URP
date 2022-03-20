using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TrainerCard : MonoBehaviour
{
    public RectTransform HealthBar;
    [HideInInspector] private HealthBar health;

    [SerializeField] private Image readyOrNot;
    [SerializeField] private TextMeshProUGUI level;

    [Header("Trainer Setting Specific")]
    [SerializeField] private TextMeshProUGUI username;
    [SerializeField] private Image trainerBackground;
    [SerializeField] private Image trainerSprite;


    [Header("Graphics")]
    [SerializeField] private Sprite ready;
    [SerializeField] private Sprite notReady;

    public HealthBar Health { get; private set; }

    private void Start()
    {
        level.text = "1";
        health = new HealthBar(HealthBar);
    }

    /// <summary>
    /// Initializes the needed assets for the UI
    /// </summary>
    /// <param name="trainerSprite"></param>
    /// <param name="trainerBackground"></param>
    public void Initialize(string username, Sprite trainerSprite, Sprite trainerBackground)
    {
        this.username.text = username;
        this.trainerSprite.sprite = trainerSprite;
        this.trainerBackground.sprite = trainerBackground;
    }

    public void Ready() => readyOrNot.sprite = ready;

    public void NotReady() => readyOrNot.sprite = notReady;

    public void LevelUp() => level.text = (level.text.ToIntArray()[0] + 1).ToString();
}
