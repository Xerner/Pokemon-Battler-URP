using Poke.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Poke.Unity
{
    [AddComponentMenu("Poke Battler/TimerPauseButton")]
    [RequireComponent(typeof(Button))]
    public class TimerPauseButtonBehaviour : MonoBehaviour
    {
        Button button;
        [SerializeField] Sprite PausedSprite;
        [SerializeField] Sprite ResumedSprite;

        void Start()
        {
            HostBehaviour.Instance.Host.OnGameCreated += (Game game) =>
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
            button.enabled = HostBehaviour.Instance.Host.Game.RoundTimer.IsComplete();
        }

        public void ToggleTimer()
        {
            HostBehaviour.Instance.Host.Game.RoundTimer.Toggle();
            if (HostBehaviour.Instance.Host.Game.RoundTimer.Paused)
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
