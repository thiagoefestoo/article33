using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(
            "===== AWAKE GAME INITIALIZER ====="
        );
    }


    private void Start()
    {
        Debug.Log(
            "===== START GAME INITIALIZER ====="
        );


        // =============================================
        // PLAYER SESSION
        // =============================================

        if (PlayerSession.Instance == null)
        {
            Debug.LogError(
                "[GAME] PlayerSession NÃO EXISTE."
            );

            return;
        }


        // =============================================
        // USUÁRIO PRECISA ESTAR LOGADO
        // =============================================

        if (!PlayerSession.Instance.authenticated)
        {
            Debug.LogWarning(
                "[GAME] Nenhum usuário autenticado."
            );

            return;
        }


        // =============================================
        // PERSONAGEM PRECISA ESTAR SELECIONADO
        // =============================================

        if (PlayerSession.Instance.characterId <= 0)
        {
            Debug.LogError(
                "[GAME] Nenhum personagem selecionado."
            );

            return;
        }


        // =============================================
        // UNITY SERVICE
        // =============================================

        if (UnityService.Instance == null)
        {
            Debug.LogError(
                "[GAME] UnityService NÃO EXISTE."
            );

            return;
        }


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


        UnityService.Instance.LoadPlayer(
            userId,
            characterId
        );
    }
}