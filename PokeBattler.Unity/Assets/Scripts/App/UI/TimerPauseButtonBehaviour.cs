using PokeBattler.Core;
using PokeBattler.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/TimerPauseButton")]
    [RequireComponent(typeof(Button))]
    public class TimerPauseButtonBehaviour : MonoBehaviour
    {
        Button button;
        [SerializeField] Sprite PausedSprite;
        [SerializeField] Sprite ResumedSprite;


        private GameService gameService;

        [Inject]
        public void Construct(GameService gameService)
        {
            this.gameService = gameService;
        }


        void Start()
        {
            gameService.OnGameCreated += (Game game) =>
            {
                game.RoundTimer.OnComplete += OnTimerComplete;
                game.RoundTimer.OnStart += OnTimerStart;
            };
            button = GetComponent<Button>();
            button.onClick.AddListener(ToggleTimer);
        }

        void OnTimerComplete()
        {
            button.enabled = false;
            button.image.sprite = PausedSprite;
        }

        void OnTimerStart()
        {
            button.enabled = true;
            button.image.sprite = ResumedSprite;
        }

        void Update()
        {
            button.enabled = gameService.Game.RoundTimer.IsComplete();
        }

        public void ToggleTimer()
        {
            gameService.Game.RoundTimer.Toggle();
            if (gameService.Game.RoundTimer.Paused)
            {
                button.image.sprite = ResumedSprite;
            }
            else
            {
                button.image.sprite = PausedSprite;
            }
        }
    }
}
