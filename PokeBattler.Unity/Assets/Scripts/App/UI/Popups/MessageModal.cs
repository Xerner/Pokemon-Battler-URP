using System;
using UnityEngine;
using PokeBattler.Unity;

namespace Assets.Scripts.App.UI.Popups
{
    [AddComponentMenu("Poke Battler/UI/Message Modal")]
    public class MessageModal : ModalBehaviour<string>
    {
        [SerializeField]
        TMPro.TextMeshProUGUI messageText;

        private void OnValidate()
        {
            if (messageText == null)
            {
                messageText = transform. GetComponentInChildren<TMPro.TextMeshProUGUI>();
            }
            if (messageText == null)
            {
                throw new NullReferenceException("Modal message text GameObject not set");
            }
        }

        public override void Initialize(string data)
        {
            messageText.text = data;
        }
    }
}