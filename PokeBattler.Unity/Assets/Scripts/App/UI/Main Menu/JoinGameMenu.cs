using PokeBattler.Client.Services;
using UnityEngine;
using Zenject;

namespace PokeBattler.Unity
{
    public class JoinGameMenu : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_InputField ipAddress;
        [SerializeField] private TMPro.TMP_InputField port;

        IMainMenuService mainMenuService;

        [Inject]
        public void Construct(IMainMenuService mainMenuService)
        {
            this.mainMenuService = mainMenuService;
        }

        public void JoinGame() => mainMenuService.JoinGame(ipAddress.text, port.text);
    }
}
