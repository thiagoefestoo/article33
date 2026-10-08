using System;
using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;

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

    [Header("Servidor")]
    [SerializeField]
    private string serverAddress = "localhost";

    [SerializeField]
    private ushort serverPort = 7770;

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
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (networkManager == null)
        {
            networkManager =
                FindAnyObjectByType<NetworkManager>();
        }

        if (networkManager == null)
        {
            Debug.LogError(
                "[MULTIPLAYER] FishNet NetworkManager não encontrado."
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
    // INICIALIZAÇÃO AUTOMÁTICA
    // =====================================================

    private void StartNetworkAutomatically()
    {
        Debug.Log(
            "[MULTIPLAYER] Modo de rede: "
            + startMode
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
        Debug.Log(
            "[MULTIPLAYER] Iniciando HOST local de desenvolvimento."
        );

        if (!networkManager.ServerManager.Started)
        {
            networkManager.ServerManager.StartConnection();
        }

        if (!networkManager.ClientManager.Started)
        {
            networkManager.ClientManager.StartConnection(
                serverAddress,
                serverPort
            );
        }
    }


    // =====================================================
    // CLIENTE
    // =====================================================

    public void StartClient()
    {
        if (networkManager == null)
            return;

        if (networkManager.ClientManager.Started)
            return;

        Debug.Log(
            "[MULTIPLAYER] Cliente conectando em "
            + serverAddress
            + ":"
            + serverPort
        );

        networkManager.ClientManager.StartConnection(
            serverAddress,
            serverPort
        );
    }


    // =====================================================
    // SERVIDOR DEDICADO
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
    // CLIENT CONNECTION STATE
    // =====================================================

    private void OnClientConnectionState(
        ClientConnectionStateArgs args
    )
    {
        Debug.Log(
            "[MULTIPLAYER] Estado CLIENTE: "
            + args.ConnectionState
        );

        if (
            args.ConnectionState ==
            LocalConnectionState.Started
        )
        {
            Debug.Log(
                "[MULTIPLAYER] CLIENTE CONECTADO."
            );

            SetNetworkReady();
        }

        if (
            args.ConnectionState ==
            LocalConnectionState.Stopped
        )
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
            "[MULTIPLAYER] Estado SERVIDOR: "
            + args.ConnectionState
        );

        if (
            args.ConnectionState ==
            LocalConnectionState.Started
        )
        {
            Debug.Log(
                "[MULTIPLAYER] SERVIDOR INICIADO."
            );

            /*
             * Dedicated Server não possui cliente local.
             * Portanto, estar com servidor iniciado já
             * significa que a rede está pronta.
             */
            if (
                startMode ==
                NetworkStartMode.DedicatedServer
            )
            {
                SetNetworkReady();
            }
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
    // CONFIGURA SERVIDOR
    // =====================================================

    public void SetServerAddress(
        string address,
        ushort port
    )
    {
        serverAddress = address;
        serverPort = port;
    }


    // =====================================================
    // DESTROY
    // =====================================================

    private void OnDestroy()
    {
        if (networkManager != null)
        {
            networkManager.ClientManager.OnClientConnectionState -=
                OnClientConnectionState;

            networkManager.ServerManager.OnServerConnectionState -=
                OnServerConnectionState;
        }

        if (Instance == this)
        {
            Instance = null;
        }
    }
}