
using System;
using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;

// =====================================================
// ARTIGO33 - MULTIPLAYER NETWORK BOOTSTRAP
// UNITY 6 + FISHNET + AWS EC2
//
// Modos:
// DevelopmentHost - Servidor e cliente local
// Client          - Cliente remoto
// DedicatedServer - Servidor Linux dedicado
//
// API HTTPS e banco Neon utilizam uma conexão separada.
// =====================================================

public class Artigo33NetworkBootstrap : MonoBehaviour
{
    public static Artigo33NetworkBootstrap Instance;

    public enum NetworkStartMode
    {
        DevelopmentHost,
        Client,
        DedicatedServer
    }

    [Header("FishNet")]
    [SerializeField]
    private NetworkManager networkManager;

    [Header("Modo de Inicialização")]
    [SerializeField]
    private NetworkStartMode startMode =
        NetworkStartMode.DevelopmentHost;

    [Header("Servidor Multiplayer")]
    [SerializeField]
    private string serverAddress = "54.94.200.173";

    [SerializeField]
    private ushort serverPort = 7770;

    [Header("Desenvolvimento")]
    [SerializeField]
    private string localServerAddress = "127.0.0.1";

    public bool IsNetworkReady { get; private set; }

    public bool IsServerRunning =>
        networkManager != null &&
        networkManager.ServerManager.Started;

    public bool IsClientConnected =>
        networkManager != null &&
        networkManager.ClientManager.Started;

    public event Action NetworkReady;

    // =====================================================
    // AWAKE
    // =====================================================

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (networkManager == null)
        {
            networkManager =
                FindAnyObjectByType<NetworkManager>();
        }

        if (networkManager == null)
        {
            Debug.LogError(
                "[MULTIPLAYER] NetworkManager não encontrado."
            );

            return;
        }

        Debug.Log(
            "[MULTIPLAYER] FishNet NetworkManager encontrado."
        );
    }

    // =====================================================
    // START
    // =====================================================

    private void Start()
    {
        if (networkManager == null)
            return;

        networkManager.ClientManager.OnClientConnectionState +=
            OnClientConnectionState;

        networkManager.ServerManager.OnServerConnectionState +=
            OnServerConnectionState;

        StartNetworkAutomatically();
    }

    // =====================================================
    // SELECIONAR MODO DE REDE
    // =====================================================

    private void StartNetworkAutomatically()
    {
        NetworkStartMode selectedMode = startMode;

#if UNITY_SERVER
        selectedMode = NetworkStartMode.DedicatedServer;
#elif !UNITY_EDITOR
        selectedMode = NetworkStartMode.Client;
#endif

        startMode = selectedMode;

        Debug.Log(
            "[MULTIPLAYER] Modo de rede: " + startMode
        );

        switch (startMode)
        {
            case NetworkStartMode.DevelopmentHost:
                StartDevelopmentHost();
                break;

            case NetworkStartMode.Client:
                StartClient();
                break;

            case NetworkStartMode.DedicatedServer:
                StartDedicatedServer();
                break;
        }
    }

    // =====================================================
    // HOST LOCAL DE DESENVOLVIMENTO
    // =====================================================

    private void StartDevelopmentHost()
    {
        if (networkManager == null)
            return;

        Debug.Log(
            "[MULTIPLAYER] Iniciando HOST local."
        );

        if (!networkManager.ServerManager.Started)
        {
            networkManager.ServerManager.StartConnection();
        }

        if (!networkManager.ClientManager.Started)
        {
            networkManager.ClientManager.StartConnection(
                localServerAddress,
                serverPort
            );
        }
    }

    // =====================================================
    // CLIENTE REMOTO
    // =====================================================

    public void StartClient()
    {
        if (networkManager == null)
        {
            Debug.LogError(
                "[MULTIPLAYER] NetworkManager indisponível."
            );

            return;
        }

        if (networkManager.ClientManager.Started)
            return;

        Debug.Log(
            "[MULTIPLAYER] Conectando cliente ao servidor: " +
            serverAddress + ":" + serverPort
        );

        networkManager.ClientManager.StartConnection(
            serverAddress,
            serverPort
        );
    }

    // =====================================================
    // SERVIDOR DEDICADO AWS
    // =====================================================

    private void StartDedicatedServer()
    {
        if (networkManager == null)
            return;

        if (networkManager.ServerManager.Started)
            return;

        Debug.Log(
            "[MULTIPLAYER] Iniciando servidor dedicado AWS."
        );

        networkManager.ServerManager.StartConnection();
    }

    // =====================================================
    // CLIENT CONNECTION STATE
    // =====================================================

    private void OnClientConnectionState(
        ClientConnectionStateArgs args
    )
    {
        Debug.Log(
            "[MULTIPLAYER] Estado CLIENTE: " +
            args.ConnectionState
        );

        if (args.ConnectionState ==
            LocalConnectionState.Started)
        {
            Debug.Log(
                "[MULTIPLAYER] CLIENTE CONECTADO."
            );

            SetNetworkReady();
        }
        else if (args.ConnectionState ==
                 LocalConnectionState.Stopped)
        {
            IsNetworkReady = false;

            Debug.Log(
                "[MULTIPLAYER] CLIENTE DESCONECTADO."
            );
        }
    }

    // =====================================================
    // SERVER CONNECTION STATE
    // =====================================================

    private void OnServerConnectionState(
        ServerConnectionStateArgs args
    )
    {
        Debug.Log(
            "[MULTIPLAYER] Estado SERVIDOR: " +
            args.ConnectionState
        );

        if (args.ConnectionState ==
            LocalConnectionState.Started)
        {
            Debug.Log(
                "[MULTIPLAYER] SERVIDOR INICIADO."
            );

            if (startMode ==
                NetworkStartMode.DedicatedServer)
            {
                SetNetworkReady();
            }
        }
        else if (args.ConnectionState ==
                 LocalConnectionState.Stopped)
        {
            if (startMode ==
                NetworkStartMode.DedicatedServer)
            {
                IsNetworkReady = false;
            }

            Debug.Log(
                "[MULTIPLAYER] SERVIDOR ENCERRADO."
            );
        }
    }

    // =====================================================
    // NETWORK READY
    // =====================================================

    private void SetNetworkReady()
    {
        if (IsNetworkReady)
            return;

        IsNetworkReady = true;

        Debug.Log(
            "[MULTIPLAYER] REDE PRONTA."
        );

        NetworkReady?.Invoke();
    }

    // =====================================================
    // CONFIGURAR ENDERECO DO SERVIDOR
    // =====================================================

    public void SetServerAddress(
        string address,
        ushort port
    )
    {
        if (string.IsNullOrWhiteSpace(address) || port == 0)
        {
            Debug.LogError(
                "[MULTIPLAYER] Endereço ou porta inválidos."
            );

            return;
        }

        serverAddress = address.Trim();
        serverPort = port;

        Debug.Log(
            "[MULTIPLAYER] Servidor configurado: " +
            serverAddress + ":" + serverPort
        );
    }

    // =====================================================
    // DESTROY
    // =====================================================

    private void OnDestroy()
    {
        if (Instance != this)
            return;

        if (networkManager != null)
        {
            networkManager.ClientManager.OnClientConnectionState -=
                OnClientConnectionState;

            networkManager.ServerManager.OnServerConnectionState -=
                OnServerConnectionState;
        }

        Instance = null;
    }
}
