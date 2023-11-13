using UnityEngine;

namespace Poke.Unity
{
    public class JoinGameMenu : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_InputField ipAddress;
        [SerializeField] private TMPro.TMP_InputField port;

        public void JoinGame() => MainMenu.Instance.JoinGame(ipAddress.text, port.text);
    }
}
