using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using Zenject;
using PokeBattler.Common.Models;
using PokeBattler.Client.Services;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Main Menu/Login View")]
    public class LoginViewBehaviour : MonoBehaviour
    {
        [SerializeField] GameObject nextMenu;
        [SerializeField] TMPro.TMP_InputField username;
        [SerializeField] ErrorTextBehaviour error;

        void OnValidate()
        {
            if (nextMenu == null)
            {
                Debug.LogError("nextMenu is null", this);
            }
            if (username == null)
            {
                Debug.LogError("username is null", this);
            }
            if (error == null)
            {
                Debug.LogError("errorMessage is null", this);
            }
        }

        ISaveSystem saveSystem;
        IClientService clientService;
        IViewManagerService viewManager;

        [Inject]
        public void Construct(IClientService clientService, ISaveSystem saveSystem, IViewManagerService viewManager)
        {
            this.clientService = clientService;
            this.saveSystem = saveSystem;
            this.viewManager = viewManager;
        }

        public void LoadSettings()
        {
            Account account = null;
            try
            {
                account = LoadSettings(username.text, nextMenu);
            }
            catch (SerializationException)
            {
                error.gameObject.SetActive(true);
                error.Error("Error loading profile. Profile data is corrupted");
            }
            catch (IOException)
            {
                error.gameObject.SetActive(true);
                error.Error("Profile data is already open by another process");
            }
            if (account is null)
            {
                error.gameObject.SetActive(true);
                error.Error("Trainer not found");
                return;
            }
            error.gameObject.SetActive(false);
        }

        public Account LoadSettings(string username, GameObject nextMenu)
        {
            Account account = saveSystem.LoadAccount(username.Trim().ToLower());
            if (account == null)
            {
                UIWindowManagerBehaviour.Instance.CreatePopupMessage("Trainer not found");
                return account;
            }
            Debug.Log("Loading trainer '" + username.Trim().ToLower() + "' with Settings\n" + account);
            viewManager.ChangeViews(nextMenu);
            clientService.Account = account;
            return account;
        }
    }
}
