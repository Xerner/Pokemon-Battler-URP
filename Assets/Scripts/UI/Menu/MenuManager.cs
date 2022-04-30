using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public Action<bool> OnUIToggle;
    public Action OnEnteringUI;
    public Action OnExitingUI;
    public static bool DashboardMode = false;
    [HideInInspector]
    public Menu ActiveMenu;

    private static List<Menu> activeMenus = new List<Menu>();

    private void Start()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        } else {
            Instance = this;
        }
    }

    public void Subscribe(Menu menu)
    {
        menu.Close();
        menu.OnOpen += AddMenu;
        menu.OnClose += RemoveMenu;
        menu.OnEnter += OnPointerEnter;
        menu.OnExit += OnPointerExit;
    }

    public void ToggleMenu(Menu menu)
    {
        menu.Toggle();
    }

    private void RemoveMenu(Menu menu)
    {
        activeMenus.Remove(menu);
        // only re-set ActiveMenu if the menu being turned off was the ActiveMenu
        if (activeMenus.Count > 0)
        {
            if (menu == ActiveMenu) ActiveMenu = activeMenus[activeMenus.Count - 1];
        }
        else
        {
            ActiveMenu = null;
        }
        OnUIToggle?.Invoke(IsUIActive());
    }

    private void AddMenu(Menu menu)
    {
        activeMenus.Add(menu);
        ActiveMenu = menu;
        OnUIToggle?.Invoke(true);
    }

    /// <summary>
    /// Returns true if any Menu is open
    /// </summary>
    /// <returns></returns>
    public bool IsUIActive()
    {
        foreach (Menu menu in activeMenus)
        {
            if (menu.IsOpen()) return true;
        }
        return false;
    }

    public static void CloseAll()
    {
        for (int i = 0; i < activeMenus.Count; i++)
        {
            activeMenus[i].Close();
        }
    }

    #region Tab group methods

    public void OnNumberKeyPress(int value)
    {
        if (ActiveMenu != null)
            ActiveMenu.OnNumberKeyPress(value);
    }

    /// <summary>Switches menu tabs</summary>
    public void NextTab()
    {
        if (ActiveMenu != null)
            ActiveMenu.NextTab();
    }

    #endregion

    //#region Sensor popup methods

    //public void ToggleSensorPopup() => ToggleMenu(TileSensorPreview);

    //internal void InspectTile(Vector2Int position)
    //{
    //    TileSensorPreview.FocusTile(position);
    //    if (!activeMenus.Contains(TileSensorPreview)) ToggleSensorPopup();
    //}

    //#endregion

    /// <summary>
    /// Called whenever the pointer hovers over a Menu
    /// </summary>
    public void OnPointerEnter()
    {
        OnEnteringUI?.Invoke();
    }

    /// <summary>
    /// Called whenever the pointer hovers off of a Menu
    /// </summary>
    public void OnPointerExit()
    {
        OnExitingUI?.Invoke();
    }
}

//public enum UIBackgroundSprite
//{
//    Red,
//    Green,
//    Blue,
//    Yellow,
//    Orange,
//    OrangeButton,
//    Purple
//}

//public static class UIBackgroundSpriteExtensions
//{
//    public static string GetAddress(this UIBackgroundSprite sprite) => sprite switch
//    {
//        UIBackgroundSprite.Red => "UI/UI Elements/Buttons/red button",
//        UIBackgroundSprite.Green => "UI/UI Elements/Buttons/green button",
//        UIBackgroundSprite.Blue => "UI/UI Elements/Buttons/blue button",
//        UIBackgroundSprite.Yellow => "UI/UI Elements/Buttons/yellow button",
//        UIBackgroundSprite.Orange => "UI/UI Elements/Buttons/Orange button",
//        UIBackgroundSprite.OrangeButton => "UI/UI Elements/Buttons/Orange_button",
//        UIBackgroundSprite.Purple => "UI/ UI Elements/Buttons/purple_button_inverted",
//        _ => ""
//    };
//}