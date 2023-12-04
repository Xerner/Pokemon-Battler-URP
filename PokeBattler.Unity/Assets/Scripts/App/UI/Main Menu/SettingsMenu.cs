using UnityEngine;
using Zenject;
using PokeBattler.Client.Services;

namespace PokeBattler.Unity
{
    public class SettingsMenu : MonoBehaviour
    {
        public TMPro.TMP_InputField username;
        public ImageSelector trainerSprite;
        public ImageSelector trainerBackground;


        private IClientService clientService;

        [Inject]
        public void Construct(IClientService clientService)
        {
            this.clientService = clientService;
        }

        private void OnEnable()
        {
            username.text = clientService.Account.Username;
            trainerSprite.SetSprite(clientService.Account.TrainerSpriteId);
            trainerBackground.SetSprite(clientService.Account.TrainerBackgroundId);
        }

        public void SaveSettings() => MainMenu.Instance.SaveSettings(username.text, trainerSprite.ActiveSprite, trainerBackground.ActiveSprite);
    }
}
