using Poke.Core;
using Poke.Network;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NetworkManager))]
[RequireComponent(typeof(UnityTransport))]
[RequireComponent(typeof(UIPersistentStatus))]
public class HostBehaviour : MonoBehaviour {
    public static HostBehaviour Instance { get; private set; }

    public AccountSO AccountSettingsSO;
    public Account HostAccount;
    public GameSettings GameSettings;
    public string FirstScene;
    [HideInInspector] public Host Host;

    void OnValidate()
    {
        HostAccount = AccountSettingsSO?.Account;
    }

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        } else {
            Instance = this;
        }
    }

    void Start() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Host = new Host(HostAccount, NetworkManager.Singleton, GetComponent<UnityTransport>(), GetComponent<UIPersistentStatus>(), this);
        SceneManager.sceneLoaded += Host.OnSceneLoaded;
        SceneManager.LoadScene(FirstScene);

        Host.OnGameCreated += async (Game game) =>
        {
            await Pokemon.InitializeListOfPokemon(new List<string>() { "bulbasaur", "squirtle", "charmander", "magnemite", "abra" });
            game.OnPokemonDataLoaded.Invoke();
        };
    }
}
