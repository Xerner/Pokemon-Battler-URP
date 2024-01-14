using PokeBattler.Client.Services;
using PokeBattler.Common.Models;
using PokeBattler.Common.Services;
using PokeBattler.Unity;
using UnityEngine;

namespace PokeBattler.Client.Services
{
    public interface IMainMenuService
    {
        public void CreateGame();
        public void JoinGame(string ipAddress, string port);
        public Account LoadSettings(string username, GameObject nextMenu);
        public void SaveSettings(string username, string trainerSpriteName, string trainerBackgroundName);
    }

    public class MainMenuService : IMainMenuService
    {
        readonly IClientService clientService;
        readonly IGameService gameService;

        public MainMenuService(IClientService clientService, IGameService gameService)
        {
            this.clientService = clientService;
            this.gameService = gameService;
        }

        public void CreateGame()
        {
            gameService.CreateGame();
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
        public Account LoadSettings(string username, GameObject nextMenu)
        {
            Account account = SaveSystem.LoadAccount(username.Trim().ToLower());
            if (account == null)
            {
                UIWindowManagerBehaviour.Instance.CreatePopupMessage("Trainer not found");
                return account;
            }
            Debug.Log("Loading trainer '" + username.Trim().ToLower() + "' with Settings\n" + account);
            ViewManager.Instance.ChangeViews(nextMenu);
            clientService.Account = account;
            return account;
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
