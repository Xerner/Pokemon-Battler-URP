using Poke.Core;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public TMPro.TMP_InputField username;
    public ImageSelector trainerSprite;
    public ImageSelector trainerBackground;

    private void OnEnable()
    {
        Account account = HostBehaviour.Instance.HostAccount;
        username.text = account.Settings.Username;
        trainerSprite.SetSprite(account.Settings.TrainerSpriteId);
        trainerBackground.SetSprite(account.Settings.TrainerBackgroundId);
    }

    public void SaveSettings() => MainMenu.Instance.SaveSettings(username.text, trainerSprite.ActiveSprite, trainerBackground.ActiveSprite);
}
