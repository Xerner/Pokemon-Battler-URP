using Poke.Core;
using Poke.Network;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Poke.Unity
{
    [AddComponentMenu("Poke Battler/Host")]
    [RequireComponent(typeof(NetworkManager))]
    [RequireComponent(typeof(UnityTransport))]
    [RequireComponent(typeof(UIPersistentStatus))]
    public class HostBehaviour : MonoBehaviour
    {
        public static HostBehaviour Instance { get; private set; }

        public AccountSO AccountSettingsSO;
        public Account HostAccount;
        public GameSettings GameSettings;
        public string FirstScene;
        [HideInInspector] public Host Host;
        NetworkManager networkManager;

        void OnValidate()
        {
            HostAccount = AccountSettingsSO?.Account;
            networkManager = GetComponent<NetworkManager>();
        }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            else
            {
                Instance = this;
            }
        }

        void Start()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Debug2.CurrentLogLevel = GameSettings.DebugLogLevel;
            Host = new Host(HostAccount, NetworkManager.Singleton, GetComponent<UnityTransport>(), GetComponent<UIPersistentStatus>(), this);
            SceneManager.sceneLoaded += Host.OnSceneLoaded;
            SceneManager.LoadScene(FirstScene);

            Host.OnGameCreated += async (Game game) =>
            {
                game.RoundTimer.SetDuration(GameSettings.RoundTime);
                await Pokemon.InitializeListOfPokemon(new List<string>() { "bulbasaur", "squirtle", "charmander", "magnemite", "abra" });
                game.PokemonPool = new PokemonPool();
                game.OnPokemonDataLoaded(game.PokemonPool);
            };
        }
    }
}
