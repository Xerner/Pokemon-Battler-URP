using PokeBattler.Client.Services;
using PokeBattler.Common.Models;
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


        private IGameService gameService;

        [Inject]
        public void Construct(IGameService gameService)
        {
            this.gameService = gameService;
        }

        void Start()
        {
            gameService.Game.RoundTimer.OnComplete += OnTimerComplete;
            gameService.Game.RoundTimer.OnStart += OnTimerStart;
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
            if (gameService.Game is null) return;
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
