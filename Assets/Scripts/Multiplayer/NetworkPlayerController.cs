using FishNet.Object;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetworkPlayerController : NetworkBehaviour
{
    private ThirdPersonController thirdPersonController;

    private StarterAssetsInputs starterAssetsInputs;

    private PlayerInput playerInput;


    // =====================================================
    // AWAKE
    // =====================================================

    private void Awake()
    {
        thirdPersonController =
            GetComponent<ThirdPersonController>();


        starterAssetsInputs =
            GetComponent<StarterAssetsInputs>();


        playerInput =
            GetComponent<PlayerInput>();
    }


    // =====================================================
    // CLIENT START
    // =====================================================

    public override void OnStartClient()
    {
        base.OnStartClient();


        if (IsOwner)
        {
            EnableLocalPlayer();
        }
        else
        {
            DisableRemotePlayer();
        }
    }


    // =====================================================
    // LOCAL PLAYER
    // =====================================================

    private void EnableLocalPlayer()
    {
        if (
            PlayerSession.Instance != null &&
            PlayerSession.Instance.characterId > 0
        )
        {
            gameObject.name =
                "Player_"
                + PlayerSession.Instance.characterId;
        }


        if (
            thirdPersonController != null
        )
        {
            thirdPersonController.enabled =
                true;
        }


        if (
            starterAssetsInputs != null
        )
        {
            starterAssetsInputs.enabled =
                true;
        }


        if (
            playerInput != null
        )
        {
            playerInput.enabled =
                true;
        }


        Cursor.lockState =
            CursorLockMode.Locked;


        Cursor.visible =
            false;


        if (
            PlayerSpawner.Instance != null
        )
        {
            PlayerSpawner.Instance
                .RegisterLocalPlayer(
                    gameObject
                );
        }
        else
        {
            Debug.LogError(
                "[NETWORK PLAYER] PlayerSpawner.Instance não encontrado."
            );
        }


        Debug.Log(
            "[NETWORK PLAYER] PLAYER LOCAL HABILITADO."
            + "\nGameObject: "
            + gameObject.name
            + "\nOwner: "
            + IsOwner
        );
    }


    // =====================================================
    // REMOTE PLAYER
    // =====================================================

    private void DisableRemotePlayer()
    {
        if (
            thirdPersonController != null
        )
        {
            thirdPersonController.enabled =
                false;
        }


        if (
            starterAssetsInputs != null
        )
        {
            starterAssetsInputs.enabled =
                false;
        }


        if (
            playerInput != null
        )
        {
            playerInput.enabled =
                false;
        }


        Debug.Log(
            "[NETWORK PLAYER] PLAYER REMOTO."
            + "\nInput desativado em: "
            + gameObject.name
        );
    }


    // =====================================================
    // STOP CLIENT
    // =====================================================

    public override void OnStopClient()
    {
        if (
            IsOwner &&
            PlayerSpawner.Instance != null
        )
        {
            PlayerSpawner.Instance
                .ClearLocalPlayer(
                    gameObject
                );
        }


        base.OnStopClient();
    }
}