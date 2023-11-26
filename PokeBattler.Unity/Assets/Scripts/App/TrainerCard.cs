using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokeBattler.Unity
{
    [RequireComponent(typeof(RectTransform))]
    public class TrainerCard : MonoBehaviour
    {
        public RectTransform HealthBar;
        [HideInInspector] ResourceBar health;

        [SerializeField] Image readyOrNot;
        [SerializeField] TextMeshProUGUI level;

        [Header("Trainer Setting Specific")]
        [SerializeField] TextMeshProUGUI username;
        [SerializeField] Image trainerBackground;
        [SerializeField] Image trainerSprite;


        [Header("Graphics")]
        [SerializeField] Sprite ready;
        [SerializeField] Sprite notReady;

        public ResourceBar Health { get; private set; }

        void Start()
        {
            level.text = "1";
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
}
