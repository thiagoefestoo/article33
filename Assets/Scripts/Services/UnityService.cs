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
            // ATUALIZAR PLAYER MANAGER
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
            // SPAWN DO PERSONAGEM
            // =================================================

            if (PlayerSpawner.Instance != null)
            {
                GameObject spawnedPlayer =
                    PlayerSpawner.Instance.SpawnPlayer(
                        response.player
                    );


                if (spawnedPlayer != null)
                {
                    Debug.Log(
                        "[UnityService] Personagem instanciado com sucesso: "
                        + response.player.name
                        + " | CharacterId: "
                        + response.player.characterId
                    );
                }
                else
                {
                    Debug.LogError(
                        "[UnityService] Falha ao instanciar personagem."
                    );
                }
            }
            else
            {
                Debug.LogError(
                    "[UnityService] PlayerSpawner.Instance não encontrado."
                );
            }


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
            // INICIAR SESSÃO ONLINE
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