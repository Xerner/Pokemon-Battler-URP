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

        [Inject]
        public void Construct(IGameService gameService, IViewManagerService viewManager)
        {
            this.gameService = gameService;
            this.viewManager = viewManager;
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
            UIWindowManagerBehaviour.Instance.CreatePopupMessage("Failed to create game");
        }
    }
}
