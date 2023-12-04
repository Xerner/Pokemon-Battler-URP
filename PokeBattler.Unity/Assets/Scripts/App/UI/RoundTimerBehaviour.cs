using PokeBattler.Client.Services;
using PokeBattler.Common.Models;
using UnityEngine;
using Zenject;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Round Timer")]
    public class RoundTimerBehaviour : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI text;

        private IGameService gameService;

        [Inject]
        public void Construct(IGameService gameService)
        {
            this.gameService = gameService;
        }

        void Start()
        {
            gameService.OnGameCreated += SubscribeToOnTick;
        }

        void SubscribeToOnTick(Game game)
        {
            game.RoundTimer.OnTick += UpdateText;
        }

        void UpdateText(float time)
        {
            text.text = FormatTime(time);
        }

        string FormatTime(float time)
        {
            return string.Format("{0:0.00}", time); ;
        }
    }
}
