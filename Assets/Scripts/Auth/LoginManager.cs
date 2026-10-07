using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LoginManager : MonoBehaviour
{
    public static LoginManager Instance;

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

    public void Login(
        string username,
        string password,
        System.Action<bool, string> callback
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

    private IEnumerator LoginCoroutine(
        string username,
        string password,
        System.Action<bool, string> callback
    )
    {
        string url =
            ApiConfig.BaseURL +
            "/api/auth/login";

        LoginRequest loginRequest =
            new LoginRequest
            {
                username = username,
                password = password
            };

        string json =
            JsonUtility.ToJson(loginRequest);

        byte[] body =
            System.Text.Encoding.UTF8.GetBytes(json);

        using UnityWebRequest request =
            new UnityWebRequest(
                url,
                UnityWebRequest.kHttpVerbPOST
            );

        request.uploadHandler =
            new UploadHandlerRaw(body);

        request.downloadHandler =
            new DownloadHandlerBuffer();

        request.SetRequestHeader(
            "Content-Type",
            "application/json"
        );

        Debug.Log(
            "[AUTH] Login solicitado: " +
            username
        );

        yield return request.SendWebRequest();

        if (
            request.result !=
            UnityWebRequest.Result.Success
        )
        {
            string serverMessage =
                request.downloadHandler != null
                    ? request.downloadHandler.text
                    : request.error;

            Debug.LogError(
                "[AUTH] Login recusado: " +
                serverMessage
            );

            callback?.Invoke(
                false,
                serverMessage
            );

            yield break;
        }

        string responseJson =
            request.downloadHandler.text;

        LoginResponse response =
            JsonUtility.FromJson<LoginResponse>(
                responseJson
            );

        if (
            response == null ||
            response.userId <= 0
        )
        {
            callback?.Invoke(
                false,
                "Resposta de login inválida."
            );

            yield break;
        }

        if (PlayerSession.Instance == null)
        {
            Debug.LogError(
                "[AUTH] PlayerSession não encontrado."
            );

            callback?.Invoke(
                false,
                "PlayerSession não encontrado."
            );

            yield break;
        }

        PlayerSession.Instance
            .SetAuthenticatedUser(
                response.userId,
                response.username
            );

        Debug.Log(
            "[AUTH] Login realizado. UserId: " +
            response.userId +
            " | Username: " +
            response.username
        );

        callback?.Invoke(
            true,
            response.message
        );
    }
}