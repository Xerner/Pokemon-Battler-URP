using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PopupMessage))]
public class PopupManager : SingletonBehaviour<PopupManager>
{
    [SerializeField]
    private Transform backdrop;
    //private Dictionary<string, UIPopup> popupOptions = new Dictionary<string, UIPopup>();

    private void Start() {
        backdrop.gameObject.SetActive(false);
    }

    /// <summary>
    /// Create a new UI Popup with the given message and an optional height. UI Popups always have a backdrop
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public PopupMessage CreatePopupMessage(string message, float? height = null) {
        PopupMessage popup;
        if (height != null) {
            popup = GetComponent<PopupMessage>().Create(message, backdrop, (float)height);
        } else {
            popup = GetComponent<PopupMessage>().Create(message, backdrop);
        }
        popup.OnDestroy += PopupMessageDestroyed;
        return popup;
    }

    /// <summary>
    /// Invoked when a PopupMessage is destroyed
    /// </summary>
    /// <param name="popup"></param>
    private void PopupMessageDestroyed(PopupMessage popup) {
        if (backdrop.childCount == 0) backdrop.gameObject.SetActive(false);
    }
}
