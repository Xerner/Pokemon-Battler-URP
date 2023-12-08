using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Client.Controllers;
using PokeBattler.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.MemoryProfiler;
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
            connection.On("Test", Test);
            await connection.InvokeAsync("Test", "Test");
            SceneManager.sceneLoaded += OnSceneLoaded;
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }

        void Test()
        {
            Debug.Log("SignalR test");
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode _)
        {
            switch (scene.name)
            {
                case "ArenaScene":
                    if (gameService.Game is null)
                        gameService.CreateGame();
                    return;
                default:
                    return;
            }
        }
    }
}
