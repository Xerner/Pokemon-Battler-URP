using PokeBattler.Client.Services;
using PokeBattler.Common;
using PokeBattler.Common.Services;
using PokeBattler.Common.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using PokeBattler.Client.Controllers;

namespace PokeBattler.Unity
{
    public class MainMenu : MonoInstaller<MainMenu>
    {
        public static MainMenu Instance { get; private set; }

        private GameController gameController;
        private IClientService clientService;

        [Inject]
        public void Construct(GameController gameController, IClientService clientService)
        {
            this.gameController = gameController;
            this.clientService = clientService;
        }

        public void CreateGame()
        {
            gameController.RequestCreateGame();
        }

        public void JoinGame(string ipAddress, string port)
        {
            // port validation
            if (!int.TryParse(port, out int port_int) && (port_int < 1 || port_int > ushort.MaxValue))
            {
                UIWindowManagerBehaviour.Instance.CreatePopupMessage($"Invalid port\n\nPorts are only digits and no larger than {ushort.MaxValue}", 170f);
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
            Account account = SaveSystem.LoadAccount(username.Trim().ToLower());
            if (account == null)
            {
                UIWindowManagerBehaviour.Instance.CreatePopupMessage("Trainer not found");
            }
            else
            {
                Debug.Log("Loading trainer '" + username.Trim().ToLower() + "' with Settings\n" + account);
                ViewManager.Instance.ChangeViews(nextMenu);
                clientService.Account = account;
            }
        }

        /// <summary>Save username, trainer sprite, and trainer background sprite to the account file</summary>
        public void SaveSettings(string username, string trainerSpriteName, string trainerBackgroundName)
        {
            clientService.Account = new Account()
            {
                Username = username,
                TrainerSpriteId = trainerSpriteName,
                TrainerBackgroundId = trainerBackgroundName,
                Id = clientService.Account.Id,
                GameId = clientService.Account.GameId
            };
            SaveSystem.SaveAccount(clientService.Account);
        }
    }
}
