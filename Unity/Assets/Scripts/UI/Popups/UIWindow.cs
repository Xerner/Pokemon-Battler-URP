using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A generic popup class. Popups are intended to be factory classes.
/// </summary>
public interface UIWindow
{
    /// <summary>
    /// Whether or not the Popup should have a backdrop panel behind it. The actual backdrop panel is implemented through the PopupManager.
    /// False by default.
    /// </summary>
    public bool HasBackdrop { get; }
}
