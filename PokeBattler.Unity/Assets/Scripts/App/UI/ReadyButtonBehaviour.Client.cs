using PokeBattler.Client.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Ready Button")]
    [RequireComponent(typeof(Button))]
    public partial class ReadyButtonBehaviour : MonoBehaviour
    {
        [SerializeField] Sprite readySprite;
        [SerializeField] Sprite notReadySprite;

        Button button;
        bool isReady = false;

        private ITrainersService trainersService;

        [Inject]
        public void Construct(ITrainersService trainersService)
        {
            this.trainersService = trainersService;
        }

        void Start()
        {
            button = GetComponent<Button>();
            SetReady(isReady);
            button.image.sprite = notReadySprite;
            trainersService.ClientsTrainer.OnReadySubscribe(SetReady);
        }

        void OnDestroy()
        {
            trainersService.ClientsTrainer.OnReadyUnsubscribe(SetReady);
        }

        public void SetReady(bool ready)
        {
            button.image.sprite = ready ? readySprite : notReadySprite;
            if (ready)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
            isReady = ready;
        }

        public void ToggleReady()
        {
            //SetReadyServerRpc(!isReady);
        }
    }
}
