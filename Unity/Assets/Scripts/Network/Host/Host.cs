using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Poke.Core;
using System;
using Poke.Unity;

namespace Poke.Network
{

    public partial class Host // General
    {
        private const int maxConnections = 8;

        NetworkManager netManager;
        UnityTransport transport;
        UIPersistentStatus connectionStatus;
        public HostBehaviour hostBehaviour;

        Account account;

        public Action<Game> OnGameCreated;

        #region In-game
        public Game Game;
        public Trainer Trainer;
        #endregion

        public Account Account { get => account; }

        public Host(Account account, NetworkManager netManager, UnityTransport transport, UIPersistentStatus connectionStatus, HostBehaviour hostBehaviour)
        {
            this.account = account;
            this.netManager = netManager;
            this.transport = transport;
            this.connectionStatus = connectionStatus;
            this.hostBehaviour = hostBehaviour;
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode _)
        {
            if (hostBehaviour.FirstScene == scene.name)
            {
                StartHosting();
            }
            if (scene.name == "ArenaScene")
            {
                Game = new Game();
                Trainer = Game.CreateTrainer(account);
                OnGameCreated.Invoke(Game);
            }
        }
    }
}
