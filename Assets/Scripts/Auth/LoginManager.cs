
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

// =====================================================
// ARTIGO33 - LOGIN MANAGER
// UNITY 6 + AWS HTTPS + NEON POSTGRESQL
//
// Responsabilidades:
// - Enviar login para a API
// - Validar a resposta
// - Atualizar PlayerSession
// - Retornar resultado para a interface
// =====================================================

public class LoginManager : MonoBehaviour
{
    public static LoginManager Instance;

    // =====================================================
    // SINGLETON
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
        }
    }

    // =====================================================
    // LOGIN PUBLICO
    // =====================================================

    public void Login(
        string username,
        string password,
        Action<bool, string> callback
    )
    {
        StartCoroutine(
            LoginCoroutine(
                username,
                password,
                callback
            )
        );
    }

    // =====================================================
    // COMUNICACAO COM A API
    // =====================================================

    private IEnumerator LoginCoroutine(
        string username,
        string password,
        Action<bool, string> callback
    )
    {
        if (string.IsNullOrWhiteSpace(ApiConfig.BaseURL))
        {
            Debug.LogError("[AUTH] URL da API não configurada.");

            callback?.Invoke(
                false,
                "Servidor não configurado."
            );

            yield break;
        }

        string url =
            ApiConfig.BaseURL.TrimEnd('/') +
            "/api/auth/login";

        LoginRequest loginRequest = new LoginRequest
        {
            username = username,
            password = password
        };

        string json = JsonUtility.ToJson(loginRequest);

        byte[] body = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request =
            new UnityWebRequest(
                url,
                UnityWebRequest.kHttpVerbPOST
            ))
        {
            request.uploadHandler = new UploadHandlerRaw(body);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader(
                "Content-Type",
                "application/json"
            );

            request.timeout = 30;

            Debug.Log(
                "[AUTH] Solicitando login via API HTTPS."
            );

            yield return request.SendWebRequest();

            // =================================================
            // ERRO DE CONEXAO OU RESPOSTA HTTP
            // =================================================

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning(
                    "[AUTH] Falha no login. HTTP: " +
                    request.responseCode +
                    " | " +
                    request.error
                );

                string message =
                    request.responseCode == 401
                        ? "Usuário ou senha inválidos."
                        : request.responseCode == 429
                            ? "Muitas tentativas. Aguarde e tente novamente."
                            : "Não foi possível realizar o login. Verifique a conexão ou tente novamente.";

                callback?.Invoke(false, message);
                yield break;
            }

            // =================================================
            // VALIDAR RESPOSTA DO SERVIDOR
            // =================================================

            string responseJson =
                request.downloadHandler != null
                    ? request.downloadHandler.text
                    : string.Empty;

            LoginResponse response;

            try
            {
                response = JsonUtility.FromJson<LoginResponse>(
                    responseJson
                );
            }
            catch (Exception ex)
            {
                Debug.LogError(
                    "[AUTH] Resposta JSON inválida: " +
                    ex.Message
                );

                callback?.Invoke(
                    false,
                    "Resposta inválida do servidor."
                );

                yield break;
            }

            if (response == null || response.userId <= 0)
            {
                callback?.Invoke(
                    false,
                    "Resposta de login inválida."
                );

                yield break;
            }

            // =================================================
            // VALIDAR PLAYER SESSION
            // =================================================

            if (PlayerSession.Instance == null)
            {
                Debug.LogError(
                    "[AUTH] PlayerSession não encontrado."
                );

                callback?.Invoke(
                    false,
                    "Sistema de sessão indisponível."
                );

                yield break;
            }

            // =================================================
            // SALVAR USUARIO AUTENTICADO
            // =================================================

            PlayerSession.Instance.SetAuthenticatedUser(
                response.userId,
                response.username
            );

            Debug.Log(
                "[AUTH] Login concluído. UserId: " +
                response.userId
            );

            callback?.Invoke(
                true,
                response.message
            );
        }
    }
}
