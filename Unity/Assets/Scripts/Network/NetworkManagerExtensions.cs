using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Poke.Network {
    public static class NetworkManagerExtensions
    {
        public static bool ShouldCallClientRpc(this NetworkManager networkManager)
        {
            return networkManager.IsServer;
        }

        public static bool ShouldHandleClientRpcs(this NetworkManager networkManager, ulong clientId)
        {
            return networkManager.IsClient || 
                   networkManager.IsHost && 
                   clientId == networkManager.LocalClientId;
        }
    }
}
