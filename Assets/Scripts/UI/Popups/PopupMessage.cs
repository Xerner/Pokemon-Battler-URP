using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMessage : PrefabFactory<PopupMessage>, UIPopup {
    [SerializeField] private TMPro.TextMeshProUGUI message;

    /// <summary>
    /// UI Popup messages always have a backdrop
    /// </summary>
    public bool HasBackdrop { get => true; }

    /// <summary>
    /// Create a Popup instance and return it
    /// </summary>
    public PopupMessage Create(string message, Transform backdrop) {
        PopupMessage popup = Create(backdrop);
        popup.message.text = message;
        return this;
    }

    /// <summary>
    /// Create a Popup instance, set its height, and return it
    /// </summary>
    /// <returns></returns>
    public PopupMessage Create(string message, Transform backdrop, float height) {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, height);
        return Create(message, backdrop);
    }
}
