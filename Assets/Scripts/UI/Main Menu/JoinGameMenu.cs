using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class JoinGameMenu : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField ipAddress;
    [SerializeField] private TMPro.TMP_InputField port;

    public void JoinGame() => MainMenu.Instance.JoinGame(ipAddress.text, port.text);
}
