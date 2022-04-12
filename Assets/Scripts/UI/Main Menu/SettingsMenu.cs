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
        trainerSprite.SetSprite(account.settings.TrainerSpriteName);
        trainerBackground.SetSprite(account.settings.TrainerBackgroundName);
    }

    public void SaveSettings() => MainMenu.Instance.SaveSettings(username.text, trainerSprite.ActiveSprite, trainerBackground.ActiveSprite);
}
