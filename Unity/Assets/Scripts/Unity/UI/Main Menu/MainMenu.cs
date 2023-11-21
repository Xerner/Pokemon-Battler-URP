using Poke.Serializable;
using Unity.Netcode;
using UnityEngine;

namespace Poke.Unity
{
    public class MainMenu : MonoBehaviour
    {
        public static MainMenu Instance { get; private set; }

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

        public void CreateGame() => HostBehaviour.Instance.Host.HostGame();

        public void JoinGame(string ipAddress, string port)
        {
            HostBehaviour pokehost = NetworkManager.Singleton.gameObject.GetComponent<HostBehaviour>();
            // port validation
            if (!int.TryParse(port, out int port_int) && (port_int < 1 || port_int > 65535))
            {
                UIWindowManagerBehaviour.Instance.CreatePopupMessage("Invalid port\n\nPorts are only digits and no larger than 65535", 170f);
                return;
            }
            pokehost.Host.ConnectToGame(ipAddress, port_int);
        }

        /// <summary>
        /// Attempts to load the Settings of the given username.<br/>
        /// On success: sends the user to the main menu<br/>
        /// On fail: shows a popup message
        /// </summary>
        public void LoadSettings(string username, UnityEngine.GameObject nextMenu)
        {
            AccountSettings settings = SaveSystem.LoadAccount(username.Trim().ToLower());
            if (settings == null)
            {
                UIWindowManagerBehaviour.Instance.CreatePopupMessage("Trainer not found");
            }
            else
            {
                Debug.Log("Loading trainer '" + username.Trim().ToLower() + "' with Settings\n" + settings);
                ViewManager.Instance.ChangeViews(nextMenu);
                HostBehaviour.Instance.HostAccount.Settings = settings;
            }
        }

        /// <summary>Save username, trainer sprite, and trainer background sprite to the account file</summary>
        public void SaveSettings(string username, string trainerSpriteName, string trainerBackgroundName)
        {
            HostBehaviour.Instance.HostAccount.Settings = new AccountSettings(
                username,
                trainerSpriteName,
                trainerBackgroundName,
                HostBehaviour.Instance.HostAccount.Settings.ClientID,
                HostBehaviour.Instance.HostAccount.Settings.GameID);
            SaveSystem.SaveAccount(HostBehaviour.Instance.HostAccount.Settings);
        }
    }
}
