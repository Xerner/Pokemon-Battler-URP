using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Common;
using PokeBattler.Common.Models;
using PokeBattler.Unity;
using Assets.Scripts.App.UI.Popups;

namespace PokeBattler.Client.Services
{
    public interface IGameService
    {
        public Game Game { get; }
        public Action<Game> OnGameCreated { get; set; }
        public Task<bool> CreateGame();
        public Task JoinGame(string ipAddress, string port);
        public Task JoinGame(Guid gameID, Account account);
    }

    public class GameService : IGameService
    {
        readonly HubConnection connection;
        readonly IAppConfig appConfig;
        readonly IClientService clientService;
        readonly IModalService modalService;

        public Game Game { get; set; }
        public Action<Game> OnGameCreated { get; set; }

        public GameService(IAppConfig appConfig,
                           IClientService clientService,
                           IModalService modalService)
        {
            this.appConfig = appConfig;
            this.clientService = clientService;
            this.modalService = modalService;
        }

        public async Task<bool> CreateGame()
        {
            if (!(connection.State == HubConnectionState.Connected))
            {
                return false;
            }
            var game = await connection.InvokeAsync<Game>("CreateGame");
            await JoinGame(game.Id, clientService.Account);
            return true;
        }

        public async Task JoinGame(string ipAddress, string port)
        {
            if (!int.TryParse(port, out int port_int) && (port_int < 1 || port_int > ushort.MaxValue))
            {
                modalService.Create<MessageModal, string>($"Invalid port\n\nPorts are only digits and no larger than {ushort.MaxValue}");
                return;
            }
            // TODO
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
