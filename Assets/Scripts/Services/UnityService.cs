using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UnityService : MonoBehaviour
{
    public static UnityService Instance;


    // =========================================================
    // AWAKE
    // =========================================================

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
        }
    }


    // =========================================================
    // CARREGAR JOGADOR
    // =========================================================

    public void LoadPlayer(
        int userId,
        int characterId
    )
    {
        Debug.Log(
            "[UnityService] Carregando jogador."
        );

        Debug.Log(
            "[UnityService] UserId: "
            + userId
            + " | CharacterId: "
            + characterId
        );

        StartCoroutine(
            LoadPlayerRequest(
                userId,
                characterId
            )
        );
    }


    // =========================================================
    // REQUEST LOAD
    // =========================================================

    private IEnumerator LoadPlayerRequest(
        int userId,
        int characterId
    )
    {
        string url =
            ApiConfig.BaseURL
            + "/api/unity/load-character/"
            + characterId;


        Debug.Log(
            "[UnityService] URL: "
            + url
        );


        using (
            UnityWebRequest request =
                UnityWebRequest.Get(url)
        )
        {
            request.SetRequestHeader(
                "Content-Type",
                "application/json"
            );


            yield return request.SendWebRequest();


            if (
                request.result !=
                UnityWebRequest.Result.Success
            )
            {
                Debug.LogError(
                    "[UnityService] Erro API | HTTP: "
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
                        "[UnityService] Servidor respondeu: "
                        + request.downloadHandler.text
                    );
                }


                yield break;
            }


            string json =
                request.downloadHandler.text;


            Debug.Log(
                "[UnityService] JSON recebido:"
            );

            Debug.Log(json);


            PlayerResponse response =
                JsonUtility.FromJson<PlayerResponse>(
                    json
                );


            if (response == null)
            {
                Debug.LogError(
                    "[UnityService] Response vazio."
                );

                yield break;
            }


            if (!response.success)
            {
                Debug.LogError(
                    "[UnityService] Falha no carregamento."
                );

                yield break;
            }


            if (response.player == null)
            {
                Debug.LogError(
                    "[UnityService] Player vazio."
                );

                yield break;
            }


            // =================================================
            // SEGURANÇA / CONSISTÊNCIA
            // =================================================

            if (
                response.player.characterId
                != characterId
            )
            {
                Debug.LogError(
                    "[UnityService] CharacterId retornado é diferente do selecionado."
                );

                yield break;
            }


            // =================================================
            // PLAYER MANAGER
            // =================================================

            if (PlayerManager.Instance != null)
            {
                PlayerManager.Instance.SetPlayer(
                    response.player
                );
            }
            else
            {
                Debug.LogError(
                    "[UnityService] PlayerManager.Instance não encontrado."
                );

                yield break;
            }


            // =================================================
            // IMPORTANTE:
            // O PERSONAGEM NÃO É MAIS INSTANCIADO AQUI.
            //
            // O PlayerArmature agora é criado pelo
            // FishNet PlayerSpawner quando o cliente conecta.
            // =================================================

            Debug.Log(
                "[UnityService] Dados do personagem carregados."
                + "\nNome: "
                + response.player.name
                + "\nCharacterId: "
                + response.player.characterId
                + "\nSpawn: controlado pelo FishNet."
            );


            // =================================================
            // ATUALIZAR SESSÃO
            // =================================================

            if (PlayerSession.Instance != null)
            {
                PlayerSession.Instance.ConnectPlayer(
                    userId,
                    characterId,
                    response.player.name
                );
            }


            Debug.Log(
                "[UnityService] Jogador carregado com sucesso."
            );


            // =================================================
            // INICIAR HEARTBEAT / SESSÃO API
            // =================================================

            if (NetworkClient.Instance != null)
            {
                NetworkClient.Instance.StartPlayerSession(
                    userId,
                    characterId,
                    response.player.name
                );


                Debug.Log(
                    "[UnityService] Sessão online iniciada para: "
                    + response.player.name
                    + " | UserId: "
                    + userId
                    + " | CharacterId: "
                    + characterId
                );
            }
            else
            {
                Debug.LogError(
                    "[UnityService] NetworkClient.Instance não encontrado."
                );
            }
        }
    }


    // =========================================================
    // SALVAR JOGADOR
    // =========================================================

    public void SavePlayer(
        PlayerData player
    )
    {
        if (player == null)
        {
            Debug.LogError(
                "[UnityService] Player vazio ao salvar."
            );

            return;
        }


        StartCoroutine(
            SavePlayerRequest(
                player
            )
        );
    }


    // =========================================================
    // REQUEST SAVE
    // =========================================================

    private IEnumerator SavePlayerRequest(
        PlayerData player
    )
    {
        string url =
            ApiConfig.BaseURL
            + "/api/unity/save";


        string json =
            JsonUtility.ToJson(
                player
            );


        Debug.Log(
            "[UnityService] Salvando jogador:"
        );

        Debug.Log(json);


        using (
            UnityWebRequest request =
                new UnityWebRequest(
                    url,
                    "POST"
                )
        )
        {
            byte[] body =
                System.Text.Encoding.UTF8
                    .GetBytes(json);


            request.uploadHandler =
                new UploadHandlerRaw(body);


            request.downloadHandler =
                new DownloadHandlerBuffer();


            request.SetRequestHeader(
                "Content-Type",
                "application/json"
            );


            yield return request.SendWebRequest();


            if (
                request.result ==
                UnityWebRequest.Result.Success
            )
            {
                Debug.Log(
                    "[UnityService] Jogador salvo com sucesso."
                );


                if (
                    request.downloadHandler != null &&
                    !string.IsNullOrEmpty(
                        request.downloadHandler.text
                    )
                )
                {
                    Debug.Log(
                        "[UnityService] Resposta save: "
                        + request.downloadHandler.text
                    );
                }
            }
            else
            {
                Debug.LogError(
                    "[UnityService] Erro ao salvar | HTTP: "
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
                        "[UnityService] Servidor respondeu: "
                        + request.downloadHandler.text
                    );
                }
            }
        }
    }
}