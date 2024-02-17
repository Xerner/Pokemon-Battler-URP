using PokeBattler.Client.Services;
using UnityEngine;
using Zenject;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Main Menu/Join Game View")]
    public class JoinGameViewBehaviour : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_InputField ipAddress;
        [SerializeField] private TMPro.TMP_InputField port;

        IGameService gameService;

        [Inject]
        public void Construct(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public void JoinGame() => gameService.JoinGame(ipAddress.text, port.text);
    }
}
