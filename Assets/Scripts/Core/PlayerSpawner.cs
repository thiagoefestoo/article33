using UnityEngine;
using Unity.Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance;

    [Header("Player Prefab")]
    public GameObject playerPrefab;

    [Header("Spawn Point")]
    public Transform spawnPoint;

    [Header("Camera")]
    public CinemachineCamera playerFollowCamera;

    private GameObject currentPlayer;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public GameObject SpawnPlayer(PlayerData playerData)
    {
        // =========================
        // VALIDA PLAYER DATA
        // =========================

        if (playerData == null)
        {
            Debug.LogError(
                "[PlayerSpawner] PlayerData está vazio."
            );

            return null;
        }


        // =========================
        // VALIDA PREFAB
        // =========================

        if (playerPrefab == null)
        {
            Debug.LogError(
                "[PlayerSpawner] Player Prefab não está configurado."
            );

            return null;
        }


        // =========================
        // REMOVE PLAYER ANTERIOR
        // =========================

        if (currentPlayer != null)
        {
            Debug.LogWarning(
                "[PlayerSpawner] Jogador anterior encontrado. Removendo..."
            );

            Destroy(currentPlayer);

            currentPlayer = null;
        }


        // =========================
        // DEFINE POSIÇÃO DE SPAWN
        // =========================

        Vector3 position;

        Quaternion rotation;


        if (spawnPoint != null)
        {
            position = spawnPoint.position;
            rotation = spawnPoint.rotation;
        }
        else
        {
            Debug.LogWarning(
                "[PlayerSpawner] Spawn Point não configurado. " +
                "Usando Vector3.zero."
            );

            position = Vector3.zero;
            rotation = Quaternion.identity;
        }


        // =========================
        // INSTANCIA PLAYER
        // =========================

        currentPlayer = Instantiate(
            playerPrefab,
            position,
            rotation
        );


        // =========================
        // DEFINE NOME
        // =========================

        currentPlayer.name =
            "Player_" + playerData.characterId;


        Debug.Log(
            "[PlayerSpawner] Player instanciado: "
            + currentPlayer.name
        );


        // =========================
        // CONFIGURA PLAYER LOCAL
        // =========================

        SetupLocalPlayer(currentPlayer);


        Debug.Log(
            "[PlayerSpawner] Personagem criado com sucesso."
            + "\nNome: " + playerData.name
            + "\nCharacterId: " + playerData.characterId
            + "\nGameObject: " + currentPlayer.name
        );


        return currentPlayer;
    }


    private void SetupLocalPlayer(GameObject player)
    {
        // =========================
        // VALIDA PLAYER
        // =========================

        if (player == null)
        {
            Debug.LogError(
                "[PlayerSpawner] SetupLocalPlayer recebeu Player null."
            );

            return;
        }


        // =========================
        // PROCURA PLAYER CAMERA ROOT
        // =========================

        Transform cameraTarget =
            player.transform.Find("PlayerCameraRoot");


        // Caso futuramente o PlayerCameraRoot
        // esteja mais fundo na hierarquia
        if (cameraTarget == null)
        {
            cameraTarget =
                FindChildRecursive(
                    player.transform,
                    "PlayerCameraRoot"
                );
        }


        if (cameraTarget == null)
        {
            Debug.LogError(
                "[PlayerSpawner] PlayerCameraRoot não foi encontrado "
                + "dentro de "
                + player.name
            );

            return;
        }


        // =========================
        // VALIDA CINEMACHINE CAMERA
        // =========================

        if (playerFollowCamera == null)
        {
            Debug.LogError(
                "[PlayerSpawner] PlayerFollowCamera não está configurada "
                + "no Inspector."
            );

            return;
        }


        // =========================
        // CONECTA NOVO PLAYER
        // À CINEMACHINE
        // =========================

        playerFollowCamera.Target.TrackingTarget =
            cameraTarget;


        // Força a Cinemachine a recalcular
        // imediatamente sua posição
        playerFollowCamera.PreviousStateIsValid = false;


        Debug.Log(
            "[PlayerSpawner] CAMERA CONFIGURADA COM SUCESSO"
            + "\nPlayer: " + player.name
            + "\nCamera Target: " + cameraTarget.name
            + "\nCamera Target Parent: "
            + (
                cameraTarget.parent != null
                    ? cameraTarget.parent.name
                    : "SEM PARENT"
            )
            + "\nTracking Target atual: "
            + (
                playerFollowCamera.Target.TrackingTarget != null
                    ? playerFollowCamera.Target.TrackingTarget.name
                    : "NULL"
            )
        );
    }


    // =====================================================
    // PROCURA UM FILHO PELO NOME EM TODA A HIERARQUIA
    // =====================================================

    private Transform FindChildRecursive(
        Transform parent,
        string childName
    )
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }


            Transform result =
                FindChildRecursive(
                    child,
                    childName
                );


            if (result != null)
            {
                return result;
            }
        }


        return null;
    }


    // =====================================================
    // RETORNA PLAYER ATUAL
    // =====================================================

    public GameObject GetCurrentPlayer()
    {
        return currentPlayer;
    }


    // =====================================================
    // DESPAWN
    // =====================================================

    public void DespawnPlayer()
    {
        if (currentPlayer == null)
        {
            Debug.LogWarning(
                "[PlayerSpawner] Nenhum jogador para remover."
            );

            return;
        }


        // Remove referência da câmera primeiro
        if (playerFollowCamera != null)
        {
            playerFollowCamera.Target.TrackingTarget =
                null;

            playerFollowCamera.PreviousStateIsValid =
                false;
        }


        Debug.Log(
            "[PlayerSpawner] Removendo personagem: "
            + currentPlayer.name
        );


        Destroy(currentPlayer);

        currentPlayer = null;


        Debug.Log(
            "[PlayerSpawner] Personagem removido."
        );
    }
}