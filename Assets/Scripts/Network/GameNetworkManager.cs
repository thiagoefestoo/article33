using System.Collections;
using UnityEngine;

public class GameNetworkManager : MonoBehaviour
{
    public static GameNetworkManager Instance;


    public bool ServerConnected { get; private set; }

    public string PlayerID { get; private set; }

    public int UserId { get; private set; }

    public int CharacterId { get; private set; }

    public string PlayerName { get; private set; }


    // =====================================================
    // AWAKE
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
    // START
    // =====================================================

    private void Start()
    {
        StartCoroutine(
            CheckServer()
        );
    }


    // =====================================================
    // VERIFICAR SERVIDOR / SESSÃO
    // =====================================================

    private IEnumerator CheckServer()
    {
        Debug.Log(
            "[GameNetworkManager] Verificando servidor..."
        );


        // Por enquanto ainda é uma verificação local simples.
        // Depois podemos trocar por WebSocket/FishNet real.

        yield return new WaitForSeconds(1f);


        ServerConnected = true;


        // =================================================
        // PEGAR SESSÃO REAL
        // =================================================

        if (PlayerSession.Instance != null)
        {
            UserId =
                PlayerSession.Instance.userId;

            CharacterId =
                PlayerSession.Instance.characterId;

            PlayerName =
                PlayerSession.Instance.playerName;


            if (CharacterId > 0)
            {
                PlayerID =
                    "CHARACTER_"
                    + CharacterId;
            }
            else if (UserId > 0)
            {
                PlayerID =
                    "USER_"
                    + UserId;
            }
            else
            {
                PlayerID =
                    "NOT_AUTHENTICATED";
            }
        }
        else
        {
            UserId = 0;

            CharacterId = 0;

            PlayerName = "";

            PlayerID =
                "NO_SESSION";


            Debug.LogWarning(
                "[GameNetworkManager] PlayerSession.Instance não encontrado."
            );
        }


        Debug.Log(
            "[GameNetworkManager] Servidor conectado."
        );


        Debug.Log(
            "[GameNetworkManager] PlayerID: "
            + PlayerID
        );


        Debug.Log(
            "[GameNetworkManager] UserId: "
            + UserId
            + " | CharacterId: "
            + CharacterId
            + " | PlayerName: "
            + PlayerName
        );
    }


    // =====================================================
    // ATUALIZAR DADOS DA SESSÃO
    // =====================================================

    public void RefreshSession()
    {
        if (PlayerSession.Instance == null)
        {
            Debug.LogWarning(
                "[GameNetworkManager] Não foi possível atualizar a sessão."
            );

            return;
        }


        UserId =
            PlayerSession.Instance.userId;

        CharacterId =
            PlayerSession.Instance.characterId;

        PlayerName =
            PlayerSession.Instance.playerName;


        PlayerID =
            CharacterId > 0
                ? "CHARACTER_" + CharacterId
                : "USER_" + UserId;


        Debug.Log(
            "[GameNetworkManager] Sessão atualizada: "
            + PlayerID
        );
    }


    // =====================================================
    // DESTROY
    // =====================================================

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}