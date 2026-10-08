using Unity.Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance;

    [Header("Camera")]
    public CinemachineCamera playerFollowCamera;

    private GameObject currentPlayer;


    // =====================================================
    // AWAKE
    // =====================================================

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


    // =====================================================
    // REGISTRA O PLAYER LOCAL CRIADO PELO FISHNET
    // =====================================================

    public void RegisterLocalPlayer(
        GameObject player
    )
    {
        if (player == null)
        {
            Debug.LogError(
                "[PlayerSpawner] Player local recebido é null."
            );

            return;
        }


        currentPlayer = player;


        if (
            PlayerSession.Instance != null &&
            PlayerSession.Instance.characterId > 0
        )
        {
            currentPlayer.name =
                "Player_"
                + PlayerSession.Instance.characterId;
        }


        Debug.Log(
            "[PlayerSpawner] Player local registrado pelo FishNet: "
            + currentPlayer.name
        );


        SetupLocalPlayer(
            currentPlayer
        );
    }


    // =====================================================
    // CAMERA LOCAL
    // =====================================================

    private void SetupLocalPlayer(
        GameObject player
    )
    {
        if (player == null)
        {
            return;
        }


        Transform cameraTarget =
            player.transform.Find(
                "PlayerCameraRoot"
            );


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
                "[PlayerSpawner] PlayerCameraRoot não encontrado em "
                + player.name
            );

            return;
        }


        if (playerFollowCamera == null)
        {
            Debug.LogError(
                "[PlayerSpawner] PlayerFollowCamera não configurada."
            );

            return;
        }


        playerFollowCamera.Target.TrackingTarget =
            cameraTarget;


        playerFollowCamera.PreviousStateIsValid =
            false;


        Debug.Log(
            "[PlayerSpawner] CAMERA CONFIGURADA NO PLAYER FISHNET."
            + "\nPlayer: "
            + player.name
            + "\nCameraTarget: "
            + cameraTarget.name
        );
    }


    // =====================================================
    // BUSCA FILHO
    // =====================================================

    private Transform FindChildRecursive(
        Transform parent,
        string childName
    )
    {
        foreach (
            Transform child
            in parent
        )
        {
            if (
                child.name ==
                childName
            )
            {
                return child;
            }


            Transform result =
                FindChildRecursive(
                    child,
                    childName
                );


            if (
                result != null
            )
            {
                return result;
            }
        }


        return null;
    }


    // =====================================================
    // RETORNA PLAYER LOCAL
    // =====================================================

    public GameObject GetCurrentPlayer()
    {
        return currentPlayer;
    }


    // =====================================================
    // REMOVE REFERÊNCIA LOCAL
    // =====================================================

    public void ClearLocalPlayer(
        GameObject player
    )
    {
        if (
            currentPlayer != player
        )
        {
            return;
        }


        if (
            playerFollowCamera != null
        )
        {
            playerFollowCamera
                .Target
                .TrackingTarget =
                null;


            playerFollowCamera
                .PreviousStateIsValid =
                false;
        }


        currentPlayer = null;


        Debug.Log(
            "[PlayerSpawner] Referência do player local removida."
        );
    }
}