using PokeBattler.Core;
using PokeBattler.Models;
using PokeBattler.Core;
using UnityEditor.PackageManager;
using UnityEngine;
using Zenject;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Round Timer")]
    public class RoundTimerBehaviour : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI text;

        private GameService gameService;

        [Inject]
        public void Construct(GameService gameService)
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
