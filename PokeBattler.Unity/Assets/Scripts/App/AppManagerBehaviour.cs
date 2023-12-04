using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Client.Controllers;
using PokeBattler.Client.Services;
using PokeBattler.Common;
using PokeBattler.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/App Manager")]
    public class AppManager : MonoBehaviour
    {
        IGameService gameService;
        GameController gameController;
        HubConnection connection;

        [Inject]
        public void Construct(IGameService gameService, GameController gameController, HubConnection connection)
        {
            this.gameService = gameService;
            this.gameController = gameController;
            this.connection = connection;
        }

        void Start()
        {
            StartApp();
        }

        async Task StartApp()
        {
            await Pokemon.InitializeListOfPokemon(new List<string>() { "bulbasaur", "squirtle", "charmander", "magnemite", "abra" });
            await connection.StartAsync();
            Debug.Log($"Connected to {HubConnectionService.HubUrl}");
            await connection.InvokeAsync("Test", "Test");
            SceneManager.sceneLoaded += OnSceneLoaded;
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode _)
        {
            switch (scene.name)
            {
                case "ArenaScene":
                    if (gameService.Game is null)
                        gameController.RequestCreateGame();
                    return;
                default:
                    return;
            }
        }
    }
}
