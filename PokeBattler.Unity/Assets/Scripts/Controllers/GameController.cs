using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Client.Services;
using PokeBattler.Common;
using PokeBattler.Common.Models;
using UnityEngine.SceneManagement;

namespace PokeBattler.Client.Controllers
{
    public class GameController
    {
        readonly HubConnection connection;
        private IAppConfig appConfig;

        public GameController(HubConnection connection, IGameService gameService, IAppConfig appConfig)
        {
            this.connection = connection;
            this.appConfig = appConfig;
            connection.On("AddToGame", (Game game) => gameService.OnGameCreated?.Invoke(game));
        }

        public void RequestCreateGame()
        {
            connection.InvokeAsync("CreateGame");
        }

        public void GameCreated()
        {
            SceneManager.LoadScene(appConfig.ArenaSceneName);
        }
    }
}