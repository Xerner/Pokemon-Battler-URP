using Unity.Netcode;
using UnityEngine;

namespace Poke.Unity
{
    public partial class ReadyButtonBehaviour : MonoBehaviour
    {
        [ServerRpc]
        public void SetReadyServerRpc()
        {
            var clientId = serverRpcParams.Receive.SenderClientId;
            if (NetworkManager.Singleton.ConnectedClients.ContainsKey(clientId))
            {
                var client = NetworkManager.Singleton.ConnectedClients[clientId];
                // Do things for this client
                HostBehaviour.Instance.Host.Trainer.OnReady(isReady);
            }
        }
    }
}
