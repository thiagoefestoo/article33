using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    private PlayerData currentPlayer;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }


    // =========================================================
    // RECEBER DADOS DO JOGADOR
    // =========================================================

    public void SetPlayer(PlayerData data)
    {
        if (data == null)
        {
            Debug.LogError(
                "[PlayerDataManager] PlayerData recebido é null."
            );

            return;
        }

        currentPlayer = data;

        Debug.Log(
            "[PlayerDataManager] Jogador carregado: "
            + currentPlayer.name
        );

        if (currentPlayer.stats != null)
        {
            currentPlayer.stats.PrintStats();
        }
    }


    // =========================================================
    // RETORNAR JOGADOR
    // =========================================================

    public PlayerData GetPlayer()
    {
        return currentPlayer;
    }


    // =========================================================
    // VERIFICAR SE EXISTE JOGADOR
    // =========================================================

    public bool HasPlayer()
    {
        return currentPlayer != null;
    }


    // =========================================================
    // RETORNAR STATUS
    // =========================================================

    public PlayerStats GetStats()
    {
        if (!HasPlayer())
        {
            return null;
        }

        return currentPlayer.stats;
    }


    // =========================================================
    // NOME
    // =========================================================

    public string GetPlayerName()
    {
        if (!HasPlayer())
        {
            return string.Empty;
        }

        return currentPlayer.name;
    }


    // =========================================================
    // LEVEL
    // =========================================================

    public int GetLevel()
    {
        if (!HasPlayer())
        {
            return 0;
        }

        return currentPlayer.level;
    }


    // =========================================================
    // EXPERIÊNCIA
    // =========================================================

    public int GetExperience()
    {
        if (!HasPlayer())
        {
            return 0;
        }

        return currentPlayer.experience;
    }


    // =========================================================
    // DINHEIRO
    // =========================================================

    public int GetMoney()
    {
        if (!HasPlayer())
        {
            return 0;
        }

        return currentPlayer.money;
    }


    // =========================================================
    // REPUTAÇÃO
    // =========================================================

    public int GetReputation()
    {
        if (!HasPlayer())
        {
            return 0;
        }

        return currentPlayer.reputation;
    }


    // =========================================================
    // ID DO PERSONAGEM
    // =========================================================

    public int GetCharacterId()
    {
        if (!HasPlayer())
        {
            return 0;
        }

        return currentPlayer.characterId;
    }


    // =========================================================
    // LIMPAR DADOS
    // =========================================================

    public void ClearPlayer()
    {
        currentPlayer = null;

        Debug.Log(
            "[PlayerDataManager] Dados do jogador removidos."
        );
    }
}