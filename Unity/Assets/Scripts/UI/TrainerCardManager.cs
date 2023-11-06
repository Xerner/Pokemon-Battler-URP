using UnityEngine;

// TODO: Try and refactor this class into a normal class and a MonoBehaviour class
public class TrainerCardManager : MonoBehaviour {
    public static TrainerCardManager Instance { get; private set; }

    [SerializeField] UnityEngine.GameObject TrainerCardPrefab; // set in the editor
    [SerializeField] Transform VerticalLayoutGroup; // set in the editor

    public TrainerCard[] TrainerCards = new TrainerCard[8];

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        } else {
            Instance = this;
        }
    }

    void Start() {
        for (int i = 0; i < TrainerCards.Length; i++) {
            TrainerCard trainerCard = Instantiate(TrainerCardPrefab, VerticalLayoutGroup).GetComponent<TrainerCard>();
            trainerCard.Initialize("", null, null);
            TrainerCards[i] = trainerCard;
        }
    }

    /// <summary>Using a players account Settings, a new trainer is added to the list of trainer cards</summary>
    /// <param name="clientID">The clientID from the NetworkManager</param>
    public void AddTrainerCard(Trainer trainer) {
        TrainerCard trainerCard = Instantiate(TrainerCardPrefab, VerticalLayoutGroup).GetComponent<TrainerCard>();
        Sprite trainerSprite = StaticAssets.Trainers[trainer.Account.Settings.TrainerSpriteId];
        Sprite trainerBackgroundSprite = StaticAssets.TrainerBackgrounds[trainer.Account.Settings.TrainerBackgroundId];
        trainerCard.Initialize(trainer.Account.Settings.Username, trainerSprite, trainerBackgroundSprite);
    }
}
