using Assets.Scripts.App.UI.Popups;
using PokeBattler.Client.Services;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Main Menu/Main Menu View")]
    public class MainMenuViewBehaviour : MonoBehaviour
    {
        IGameService gameService;
        IViewManagerService viewManager;
        IModalService modalService;

        [Inject]
        public void Construct(IGameService gameService, IViewManagerService viewManager, IModalService modalService)
        {
            this.gameService = gameService;
            this.viewManager = viewManager;
            this.modalService = modalService;
        }

        public void CreateGame() {
            CreateGameAsync();
        }

        async Task CreateGameAsync()
        {
            if (await gameService.CreateGame())
            {
                return;
            }
            viewManager.ChangeViews(gameObject);
            modalService.Create<MessageModal, string>("Failed to create game");
        }
    }
}
