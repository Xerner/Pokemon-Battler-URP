using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MainMenu : SingletonBehaviour<MainMenu>
{
    public void JoinGame(string ipAddress, string port) {
        PokeHost pokehost = NetworkManager.Singleton.gameObject.GetComponent<PokeHost>();
        // port validation
        if (!int.TryParse(port, out int port_int) && (port_int < 1 || port_int > 65535)) {
            PopupManager.Instance.CreatePopupMessage("Invalid port\n\nPorts are only digits and no larger than 65535", 170f);
            return;
        }
        pokehost.ConnectToGame(ipAddress, port_int);
    }

    public void LoadSettings(string username, GameObject nextMenu) {
        Account account = Account.FindAccount();
        AccountSettings settings = SaveSystem.LoadAccount(username.Trim().ToLower());
        if (settings == null) {
            PopupManager.Instance.CreatePopupMessage("Trainer not found");
        } else {
            Debug.Log("Loading trainer '" + username.Trim().ToLower() + "' with settings\n" + settings);
            ViewManager.Instance.ChangeViews(nextMenu);
            account.settings = settings;
        }
    }

    /// <summary>
    /// Save username, trainer sprite, and trainer background sprite to the account file
    /// </summary>
    /// <param name="username"></param>
    /// <param name="trainerSpriteIndex"></param>
    /// <param name="trainerBackgroundIndex"></param>
    public void SaveSettings(string username, int trainerSpriteIndex, int trainerBackgroundIndex) {
        Account account = Account.FindAccount();
        account.SetSettings(username, trainerSpriteIndex, trainerBackgroundIndex);
        SaveSystem.SaveAccount(account);
    }
}