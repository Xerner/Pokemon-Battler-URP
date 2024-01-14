using PokeBattler.Client.Services;
using PokeBattler.Common.Models;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using Zenject;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Main Menu/Login View")]
    public class LoginViewBehaviour : MonoBehaviour
    {
        [SerializeField] GameObject nextMenu;
        [SerializeField] TMPro.TMP_InputField username;
        [SerializeField] ErrorTextBehaviour error;

        IMainMenuService mainMenuService;

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

        [Inject]
        public void Construct(IMainMenuService mainMenuService)
        {
            this.mainMenuService = mainMenuService;
        }

        public void LoadSettings()
        {
            Account account = null;
            try
            {
                account = mainMenuService.LoadSettings(username.text, nextMenu);
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
    }
}
