using UnityEngine;
using UnityEngine.UI;

public class UIPersistentStatus : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI statusText;
    [SerializeField] private TMPro.TextMeshProUGUI connectionStatusText;
    [SerializeField] private Image connectionStatusImg;

    private void Start() {
        SetIdle();
    }

    /// <summary>
    /// Sets the message in the lower left corner
    /// </summary>
    /// <param name="message"></param>
    public void SetMessage(string message) {
        if (message != "") statusText.text = message;
    }

    /// <summary>
    /// Set the connection status message and state
    /// </summary>
    /// <param name="message"></param>
    /// <param name="connectionStatus"></param>
    /// <param name="state"></param>
    public void SetStatus(string connectionStatus, ConnectionState state) {
        connectionStatusText.text = connectionStatus;
        connectionStatusImg.color = stateToColor(state);
    }

    public void SetIdle() {
        SetStatus("No connection", ConnectionState.Bad);
        connectionStatusImg.color = new Color(255f, 0f, 0f);
    }

    private Color stateToColor(ConnectionState state) {
        switch (state) {
            case ConnectionState.Good:
                return new Color(0f, 255f, 0f);
            case ConnectionState.Bad:
                return new Color(255f, 0f, 0f);
            default: return new Color(0f, 0f, 0f);
        }
    }

    public enum ConnectionState {
        Good,
        Bad
    }
}
