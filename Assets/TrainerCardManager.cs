using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TrainerCardManager : SingletonBehaviour<TrainerCardManager>
{
    public GameObject TrainerCardPrefab; // set in the editor
    public Transform VerticalLayoutGroup; // set in the editor

    public List<TrainerCard> TrainerCards = new List<TrainerCard>();
    private Dictionary<ulong, int> trainerCardIndexes = new Dictionary<ulong, int>();

    /// <summary>
    /// Using a players account settings, a new trainer is added to the list of trainer cards
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="clientID">The clientID from the NetworkManager</param>
    public void AddTrainer(AccountSettings settings, ulong clientID)
    {
        TrainerCard trainerCard = Instantiate(TrainerCardPrefab, VerticalLayoutGroup).GetComponent<TrainerCard>();
        Sprite trainerSprite = AssetManager.Singleton.Trainers[settings.TrainerSpriteID];
        Sprite trainerBackgroundSprite = AssetManager.Singleton.TrainerBackgrounds[settings.TrainerBackgroundID];
        trainerCardIndexes.Add(clientID, transform.childCount);
        trainerCard.Initialize(settings.Username, trainerSprite, trainerBackgroundSprite);
    }

    /// <summary>
    /// Removes the player from the list of trainer cards
    /// </summary>
    /// <param name="clientID"></param>
    public void RemoveTrainer(ulong clientID)
    {
        TrainerCards.RemoveAt(trainerCardIndexes[clientID]);
        trainerCardIndexes.Remove(clientID);
    }
}
