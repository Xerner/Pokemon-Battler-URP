using System;
using UnityEngine.SceneManagement;

namespace Poke.Network
{
    public partial class Host // Server
    {
        public void HostGame()
        {
            StartHosting();
            SceneManager.LoadScene("ArenaScene");
        }

        public void StartHosting()
        {
            netManager.StartHost();
            connectionStatus.SetStatus("Hosting", UIPersistentStatus.ConnectionState.Good);
        }
    }
}
