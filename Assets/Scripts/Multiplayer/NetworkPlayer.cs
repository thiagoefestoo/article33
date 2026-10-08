using FishNet.Object;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    [Header("Identidade")]
    [SerializeField]
    private int characterId;

    [SerializeField]
    private string playerName;

    public override void OnStartClient()
    {
        base.OnStartClient();

        Debug.Log(
            "[NETWORK PLAYER] Spawn recebido."
            + " ObjectId=" + ObjectId
            + " IsOwner=" + IsOwner
        );

        if (IsOwner)
        {
            LoadLocalIdentity();
        }
    }

    private void LoadLocalIdentity()
    {
        if (PlayerSession.Instance == null)
        {
            Debug.LogWarning(
                "[NETWORK PLAYER] PlayerSession não encontrada."
            );

            return;
        }

        characterId = PlayerSession.Instance.characterId;
        playerName = PlayerSession.Instance.playerName;

        gameObject.name =
            "NetworkPlayer_" + characterId;

        Debug.Log(
            "[NETWORK PLAYER] Jogador local identificado."
            + " CharacterId=" + characterId
            + " Nome=" + playerName
        );
    }
}