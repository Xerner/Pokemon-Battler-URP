using UnityEngine;
using Zenject;
using PokeBattler.Client.Services;
using PokeBattler.Common.Models;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Main Menu/Settings View")]
    public class SettingsViewBehaviour : MonoBehaviour
    {
        public TMPro.TMP_InputField username;
        public ImageSelector trainerSprite;
        public ImageSelector trainerBackground;


        private IClientService clientService;
        ISaveSystem saveSystem;

        [Inject]
        public void Construct(IClientService clientService, ISaveSystem saveSystem)
        {
            this.clientService = clientService;
            this.saveSystem = saveSystem;
        }

        private void OnEnable()
        {
            username.text = clientService.Account.Username;
            trainerSprite.SetSprite(clientService.Account.TrainerSpriteId);
            trainerBackground.SetSprite(clientService.Account.TrainerBackgroundId);
        }

        public void SaveSettings()
        {
            SaveSettings(username.text, trainerSprite.ActiveSprite, trainerBackground.ActiveSprite);
        }

        /// <summary>Save username, trainer sprite, and trainer background sprite to the account file</summary>
        public void SaveSettings(string username, string trainerSpriteName, string trainerBackgroundName)
        {
            clientService.Account = new Account()
            {
                Username = username,
                TrainerSpriteId = trainerSpriteName,
                TrainerBackgroundId = trainerBackgroundName,
                Id = clientService.Account.Id,
                GameId = clientService.Account.GameId
            };
            saveSystem.SaveAccount(clientService.Account);
        }
    }
}
