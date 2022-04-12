using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NetworkManager))]
[RequireComponent(typeof(UNetTransport))]
[RequireComponent(typeof(UIPersistentStatus))]
public class PokeHost : MonoBehaviour
{
    public static PokeHost Instance { get; private set; }

    private NetworkManager netManager;
    private UIPersistentStatus connectionStatus;
    private const int maxConnections = 8;
    private bool arenaIsStartingScene = false;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        } else {
            Instance = this;
        }
    }

    void Start()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        netManager = NetworkManager.Singleton;
        connectionStatus = GetComponent<UIPersistentStatus>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (SceneManager.GetActiveScene().name == "ArenaScene")
        {
            arenaIsStartingScene = true;
            InitializeArena();
            CreateGame();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "ArenaScene" && arenaIsStartingScene == false) InitializeArena();
    }

    public void InitializeArena()
    {
        // Add the main user to the game
        if (TryGetComponent<Account>(out var account)) {
            Pokemon.InitializeAllPokemon();
            AddTrainerToGame(account.settings);
        } else {
            Debug.LogError("No Account component found on the NetworkManager object");
        }
    }

    private void AddTrainerToGame(AccountSettings settings)
    {
        TrainerCardManager.Instance.AddTrainer(settings, netManager.LocalClientId);
    }

    /// <summary>
    /// Starts hosting a game. Automatically adds this player to the game as well.
    /// </summary>
    public void CreateGame()
    {
        netManager.StartHost();
        connectionStatus.SetStatus("Hosting", UIPersistentStatus.ConnectionState.Good);
        SceneManager.LoadScene(1); // 1 = arena scene
    }

    /// <summary>
    /// Attempt to connect to a game server. Displays an error popup on fail
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <param name="port"></param>
    /// <returns>If the client was successfully connected</returns>
    public bool ConnectToGame(string ipAddress, int port)
    {
        UNetTransport transport = GetComponent<UNetTransport>();
        // NetworkManager uses the transport object settings for connecting
        transport.ConnectAddress = ipAddress;
        transport.ConnectPort = port;
        // https://github.com/Unity-Technologies/com.unity.multiplayer.samples.coop/blob/fa6d2ba907f2ee18150163dd66773b3f1fc3f5f3/Assets/BossRoom/Scripts/Shared/Net/ConnectionManagement/ClientGameNetPortal.cs#L260
        // and...we're off! Netcode will establish a socket connection to the host.
        //  If the socket connection fails, we'll hear back by getting an OnClientDisconnect callback for ourselves and get a message telling us the reason
        //  If the socket connection succeeds, we'll get our RecvConnectFinished invoked. This is where game-layer failures will be reported.
        UIWindowManager.Instance.CreatePopupMessage($"Connecting to\n\n{transport.ConnectAddress}:{transport.ConnectPort}");
        netManager.StartClient(); // synchronous
        // I would've thought that StartClient() would return false if the server doesn't exist
        // but it actually still returns true... although IsConnectedClient is false. Don't know why
        if (netManager.IsConnectedClient)
        {
            OnClientConnect(netManager.ServerClientId, ipAddress, port);
        }
        else
        {
            OnClientDisconnect(netManager.ServerClientId, ipAddress, port);
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
    private void ApprovalCheck(byte[] connectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback)
    {
        // Dont approve more than 8 trainers
        bool approve = netManager.ConnectedClientsIds.Count == maxConnections;
        bool createPlayerObject = false; // no player object is needed yet

        // The prefab hash. Use null to use the default player prefab
        // If using this hash, replace "MyPrefabHashGenerator" with the name of a prefab added to the NetworkPrefabs field of your NetworkManager object in the scene
        //ulong? prefabHash = NetworkSpawnManager.GetPrefabHashFromGenerator("MyPrefabHashGenerator");

        //If approve is true, the connection gets added. If it's false. The client gets disconnected
        callback(createPlayerObject, null, approve, null, null);
    }

    public void OnClientConnect(ulong clientId, string ipAddress, int port)
    {
        connectionStatus.SetMessage($"Connected to host {ipAddress}:{port}");
        connectionStatus.SetStatus("Connected", UIPersistentStatus.ConnectionState.Good);
        UIWindowManager.Instance.CreatePopupMessage("Connected to server");
    }

    public void OnClientDisconnect(ulong clientId, string ipAddress, int port)
    {
        UNetTransport transport = GetComponent<UNetTransport>();
        netManager.Shutdown();
        connectionStatus.SetMessage($"Failed to connect to host {ipAddress}:{port}");
        connectionStatus.SetIdle();
        UIWindowManager.Instance.CreatePopupMessage($"Failed to connect to server\n\nIP: {transport.ConnectAddress}\nPort: {transport.ConnectPort}");
    }

    //[ClientRpc]
    public void HelloWorldClientRPC()
    {
        Debug.Log("Hello, I am Client");
    }

    //[ServerRpc]
    public void HelloWorldServerRPC()
    {
        Debug.Log("Hello, I am Server");
    }
}
