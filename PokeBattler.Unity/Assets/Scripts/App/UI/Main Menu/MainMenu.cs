using PokeBattler.Core;
using PokeBattler.Models;
using PokeBattler.Services;
using UnityEngine;
using Zenject;

namespace PokeBattler.Unity
{
    public class MainMenu : MonoBehaviour
    {
        public static MainMenu Instance { get; private set; }

        private GameService gameService;
        private ClientService clientService;

        [Inject]
        public void Construct(GameService gameService, ClientService clientService)
        {
            this.gameService = gameService;
            this.clientService = clientService;
        }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            else
            {
                Instance = this;
            }
        }

        public void CreateGame() => gameService.CreateGame();

        public void JoinGame(string ipAddress, string port)
        {
            // port validation
            if (!int.TryParse(port, out int port_int) && (port_int < 1 || port_int > 65535))
            {
                UIWindowManagerBehaviour.Instance.CreatePopupMessage("Invalid port\n\nPorts are only digits and no larger than 65535", 170f);
                return;
            }
            // TODO
            // gameService.JoinGame(ipAddress, port_int);
        }

        /// <summary>
        /// Attempts to load the Settings of the given username.<br/>
        /// On success: sends the user to the main menu<br/>
        /// On fail: shows a popup message
        /// </summary>
        public void LoadSettings(string username, GameObject nextMenu)
        {
            AccountSettings settings = SaveSystem.LoadAccount(username.Trim().ToLower());
            if (settings == null)
            {
                UIWindowManagerBehaviour.Instance.CreatePopupMessage("Trainer not found");
            }
            else
            {
                Debug.Log("Loading trainer '" + username.Trim().ToLower() + "' with Settings\n" + settings);
                ViewManager.Instance.ChangeViews(nextMenu);
                clientService.Account.Settings = settings;
            }
        }

        /// <summary>Save username, trainer sprite, and trainer background sprite to the account file</summary>
        public void SaveSettings(string username, string trainerSpriteName, string trainerBackgroundName)
        {
            clientService.Account.Settings = new AccountSettings(
                username,
                trainerSpriteName,
                trainerBackgroundName,
                clientService.Account.Settings.ClientID,
                clientService.Account.Settings.GameID);
            SaveSystem.SaveAccount(clientService.Account.Settings);
        }
    }
}
