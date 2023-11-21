using Poke.Network;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Poke.Unity
{
    [AddComponentMenu("Poke Battler/Ready Button")]
    [RequireComponent(typeof(Button))]
    public partial class ReadyButtonBehaviour : MonoBehaviour
    {
        [SerializeField] Sprite readySprite;
        [SerializeField] Sprite notReadySprite;

        Button button;
        bool isReady = false;

        void Start()
        {
            button = GetComponent<Button>();
            SetReady(isReady);
            button.image.sprite = notReadySprite;
            HostBehaviour.Instance.Host.Trainer.OnReady += SetReady;
        }

        void OnDestroy()
        {
            Host.Instance.Trainer.OnReady += SetReady;
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
            SetReadyServerRpc(!isReady);
        }
    }
}
