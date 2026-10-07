using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [Header("Campos")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;

    [Header("Interface")]
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_Text statusText;

    private void Start()
    {
        if (statusText != null)
        {
            statusText.text = "";
        }
    }

    public void Login()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text;

        if (string.IsNullOrWhiteSpace(username))
        {
            SetStatus("Informe seu usuário.");
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            SetStatus("Informe sua senha.");
            return;
        }

        if (LoginManager.Instance == null)
        {
            SetStatus("Sistema de login indisponível.");
            Debug.LogError("[LOGIN UI] LoginManager não encontrado.");
            return;
        }

        loginButton.interactable = false;
        SetStatus("Entrando...");

        LoginManager.Instance.Login(
            username,
            password,
            (success, message) =>
            {
                loginButton.interactable = true;

                if (!success)
                {
                    SetStatus("Usuário ou senha inválidos.");
                    return;
                }

                SetStatus("Login realizado.");

                Debug.Log(
                    "[LOGIN UI] Login realizado. Indo para seleção de personagem."
                );

                SceneManager.LoadScene("CharacterSelection");
            }
        );
    }

    private void SetStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
    }
}