using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMenu : MonoBehaviour
{
    [SerializeField] private GameObject nextMenu;
    [SerializeField] private TMPro.TMP_InputField username;

    public void LoadSettings() => MainMenu.Instance.LoadSettings(username.text, nextMenu);
}
