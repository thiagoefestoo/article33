using UnityEngine;


public class PlayerSession : MonoBehaviour
{
    public static PlayerSession Instance;


    // =====================================================
    // USUÁRIO
    // =====================================================

    public int userId;

    public string username;

    public bool authenticated;


    // =====================================================
    // PERSONAGEM
    // =====================================================

    public int characterId;

    public string playerName;

    public bool connected;


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
    // USUÁRIO AUTENTICADO
    // =====================================================

    public void SetAuthenticatedUser(
        int id,
        string user
    )
    {
        userId = id;

        username = user;

        authenticated = true;


        Debug.Log(
            "[PLAYER SESSION] Usuário autenticado: "
            + username
            + " | ID: "
            + userId
        );
    }


    // =====================================================
    // PERSONAGEM ONLINE
    // =====================================================

    public void ConnectPlayer(
        int id,
        int charId,
        string name
    )
    {
        userId = id;

        characterId = charId;

        playerName = name;

        connected = true;


        Debug.Log(
            "[PLAYER SESSION] Personagem online: "
            + playerName
            + " | CharacterId: "
            + characterId
        );
    }


    // =====================================================
    // LIMPAR SESSÃO
    // =====================================================

    public void Logout()
    {
        userId = 0;

        username = "";

        authenticated = false;


        characterId = 0;

        playerName = "";

        connected = false;


        Debug.Log(
            "[PLAYER SESSION] Sessão encerrada."
        );
    }
}