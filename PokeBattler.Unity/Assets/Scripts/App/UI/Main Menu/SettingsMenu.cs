using UnityEngine;
using PokeBattler.Services;
using Zenject;

namespace PokeBattler.Unity
{
    public class SettingsMenu : MonoBehaviour
    {
        public TMPro.TMP_InputField username;
        public ImageSelector trainerSprite;
        public ImageSelector trainerBackground;


        private ClientService clientService;

        [Inject]
        public void Construct(ClientService clientService)
        {
            this.clientService = clientService;
        }

        private void OnEnable()
        {
            username.text = clientService.Account.Settings.Username;
            trainerSprite.SetSprite(clientService.Account.Settings.TrainerSpriteId);
            trainerBackground.SetSprite(clientService.Account.Settings.TrainerBackgroundId);
        }

        public void SaveSettings() => MainMenu.Instance.SaveSettings(username.text, trainerSprite.ActiveSprite, trainerBackground.ActiveSprite);
    }
}
