using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        } else {
            Instance = this;
        }
    }

    public void CreateGame() => PokeHost.Instance.CreateGame();

    public void JoinGame(string ipAddress, string port) {
        PokeHost pokehost = NetworkManager.Singleton.gameObject.GetComponent<PokeHost>();
        // port validation
        if (!int.TryParse(port, out int port_int) && (port_int < 1 || port_int > 65535)) {
            UIWindowManager.Instance.CreatePopupMessage("Invalid port\n\nPorts are only digits and no larger than 65535", 170f);
            return;
        }
        pokehost.ConnectToGame(ipAddress, port_int);
    }

    /// <summary>
    /// Attempts to load the settings of the given username.<br/>
    /// On success: sends the user to the main menu<br/>
    /// On fail: shows a popup message
    /// </summary>
    public void LoadSettings(string username, UnityEngine.GameObject nextMenu) {
        AccountSettings settings = SaveSystem.LoadAccount(username.Trim().ToLower());
        if (settings == null) {
            UIWindowManager.Instance.CreatePopupMessage("Trainer not found");
        } else {
            Debug.Log("Loading trainer '" + username.Trim().ToLower() + "' with settings\n" + settings);
            ViewManager.Instance.ChangeViews(nextMenu);
            PokeHost.Instance.HostAccount.settings = settings;
        }
    }

    /// <summary>Save username, trainer sprite, and trainer background sprite to the account file</summary>
    public void SaveSettings(string username, string trainerSpriteName, string trainerBackgroundName) {
        PokeHost.Instance.HostAccount.settings = new AccountSettings(
            username, 
            trainerSpriteName, 
            trainerBackgroundName, 
            PokeHost.Instance.HostAccount.settings.ClientID, 
            PokeHost.Instance.HostAccount.settings.GameID);
        SaveSystem.SaveAccount(PokeHost.Instance.HostAccount.settings);
    }
}
