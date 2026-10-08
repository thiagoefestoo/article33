
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

// =====================================================
// ARTIGO33 - NETWORK CLIENT
// UNITY 6 + AWS HTTPS + NEON POSTGRESQL
//
// Sistemas:
// - Carregamento do jogador
// - Sessao persistente
// - Heartbeat
// - Controle de jogador online
//
// Comunicacao HTTP com a API .NET 10.
// O multiplayer FishNet e configurado separadamente.
// =====================================================

public class NetworkClient : MonoBehaviour
{
    public static NetworkClient Instance;

    // =====================================================
    // CONFIGURACAO CENTRAL DA API
    // =====================================================

    private string ApiUrl =>
        ApiConfig.BaseURL.TrimEnd('/');

    private int currentCharacterId;

    private Coroutine heartbeatCoroutine;

    // =====================================================
    // AWAKE
    // =====================================================

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            Debug.Log(
                "[NetworkClient] Inicializado. API: " + ApiUrl
            );
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // =====================================================
    // CARREGAR PLAYER
    // LEGADO / COMPATIBILIDADE
    // =====================================================

    public IEnumerator LoadPlayer(int userId)
    {
        string url =
            ApiUrl +
            "/api/unity/load/" +
            userId;

        Debug.Log(
            "[NetworkClient] Conectando API: " + url
        );

        using (UnityWebRequest request =
            UnityWebRequest.Get(url))
        {
            request.timeout = 30;

            yield return request.SendWebRequest();

            if (request.result ==
                UnityWebRequest.Result.Success)
            {
                Debug.Log(
                    "[NetworkClient] Resposta servidor:"
                );

                Debug.Log(
                    request.downloadHandler.text
                );

                UnityPlayerData data = null;

                try
                {
                    data =
                        JsonUtility.FromJson<UnityPlayerData>(
                            request.downloadHandler.text
                        );
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(
                        "[NetworkClient] Erro ao interpretar jogador: "
                        + ex.Message
                    );

                    yield break;
                }

                if (
                    data != null &&
                    data.success &&
                    data.player != null
                )
                {
                    Debug.Log(
                        "[NetworkClient] Jogador recebido: "
                        + data.player.name
                    );

                    StartPlayerSession(
                        userId,
                        data.player.characterId,
                        data.player.name
                    );
                }
                else
                {
                    Debug.LogError(
                        "[NetworkClient] Dados do jogador inválidos."
                    );
                }
            }
            else
            {
                Debug.LogError(
                    "[NetworkClient] Erro conexão | HTTP: "
                    + request.responseCode
                    + " | "
                    + request.error
                );
            }
        }
    }

    // =====================================================
    // INICIAR SESSAO DO PLAYER
    // =====================================================

    public void StartPlayerSession(
        int userId,
        int characterId,
        string playerName
    )
    {
        // =================================================
        // VALIDAR PLAYER SESSION
        // =================================================

        if (PlayerSession.Instance == null)
        {
            Debug.LogError(
                "[NetworkClient] PlayerSession.Instance não encontrado."
            );

            return;
        }

        // =================================================
        // ATUALIZAR SESSAO GLOBAL
        // =================================================

        PlayerSession.Instance.ConnectPlayer(
            userId,
            characterId,
            playerName
        );

        currentCharacterId = characterId;

        Debug.Log(
            "[NetworkClient] Personagem ONLINE: "
            + playerName
            + " | UserId: "
            + userId
            + " | CharacterId: "
            + characterId
        );

        // =================================================
        // EVITAR HEARTBEAT DUPLICADO
        // =================================================

        if (heartbeatCoroutine != null)
        {
            StopCoroutine(heartbeatCoroutine);

            heartbeatCoroutine = null;
        }

        heartbeatCoroutine =
            StartCoroutine(HeartbeatLoop());
    }

    // =====================================================
    // LOOP HEARTBEAT
    // =====================================================

    private IEnumerator HeartbeatLoop()
    {
        Debug.Log(
            "[NetworkClient] Sistema heartbeat iniciado."
        );

        // Primeiro heartbeat imediato
        if (currentCharacterId > 0)
        {
            yield return StartCoroutine(
                SendHeartbeat(currentCharacterId)
            );
        }

        while (true)
        {
            yield return new WaitForSeconds(10f);

            if (currentCharacterId > 0)
            {
                yield return StartCoroutine(
                    SendHeartbeat(currentCharacterId)
                );
            }
        }
    }

    // =====================================================
    // ENVIAR HEARTBEAT
    // =====================================================

    private IEnumerator SendHeartbeat(
        int characterId
    )
    {
        string url =
            ApiUrl +
            "/api/unity/heartbeat";

        HeartbeatRequest heartbeat =
            new HeartbeatRequest
            {
                characterId = characterId
            };

        string json =
            JsonUtility.ToJson(heartbeat);

        Debug.Log(
            "[NetworkClient] Enviando heartbeat: " + json
        );

        byte[] body =
            Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request =
            new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler =
                new UploadHandlerRaw(body);

            request.downloadHandler =
                new DownloadHandlerBuffer();

            request.SetRequestHeader(
                "Content-Type",
                "application/json"
            );

            request.timeout = 30;

            yield return request.SendWebRequest();

            if (request.result ==
                UnityWebRequest.Result.Success)
            {
                Debug.Log(
                    "[NetworkClient] Heartbeat enviado | CharacterId: "
                    + characterId
                );

                if (
                    request.downloadHandler != null &&
                    !string.IsNullOrEmpty(
                        request.downloadHandler.text
                    )
                )
                {
                    Debug.Log(
                        "[NetworkClient] Resposta heartbeat: "
                        + request.downloadHandler.text
                    );
                }
            }
            else
            {
                Debug.LogError(
                    "[NetworkClient] Falha heartbeat | HTTP: "
                    + request.responseCode
                    + " | Erro: "
                    + request.error
                );

                if (
                    request.downloadHandler != null &&
                    !string.IsNullOrEmpty(
                        request.downloadHandler.text
                    )
                )
                {
                    Debug.LogError(
                        "[NetworkClient] Servidor respondeu: "
                        + request.downloadHandler.text
                    );
                }
            }
        }
    }

    // =====================================================
    // PARAR SESSAO LOCAL
    // =====================================================

    public void StopPlayerSession()
    {
        if (heartbeatCoroutine != null)
        {
            StopCoroutine(heartbeatCoroutine);

            heartbeatCoroutine = null;
        }

        currentCharacterId = 0;

        Debug.Log(
            "[NetworkClient] Heartbeat encerrado."
        );
    }

    // =====================================================
    // DESTROY
    // =====================================================

    private void OnDestroy()
    {
        if (Instance == this)
        {
            StopPlayerSession();

            Instance = null;
        }
    }
}
