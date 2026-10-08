using System.Collections;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Header("Inicialização")]
    [SerializeField]
    private float networkTimeoutSeconds = 20f;

    private bool loadStarted = false;


    // =====================================================
    // AWAKE
    // =====================================================

    private void Awake()
    {
        Debug.Log(
            "===== AWAKE GAME INITIALIZER ====="
        );
    }


    // =====================================================
    // START
    // =====================================================

    private void Start()
    {
        Debug.Log(
            "===== START GAME INITIALIZER ====="
        );

        StartCoroutine(
            InitializeGame()
        );
    }


    // =====================================================
    // INICIALIZAÇÃO
    // =====================================================

    private IEnumerator InitializeGame()
    {
        // =============================================
        // PLAYER SESSION
        // =============================================

        if (PlayerSession.Instance == null)
        {
            Debug.LogError(
                "[GAME] PlayerSession NÃO EXISTE."
            );

            yield break;
        }


        // =============================================
        // USUÁRIO AUTENTICADO
        // =============================================

        if (!PlayerSession.Instance.authenticated)
        {
            Debug.LogWarning(
                "[GAME] Nenhum usuário autenticado."
            );

            yield break;
        }


        // =============================================
        // PERSONAGEM SELECIONADO
        // =============================================

        if (PlayerSession.Instance.characterId <= 0)
        {
            Debug.LogError(
                "[GAME] Nenhum personagem selecionado."
            );

            yield break;
        }


        // =============================================
        // UNITY SERVICE
        // =============================================

        if (UnityService.Instance == null)
        {
            Debug.LogError(
                "[GAME] UnityService NÃO EXISTE."
            );

            yield break;
        }


        // =============================================
        // AGUARDA BOOTSTRAP MULTIPLAYER
        // =============================================

        Debug.Log(
            "[GAME] Aguardando inicialização do multiplayer..."
        );

        float elapsedTime = 0f;


        while (
            Artigo33NetworkBootstrap.Instance == null &&
            elapsedTime < networkTimeoutSeconds
        )
        {
            elapsedTime +=
                Time.unscaledDeltaTime;

            yield return null;
        }


        if (
            Artigo33NetworkBootstrap.Instance == null
        )
        {
            Debug.LogError(
                "[GAME] MultiplayerBootstrap não foi encontrado."
            );

            yield break;
        }


        // =============================================
        // AGUARDA REDE PRONTA
        // =============================================

        while (
            !Artigo33NetworkBootstrap.Instance.IsNetworkReady &&
            elapsedTime < networkTimeoutSeconds
        )
        {
            elapsedTime +=
                Time.unscaledDeltaTime;

            yield return null;
        }


        if (
            !Artigo33NetworkBootstrap.Instance.IsNetworkReady
        )
        {
            Debug.LogError(
                "[GAME] Timeout aguardando conexão com "
                + "o servidor multiplayer."
            );

            yield break;
        }


        Debug.Log(
            "[GAME] Rede multiplayer pronta."
        );


        // =============================================
        // EVITA DUPLICIDADE
        // =============================================

        if (loadStarted)
        {
            Debug.LogWarning(
                "[GAME] Carregamento do personagem "
                + "já foi iniciado."
            );

            yield break;
        }


        loadStarted = true;


        // =============================================
        // DADOS DA SESSÃO
        // =============================================

        int userId =
            PlayerSession.Instance.userId;

        int characterId =
            PlayerSession.Instance.characterId;


        Debug.Log(
            "[GAME] Carregando personagem selecionado."
        );


        Debug.Log(
            "[GAME] UserId: "
            + userId
            + " | CharacterId: "
            + characterId
        );


        // =============================================
        // CARREGA DADOS DO BACKEND
        // =============================================

        UnityService.Instance.LoadPlayer(
            userId,
            characterId
        );
    }
}