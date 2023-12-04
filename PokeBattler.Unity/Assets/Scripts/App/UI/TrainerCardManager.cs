using PokeBattler.Common;
using PokeBattler.Common.Models;
using UnityEngine;
using Zenject;

namespace PokeBattler.Unity
{
    // TODO: Try and refactor this class into a normal class and a MonoBehaviour class
    [AddComponentMenu("Poke Battler/Trainer Card Manager")]
    public class TrainerCardManager : MonoInstaller<TrainerCardManager>
    {
        [SerializeField] GameObject TrainerCardPrefab; // set in the editor
        [SerializeField] Transform VerticalLayoutGroup; // set in the editor

        public TrainerCard[] TrainerCards = new TrainerCard[8];

        public override void Start()
        {
            for (int i = 0; i < TrainerCards.Length; i++)
            {
                TrainerCard trainerCard = Instantiate(TrainerCardPrefab, VerticalLayoutGroup).GetComponent<TrainerCard>();
                trainerCard.Initialize("", null, null);
                TrainerCards[i] = trainerCard;
            }
        }

        /// <summary>Using a players account Settings, a new trainer is added to the list of trainer cards</summary>
        /// <param name="clientID">The clientID from the NetworkManager</param>
        public void AddTrainerCard(Trainer trainer)
        {
            TrainerCard trainerCard = Instantiate(TrainerCardPrefab, VerticalLayoutGroup).GetComponent<TrainerCard>();
            Sprite trainerSprite = StaticAssets.Trainers[trainer.Account.TrainerSpriteId];
            Sprite trainerBackgroundSprite = StaticAssets.TrainerBackgrounds[trainer.Account.TrainerBackgroundId];
            trainerCard.Initialize(trainer.Account.Username, trainerSprite, trainerBackgroundSprite);
        }
    }
}
