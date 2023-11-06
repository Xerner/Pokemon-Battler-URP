using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Poke.Network
{
    public partial class Host // Client
    {
        /// <summary>Attempt to connect to a game server. Displays an error popup on fail</summary>
        /// <returns>If the client was successfully connected</returns>
        public bool ConnectToGame(string ipAddress, int port) => ConnectToGame(ipAddress, (ushort)port);


        /// <summary>Attempt to connect to a game server. Displays an error popup on fail</summary>
        /// <returns>If the client was successfully connected</returns>
        public bool ConnectToGame(string ipAddress, ushort port)
        {
            // NetworkManager uses the transport object Settings for connecting
            transport.ConnectionData.Address = ipAddress;
            transport.ConnectionData.Port = port;
            // https://github.com/Unity-Technologies/com.unity.multiplayer.samples.coop/blob/fa6d2ba907f2ee18150163dd66773b3f1fc3f5f3/Assets/BossRoom/Scripts/Shared/Net/ConnectionManagement/ClientGameNetPortal.cs#L260
            UIWindowManager.Instance.CreatePopupMessage($"Connecting to\n\n{transport.ConnectionData.Address}:{transport.ConnectionData.Port}");
            netManager.StartClient(); // synchronous
                                      // I would've thought that StartClient() would return false if the server doesn't exist
                                      // but it actually still returns true... although IsConnectedClient is false. Don't know why
            if (netManager.IsConnectedClient)
            {
                connectionStatus.SetMessage($"Connected to host {ipAddress}:{port}");
                connectionStatus.SetStatus("Connected", UIPersistentStatus.ConnectionState.Good);
                UIWindowManager.Instance.CreatePopupMessage("Connected to server");
            }
            else
            {
                netManager.Shutdown();
                connectionStatus.SetMessage($"Failed to connect to host {ipAddress}:{port}");
                connectionStatus.SetIdle();
                UIWindowManager.Instance.CreatePopupMessage($"Failed to connect to server\n\nIP: {transport.ConnectionData.Address}\nPort: {transport.ConnectionData.Port}");
            }
            return (netManager.IsConnectedClient);
        }

        /// <summary>
        /// This logic plugs into the "ConnectionApprovalCallback" exposed by NetworkManager, and is run every time a client connects to us.
        /// See GNH_Client.StartClient for the complementary logic that runs when the client starts its connection.
        /// </summary>
        /// <remarks>
        /// Since our game doesn't have to interact with some third party authentication service to validate the identity of the new connection, our ApprovalCheck
        /// method is simple, and runs synchronously, invoking "callback" to signal approval at the end of the method. MLAPI currently doesn't support the ability
        /// to send back more than a "true/false", which means we have to work a little harder to provide a useful error return to the client. To do that, we invoke a
        /// client RPC in the same channel that MLAPI uses for its connection callback. Since that channel ("MLAPI_INTERNAL") is both reliable and sequenced, we can be
        /// confident that our login result message will execute before any disconnect message.
        /// </remarks>
        /// <param name="connectionData">binary data passed into StartClient. In our case this is the client's GUID, which is a unique identifier for their install of the game that persists across app restarts. </param>
        /// <param name="clientId">This is the clientId that MLAPI assigned us on login. It does not persist across multiple logins from the same client. </param>
        /// <param name="callback">The delegate we must invoke to signal that the connection was approved or not. </param>
        private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            // Dont approve more than 8 trainers

            bool approve = netManager.ConnectedClientsIds.Count == maxConnections;
            bool createPlayerObject = false; // no player object is needed yet

            // The prefab hash. Use null to use the default player prefab
            // If using this hash, replace "MyPrefabHashGenerator" with the name of a prefab added to the NetworkPrefabs field of your NetworkManager object in the scene
            //ulong? prefabHash = NetworkSpawnManager.GetPrefabHashFromGenerator("MyPrefabHashGenerator");

            //If approve is true, the connection gets added. If it's false. The client gets disconnected
            //callback(createPlayerObject, null, approve, null, null);
        }
    }
}
