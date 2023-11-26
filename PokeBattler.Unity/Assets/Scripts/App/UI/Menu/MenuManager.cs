using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PokeBattler.Unity
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance { get; private set; }

        private List<Menu> menus = new List<Menu>();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            else
            {
                Instance = this;
            }
        }

        public void Subscribe(Menu menu)
        {
            menus.Add(menu);
        }

        public void Unsubscribe(Menu menu)
        {
            menus.Remove(menu);
        }

        public void CloseAll()
        {
            for (int i = 0; i < menus.Count; i++)
            {
                menus[i].Close();
            }
        }
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