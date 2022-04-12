using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TrainerCardManager : MonoBehaviour
{
    public static TrainerCardManager Instance { get; private set; }

    [SerializeField] GameObject TrainerCardPrefab; // set in the editor
    [SerializeField] Transform VerticalLayoutGroup; // set in the editor

    public List<TrainerCard> TrainerCards = new List<TrainerCard>();
    private Dictionary<ulong, int> trainerCardIndexes = new Dictionary<ulong, int>();

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        } else {
            Instance = this;
        }
    }

    /// <summary>
    /// Using a players account settings, a new trainer is added to the list of trainer cards
    /// </summary>
    /// <param name="clientID">The clientID from the NetworkManager</param>
    public void AddTrainer(AccountSettings settings, ulong clientID)
    {
        TrainerCard trainerCard = Instantiate(TrainerCardPrefab, VerticalLayoutGroup).GetComponent<TrainerCard>();
        Sprite trainerSprite = StaticAssets.Trainers[settings.TrainerSpriteName];
        Sprite trainerBackgroundSprite = StaticAssets.TrainerBackgrounds[settings.TrainerBackgroundName];
        trainerCardIndexes.Add(clientID, transform.childCount);
        trainerCard.Initialize(settings.Username, trainerSprite, trainerBackgroundSprite);
    }

    /// <summary>
    /// Removes the player from the list of trainer cards
    /// </summary>
    public void RemoveTrainer(ulong clientID)
    {
        TrainerCards.RemoveAt(trainerCardIndexes[clientID]);
        trainerCardIndexes.Remove(clientID);
    }
}
