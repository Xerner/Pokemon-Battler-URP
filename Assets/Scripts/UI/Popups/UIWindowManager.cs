using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIModal))]
public class UIWindowManager : SingletonBehaviour<UIWindowManager>
{
    [SerializeField]
    private Transform backdrop;
    //private Dictionary<string, UIPopup> popupOptions = new Dictionary<string, UIPopup>();

    void Start() {
        base.Start();
        backdrop.gameObject.SetActive(false);
    }

    /// <summary>
    /// Create a new UI Popup with the given message and an optional height. UI Popups always have a backdrop
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public UIModal CreatePopupMessage(string message, float? height = null) {
        UIModal popup;
        UIModal factory = GetComponent<UIModal>();
        if (height != null) {
            popup = factory.Create(message, backdrop, (float)height);
        } else {
            popup = factory.Create(message, backdrop);
        }
        if (popup.HasBackdrop) backdrop.gameObject.SetActive(true);
        popup.OnDestroy += PopupMessageDestroyed;
        return popup;
    }

    /// <summary>
    /// Invoked when a PopupMessage is destroyed
    /// </summary>
    private void PopupMessageDestroyed(UIModal popup) {
        if (backdrop.childCount == 1) backdrop.gameObject.SetActive(false);
    }
}
