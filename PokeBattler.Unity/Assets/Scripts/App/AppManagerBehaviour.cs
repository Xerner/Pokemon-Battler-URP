using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using PokeBattler.Client.Models;
using PokeBattler.Client.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/App Manager")]
    public class AppManager : MonoBehaviour
    {
        Serilog.ILogger logger;
        IGameService gameService;
        HubConnection connection;

        [Inject]
        public void Construct(Serilog.ILogger logger,
                              IGameService gameService, 
                              HubConnection connection)
        {
            this.logger = logger;
            this.gameService = gameService;
            this.connection = connection;
        }

        void Start()
        {
            // please do not remove the assignment to _. It will cause squiggly
            _ = StartApp();
        }

        async Task StartApp()
        {
            await connection.StartAsync();
            logger.Information($"Connected to {HubConnectionService.HubUrl}");
            connection.On(nameof(HubClient.Singleton.Ping), Ping);
            await connection.InvokeAsync(nameof(HubServer.Singleton.Ping), "Ping");
            SceneManager.sceneLoaded += OnSceneLoaded;
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }

        void Ping()
        {
            Debug.Log("SignalR test");
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode _)
        {
            switch (scene.name)
            {
                case "ArenaScene":
                    if (gameService.Game is null)
                    {
                        var task = Task.Run(() => gameService.CreateGame());
                        task.Wait();
                        var succeeded = task.Result;
                        if (!succeeded)
                        {
                            logger.Error("Failed to create game");
                            return;
                        }
                    }
                    return;
                default:
                    return;
            }
        }
    }
}
