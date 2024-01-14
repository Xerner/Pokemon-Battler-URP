using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Common;
using PokeBattler.Common.Models;

namespace PokeBattler.Client.Services
{
    public interface IGameService
    {
        public Game Game { get; }
        public Action<Game> OnGameCreated { get; set; }
        public Task CreateGame();
        public Task JoinGame(Guid gameID, Account account);
    }

    public class GameService : IGameService
    {
        readonly HubConnection connection;
        readonly IAppConfig appConfig;
        readonly IClientService clientService;

        public Game Game { get; set; }
        public Action<Game> OnGameCreated { get; set; }

        public GameService(HubConnection connection, 
                           IAppConfig appConfig,
                           IClientService clientService)
        {
            this.connection = connection;
            this.appConfig = appConfig;
            this.clientService = clientService;
        }

        public async Task CreateGame()
        {
            var game = await connection.InvokeAsync<Game>("CreateGame");
            await JoinGame(game.Id, clientService.Account);
        }

        public async Task JoinGame(Guid gameID, Account account)
        {
            var game = await connection.InvokeAsync<Game>("JoinGame");
            Game = game;
            if (SceneManager.GetActiveScene().name != appConfig.ArenaSceneName)
            {
                SceneManager.LoadScene(appConfig.ArenaSceneName);
            }
        }
    }
}
