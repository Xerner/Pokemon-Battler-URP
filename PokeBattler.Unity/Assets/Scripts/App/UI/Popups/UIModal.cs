using UnityEngine;

 namespace PokeBattler.Unity
{
    public class UIModal : PrefabFactory<UIModal>, UIWindow
    {
        [SerializeField] private TMPro.TextMeshProUGUI message;

        /// <summary>
        /// UI Popup messages always have a backdrop
        /// </summary>
        public bool HasBackdrop { get => true; }

        /// <summary>
        /// RequestAddToGame a Popup instance and return it
        /// </summary>
        public UIModal Create(string message, Transform backdrop)
        {
            UIModal popup = Create(backdrop);
            popup.message.text = message;
            return popup;
        }

        /// <summary>
        /// RequestAddToGame a Popup instance, set its height, and return it
        /// </summary>
        /// <returns></returns>
        public UIModal Create(string message, Transform backdrop, float height)
        {
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, height);
            return Create(message, backdrop);
        }
    }
}

