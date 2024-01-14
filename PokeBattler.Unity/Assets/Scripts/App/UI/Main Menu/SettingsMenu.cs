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
        IMainMenuService mainMenuService;

        [Inject]
        public void Construct(IClientService clientService, IMainMenuService mainMenuService)
        {
            this.clientService = clientService;
            this.mainMenuService = mainMenuService;
        }

        private void OnEnable()
        {
            username.text = clientService.Account.Username;
            trainerSprite.SetSprite(clientService.Account.TrainerSpriteId);
            trainerBackground.SetSprite(clientService.Account.TrainerBackgroundId);
        }

        public void SaveSettings() => mainMenuService.SaveSettings(username.text, trainerSprite.ActiveSprite, trainerBackground.ActiveSprite);
    }
}
