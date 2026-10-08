
using System.Collections;
using UnityEngine;

// =====================================================
// ARTIGO33 - GAME NETWORK MANAGER
// UNITY 6 + FISHNET + AWS EC2
//
// Responsabilidades:
// - Monitorar a conexão FishNet
// - Consultar a sessão do jogador
// - Identificar usuário e personagem
// - Integrar com Artigo33NetworkBootstrap
//
// API HTTPS e Neon são independentes do transporte UDP.
// =====================================================

public class GameNetworkManager : MonoBehaviour
{
    public static GameNetworkManager Instance;

    // =====================================================
    // ESTADO DA REDE E DO JOGADOR
    // =====================================================

    public bool ServerConnected { get; private set; }

    public string PlayerID { get; private set; }

    public int UserId { get; private set; }

    public int CharacterId { get; private set; }

    public string PlayerName { get; private set; }

    private Artigo33NetworkBootstrap networkBootstrap;

    private Coroutine networkCheckCoroutine;

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

        ServerConnected = false;

        PlayerID = "NO_SESSION";

        PlayerName = string.Empty;
    }

    // =====================================================
    // START
    // =====================================================

    private void Start()
    {
        RefreshSession();

        networkCheckCoroutine = StartCoroutine(CheckServer());
    }

    // =====================================================
    // VERIFICAR CONEXÃO REAL DO FISHNET
    // =====================================================

    private IEnumerator CheckServer()
    {
        Debug.Log(
            "[GameNetworkManager] Iniciando monitoramento FishNet."
        );

        while (true)
        {
            if (networkBootstrap == null)
            {
                networkBootstrap =
                    Artigo33NetworkBootstrap.Instance;
            }

            bool connected = false;

            if (networkBootstrap != null)
            {
                // No servidor dedicado, a rede está pronta
                // quando o FishNet ServerManager está iniciado.
                //
                // No cliente, a conexão é indicada pelo
                // ClientManager do FishNet.

#if UNITY_SERVER
                connected = networkBootstrap.IsServerRunning;
#else
                connected = networkBootstrap.IsClientConnected;
#endif
            }

            if (ServerConnected != connected)
            {
                ServerConnected = connected;

                if (ServerConnected)
                {
                    Debug.Log(
                        "[GameNetworkManager] Rede FishNet iniciada."
                    );
                }
                else
                {
                    Debug.LogWarning(
                        "[GameNetworkManager] Rede FishNet desconectada."
                    );
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    // =====================================================
    // ATUALIZAR DADOS DA SESSÃO
    // =====================================================

    public void RefreshSession()
    {
        if (PlayerSession.Instance == null)
        {
            UserId = 0;
            CharacterId = 0;
            PlayerName = string.Empty;
            PlayerID = "NO_SESSION";

#if !UNITY_SERVER
            Debug.LogWarning(
                "[GameNetworkManager] PlayerSession não encontrado."
            );
#endif

            return;
        }

        UserId = PlayerSession.Instance.userId;

        CharacterId = PlayerSession.Instance.characterId;

        PlayerName = PlayerSession.Instance.playerName;

        if (CharacterId > 0)
        {
            PlayerID = "CHARACTER_" + CharacterId;
        }
        else if (UserId > 0)
        {
            PlayerID = "USER_" + UserId;
        }
        else
        {
            PlayerID = "NOT_AUTHENTICATED";
        }

        Debug.Log(
            "[GameNetworkManager] Sessão atualizada: " +
            PlayerID +
            " | UserId: " + UserId +
            " | CharacterId: " + CharacterId
        );
    }

    // =====================================================
    // DESTROY
    // =====================================================

    private void OnDestroy()
    {
        if (Instance != this)
            return;

        if (networkCheckCoroutine != null)
        {
            StopCoroutine(networkCheckCoroutine);
            networkCheckCoroutine = null;
        }

        ServerConnected = false;

        Instance = null;
    }
}
