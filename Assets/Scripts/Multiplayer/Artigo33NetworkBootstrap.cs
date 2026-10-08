
using System;
using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;

// =====================================================
// ARTIGO33 - MULTIPLAYER NETWORK BOOTSTRAP
// Unity 6 + FishNet + AWS EC2
//
// Editor: DevelopmentHost (configuravel)
// Windows: Client
// Linux Dedicated Server: DedicatedServer
//
// Transporte FishNet: UDP 7770
// API HTTPS: configurada separadamente em ApiConfig
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

    [Header("Modo de Inicializacao")]
    [SerializeField]
    private NetworkStartMode startMode =
        NetworkStartMode.DevelopmentHost;

    [Header("Servidor Multiplayer AWS")]
    [SerializeField]
    private string serverAddress = "54.94.200.173";

    [SerializeField]
    private ushort serverPort = 7770;

    [Header("Desenvolvimento Local")]
    [SerializeField]
    private string localServerAddress = "127.0.0.1";

    private NetworkStartMode activeMode;
    private bool eventsRegistered;

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
                "[MULTIPLAYER] NetworkManager nao encontrado."
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

        eventsRegistered = true;

        StartNetworkAutomatically();
    }

    // =====================================================
    // SELECIONAR MODO CONFORME O AMBIENTE
    // =====================================================

    private void StartNetworkAutomatically()
    {
#if UNITY_SERVER
        activeMode = NetworkStartMode.DedicatedServer;
#elif UNITY_EDITOR
        activeMode = startMode;
#else
        activeMode = NetworkStartMode.Client;
#endif

        Debug.Log(
            "[MULTIPLAYER] Modo selecionado: " + activeMode
        );

        switch (activeMode)
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
    // HOST LOCAL - UNITY EDITOR
    // =====================================================

    private void StartDevelopmentHost()
    {
        if (networkManager == null)
            return;

        Debug.Log(
            "[MULTIPLAYER] Iniciando host local."
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
                "[MULTIPLAYER] NetworkManager indisponivel."
            );
            return;
        }

#if UNITY_SERVER
        Debug.LogWarning(
            "[MULTIPLAYER] Servidor dedicado nao inicia cliente."
        );
        return;
#endif

        if (networkManager.ClientManager.Started)
            return;

        string address =
            activeMode == NetworkStartMode.DevelopmentHost
                ? localServerAddress
                : serverAddress;

        Debug.Log(
            "[MULTIPLAYER] Conectando em " +
            address + ":" + serverPort
        );

        networkManager.ClientManager.StartConnection(
            address,
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
            "[MULTIPLAYER] Iniciando servidor dedicado."
        );

        networkManager.ServerManager.StartConnection();
    }

    // =====================================================
    // ESTADO DO CLIENTE
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
            SetNetworkReady();
        }
        else if (args.ConnectionState ==
                 LocalConnectionState.Stopped)
        {
            if (activeMode != NetworkStartMode.DedicatedServer)
            {
                IsNetworkReady = false;
            }

            Debug.Log(
                "[MULTIPLAYER] Cliente desconectado."
            );
        }
    }

    // =====================================================
    // ESTADO DO SERVIDOR
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
                "[MULTIPLAYER] Servidor iniciado."
            );

            if (activeMode ==
                NetworkStartMode.DedicatedServer)
            {
                SetNetworkReady();
            }
        }
        else if (args.ConnectionState ==
                 LocalConnectionState.Stopped)
        {
            if (activeMode ==
                NetworkStartMode.DedicatedServer)
            {
                IsNetworkReady = false;
            }

            Debug.Log(
                "[MULTIPLAYER] Servidor encerrado."
            );
        }
    }

    // =====================================================
    // REDE PRONTA
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
    // CONFIGURAR ENDERECO MULTIPLAYER
    // =====================================================

    public void SetServerAddress(
        string address,
        ushort port
    )
    {
        if (string.IsNullOrWhiteSpace(address) || port == 0)
        {
            Debug.LogError(
                "[MULTIPLAYER] Endereco ou porta invalidos."
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

        if (networkManager != null && eventsRegistered)
        {
            networkManager.ClientManager.OnClientConnectionState -=
                OnClientConnectionState;

            networkManager.ServerManager.OnServerConnectionState -=
                OnServerConnectionState;
        }

        eventsRegistered = false;
        IsNetworkReady = false;
        Instance = null;
    }
}
