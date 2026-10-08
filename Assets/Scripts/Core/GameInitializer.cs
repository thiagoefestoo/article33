
using System.Collections;
using UnityEngine;

// =====================================================
// ARTIGO33 - GAME INITIALIZER
// UNITY 6 + FISHNET + AWS EC2 + NEON
//
// CLIENTE:
// - Validar sessao e autenticacao
// - Aguardar inicializacao do FishNet
// - Carregar personagem pela API HTTPS
//
// SERVIDOR DEDICADO:
// - Nao exigir login ou PlayerSession
// - Nao carregar personagem local
// - Deixar FishNet Bootstrap iniciar servidor
// =====================================================

public class GameInitializer : MonoBehaviour
{
    [Header("Inicialização")]
    [SerializeField]
    private float networkTimeoutSeconds = 20f;

    private bool loadStarted = false;

    private Coroutine initializationCoroutine;

    // =====================================================
    // AWAKE
    // =====================================================

    private void Awake()
    {
        Debug.Log(
            "[GAME] GameInitializer inicializado."
        );
    }

    // =====================================================
    // START
    // =====================================================

    private void Start()
    {
#if UNITY_SERVER

        // O servidor dedicado nao representa um jogador.
        // Portanto nao precisa de login ou personagem.

        Debug.Log(
            "[GAME] Executando em modo Dedicated Server."
        );

        Debug.Log(
            "[GAME] Inicializacao da rede delegada ao " +
            "Artigo33NetworkBootstrap."
        );

#else

        Debug.Log(
            "[GAME] Inicializando cliente Artigo33."
        );

        initializationCoroutine =
            StartCoroutine(InitializeGame());

#endif
    }

    // =====================================================
    // INICIALIZACAO DO CLIENTE
    // =====================================================

    private IEnumerator InitializeGame()
    {
        // =============================================
        // VALIDAR PLAYER SESSION
        // =============================================

        if (PlayerSession.Instance == null)
        {
            Debug.LogError(
                "[GAME] PlayerSession nao encontrado."
            );

            yield break;
        }

        // =============================================
        // VALIDAR AUTENTICACAO
        // =============================================

        if (!PlayerSession.Instance.authenticated)
        {
            Debug.LogWarning(
                "[GAME] Nenhum usuario autenticado."
            );

            yield break;
        }

        // =============================================
        // VALIDAR PERSONAGEM
        // =============================================

        if (PlayerSession.Instance.characterId <= 0)
        {
            Debug.LogError(
                "[GAME] Nenhum personagem selecionado."
            );

            yield break;
        }

        // =============================================
        // VALIDAR UNITY SERVICE
        // =============================================

        if (UnityService.Instance == null)
        {
            Debug.LogError(
                "[GAME] UnityService nao encontrado."
            );

            yield break;
        }

        // =============================================
        // AGUARDAR FISHNET BOOTSTRAP
        // =============================================

        Debug.Log(
            "[GAME] Aguardando FishNet Bootstrap..."
        );

        float elapsedTime = 0f;

        while (
            Artigo33NetworkBootstrap.Instance == null &&
            elapsedTime < networkTimeoutSeconds
        )
        {
            elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }

        Artigo33NetworkBootstrap bootstrap =
            Artigo33NetworkBootstrap.Instance;

        if (bootstrap == null)
        {
            Debug.LogError(
                "[GAME] MultiplayerBootstrap nao encontrado."
            );

            yield break;
        }

        // =============================================
        // AGUARDAR REDE PRONTA
        // =============================================

        Debug.Log(
            "[GAME] Aguardando conexao FishNet..."
        );

        while (
            !bootstrap.IsNetworkReady &&
            elapsedTime < networkTimeoutSeconds
        )
        {
            elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }

        if (!bootstrap.IsNetworkReady)
        {
            Debug.LogError(
                "[GAME] Timeout aguardando multiplayer."
            );

            yield break;
        }

        Debug.Log(
            "[GAME] FishNet inicializado."
        );

        // =============================================
        // EVITAR CARREGAMENTO DUPLICADO
        // =============================================

        if (loadStarted)
        {
            Debug.LogWarning(
                "[GAME] Carregamento ja iniciado."
            );

            yield break;
        }

        // =============================================
        // REVALIDAR SESSAO
        // =============================================

        if (
            PlayerSession.Instance == null ||
            !PlayerSession.Instance.authenticated ||
            PlayerSession.Instance.characterId <= 0
        )
        {
            Debug.LogError(
                "[GAME] Sessao invalida apos conexao."
            );

            yield break;
        }

        if (UnityService.Instance == null)
        {
            Debug.LogError(
                "[GAME] UnityService indisponivel."
            );

            yield break;
        }

        // =============================================
        // OBTER IDENTIFICADORES
        // =============================================

        int userId =
            PlayerSession.Instance.userId;

        int characterId =
            PlayerSession.Instance.characterId;

        if (userId <= 0 || characterId <= 0)
        {
            Debug.LogError(
                "[GAME] Identificadores invalidos."
            );

            yield break;
        }

        // =============================================
        // ATUALIZAR GAME NETWORK MANAGER
        // =============================================

        if (GameNetworkManager.Instance != null)
        {
            GameNetworkManager.Instance.RefreshSession();
        }

        // =============================================
        // CARREGAR PERSONAGEM PELA API
        // =============================================

        loadStarted = true;

        Debug.Log(
            "[GAME] Carregando personagem via API HTTPS."
        );

        Debug.Log(
            "[GAME] UserId: " +
            userId +
            " | CharacterId: " +
            characterId
        );

        UnityService.Instance.LoadPlayer(
            userId,
            characterId
        );
    }

    // =====================================================
    // DESTROY
    // =====================================================

    private void OnDestroy()
    {
        if (initializationCoroutine != null)
        {
            StopCoroutine(initializationCoroutine);

            initializationCoroutine = null;
        }
    }
}
