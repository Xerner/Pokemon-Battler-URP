using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: UI Popups are intended to be used on menu screens. this needs to be renamed to be more specific to menu screens only
/// <summary>
/// A generic popup class. Popups are intended to be factory classes.
/// </summary>
public interface UIPopup
{
    /// <summary>
    /// Whether or not the Popup should have a backdrop Panel behind it. The actual backdrop panel is implemented through the PopupManager.
    /// False by default.
    /// </summary>
    public bool HasBackdrop { get; }
}
