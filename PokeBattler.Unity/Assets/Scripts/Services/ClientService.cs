using System;
using UnityEngine.SceneManagement;
using PokeBattler.Core;
using PokeBattler.Models;

namespace PokeBattler.Services
{
    public interface IClientService
    {

    }

    public class ClientService : IClientService
    {
        /// <summary>The local clients ID</summary>
        public Guid ClientID { get; private set; }
        Account account;

        public GameService Game;

        public Account Account { get => account; }

        private readonly GameService gameService;

        public ClientService(Account account, GameService gameService)
        {
            ClientID = Guid.NewGuid();
            this.account = account;
            this.gameService = gameService;
            SceneManager.sceneLoaded += OnSceneLoaded;
            gameService.OnGameCreated += CreateTrainer;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode _)
        {
            if (scene.name == "ArenaScene")
            {
                if (gameService.Game is null) gameService.CreateGame();
            }
        }

        void CreateTrainer(Game _)
        {
            gameService.CreateTrainer(account);
        }
    }
}
