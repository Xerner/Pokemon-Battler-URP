using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Client.Models;
using PokeBattler.Client.Services;
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
        HubConnection connection;

        [Inject]
        public void Construct(IGameService gameService, HubConnection connection)
        {
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
            Debug.Log($"Connected to {HubConnectionService.HubUrl}");
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
                        Task.Run(() => gameService.CreateGame()).Wait();
                    }
                    
                    return;
                default:
                    return;
            }
        }
    }
}
