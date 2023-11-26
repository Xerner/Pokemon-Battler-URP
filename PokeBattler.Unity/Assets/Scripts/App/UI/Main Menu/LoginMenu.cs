using UnityEngine;

namespace PokeBattler.Unity
{
    public class LoginMenu : MonoBehaviour
    {
        [SerializeField] private UnityEngine.GameObject nextMenu;
        [SerializeField] private TMPro.TMP_InputField username;

        public void LoadSettings() => MainMenu.Instance.LoadSettings(username.text, nextMenu);
    }
}
