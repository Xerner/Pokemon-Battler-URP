using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Common;
using PokeBattler.Common.Models;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace PokeBattler.Client.Services
{
    public interface IGameService
    {
        public Game Game { get; set; }
        public Action<Game> OnGameCreated { get; set; }
        public Task CreateGame();
    }

    public class GameService : IGameService
    {
        readonly HubConnection connection;
        readonly IAppConfig appConfig;

        public Game Game { get; set; }
        public Action<Game> OnGameCreated { get; set; }

        public GameService(HubConnection connection, IAppConfig appConfig)
        {
            this.connection = connection;
            this.appConfig = appConfig;
        }

        public async Task CreateGame()
        {
            var game = await connection.InvokeAsync<Game>("CreateGame");
            OnGameCreated?.Invoke(game);
            SceneManager.LoadScene(appConfig.ArenaSceneName);
        }
    }
}