using Poke.Core;
using Poke.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Poke.Network
{
    public static class NetworkClientExtensions
    {
        public static HostBehaviour HostBehaviour(this NetworkClient client)
        {
            return client.PlayerObject.GetComponent<HostBehaviour>();
        }

        public static Trainer Trainer(this NetworkClient client)
        {
            return client.PlayerObject.GetComponent<HostBehaviour>().Host.Trainer;
        }
    }
}

