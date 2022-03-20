using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public TMPro.TMP_InputField username;
    public ImageSelector trainerSprite;
    public ImageSelector trainerBackground;

    private void OnEnable()
    {
        Account account = Account.FindAccount();
        username.text = account.settings.Username;
        trainerSprite.SetSprite(account.settings.TrainerSpriteID);
        trainerBackground.SetSprite(account.settings.TrainerBackgroundID);
    }

    public void SaveSettings() => MainMenu.Instance.SaveSettings(username.text, trainerSprite.ActiveIndex, trainerBackground.ActiveIndex);
}
