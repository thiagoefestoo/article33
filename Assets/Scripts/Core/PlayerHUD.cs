using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [Header("Textos do HUD")]

    public TMP_Text nameText;
    public TMP_Text levelText;
    public TMP_Text healthText;
    public TMP_Text energyText;
    public TMP_Text moneyText;
    public TMP_Text xpText;


    [Header("Atributos RPG")]

    public TMP_Text strengthText;
    public TMP_Text agilityText;
    public TMP_Text intelligenceText;
    public TMP_Text accuracyText;
    public TMP_Text charismaText;
    public TMP_Text vitalityText;


    // =========================================================
    // CONTROLE DE ESTADO
    // =========================================================

    private int ultimoHealth = -1;
    private int ultimoMaxHealth = -1;
    private int ultimaEnergy = -1;
    private int ultimoMaxEnergy = -1;

    private int ultimoLevel = -1;
    private int ultimoXP = -1;
    private int ultimoMoney = -1;


    // =========================================================
    // START
    // =========================================================

    private void Start()
    {
        StartCoroutine(AguardarPlayer());
    }


    // =========================================================
    // AGUARDAR PLAYER
    // =========================================================

    private IEnumerator AguardarPlayer()
    {
        Debug.Log(
            "[PlayerHUD] Aguardando PlayerManager..."
        );


        // -----------------------------------------------------
        // AGUARDAR PLAYER MANAGER
        // -----------------------------------------------------

        while (PlayerManager.Instance == null)
        {
            yield return null;
        }


        Debug.Log(
            "[PlayerHUD] PlayerManager encontrado."
        );


        // -----------------------------------------------------
        // AGUARDAR PLAYER SER CARREGADO
        // -----------------------------------------------------

        while (!PlayerManager.Instance.HasPlayer())
        {
            yield return null;
        }


        Debug.Log(
            "[PlayerHUD] Jogador encontrado!"
        );


        // -----------------------------------------------------
        // PRIMEIRA ATUALIZAÇÃO
        // -----------------------------------------------------

        UpdateHUD();


        Debug.Log(
            "[PlayerHUD] HUD inicializado."
        );
    }


    // =========================================================
    // UPDATE
    // =========================================================

    private void Update()
    {
        if (PlayerManager.Instance == null)
            return;


        if (!PlayerManager.Instance.HasPlayer())
            return;


        PlayerData player =
            PlayerManager.Instance.CurrentPlayer;


        if (player == null)
            return;


        if (player.stats == null)
            return;


        // =====================================================
        // DETECTAR ALTERAÇÃO DE ESTADO
        // =====================================================

        bool estadoAlterado = false;


        // -----------------------------------------------------
        // VIDA
        // -----------------------------------------------------

        if (
            ultimoHealth != player.stats.currentHealth ||
            ultimoMaxHealth != player.stats.maxHealth
        )
        {
            estadoAlterado = true;
        }


        // -----------------------------------------------------
        // ENERGIA
        // -----------------------------------------------------

        if (
            ultimaEnergy != player.stats.energy ||
            ultimoMaxEnergy != player.stats.maxEnergy
        )
        {
            estadoAlterado = true;
        }


        // -----------------------------------------------------
        // PROGRESSÃO
        // -----------------------------------------------------

        if (
            ultimoLevel != player.level ||
            ultimoXP != player.experience
        )
        {
            estadoAlterado = true;
        }


        // -----------------------------------------------------
        // DINHEIRO
        // -----------------------------------------------------

        if (ultimoMoney != player.money)
        {
            estadoAlterado = true;
        }


        // -----------------------------------------------------
        // ATUALIZAR HUD
        // -----------------------------------------------------

        if (estadoAlterado)
        {
            UpdateHUD();
        }
    }


    // =========================================================
    // ATUALIZAR HUD
    // =========================================================

    public void UpdateHUD()
    {
        // -----------------------------------------------------
        // PLAYER MANAGER
        // -----------------------------------------------------

        if (PlayerManager.Instance == null)
        {
            Debug.LogWarning(
                "[PlayerHUD] PlayerManager não encontrado."
            );

            return;
        }


        // -----------------------------------------------------
        // PLAYER
        // -----------------------------------------------------

        if (!PlayerManager.Instance.HasPlayer())
        {
            Debug.LogWarning(
                "[PlayerHUD] Nenhum jogador carregado."
            );

            return;
        }


        // -----------------------------------------------------
        // PLAYER DATA
        // -----------------------------------------------------

        PlayerData player =
            PlayerManager.Instance.CurrentPlayer;


        if (player == null)
        {
            Debug.LogError(
                "[PlayerHUD] CurrentPlayer está NULL."
            );

            return;
        }


        // -----------------------------------------------------
        // STATS
        // -----------------------------------------------------

        if (player.stats == null)
        {
            Debug.LogError(
                "[PlayerHUD] Player.stats está NULL."
            );

            return;
        }


        // =====================================================
        // ATUALIZAR CACHE
        // =====================================================

        ultimoHealth =
            player.stats.currentHealth;

        ultimoMaxHealth =
            player.stats.maxHealth;

        ultimaEnergy =
            player.stats.energy;

        ultimoMaxEnergy =
            player.stats.maxEnergy;

        ultimoLevel =
            player.level;

        ultimoXP =
            player.experience;

        ultimoMoney =
            player.money;


        // =====================================================
        // IDENTIDADE
        // =====================================================

        if (nameText != null)
        {
            nameText.text =
                player.name;
        }


        // =====================================================
        // LEVEL
        // =====================================================

        if (levelText != null)
        {
            levelText.text =
                "Nível: "
                + player.level;
        }


        // =====================================================
        // XP
        // =====================================================

        if (xpText != null)
        {
            xpText.text =
                "XP: "
                + player.experience;
        }


        // =====================================================
        // DINHEIRO
        // =====================================================

        if (moneyText != null)
        {
            moneyText.text =
                "Dinheiro: "
                + player.money;
        }


        // =====================================================
        // VIDA
        // =====================================================

        if (healthText != null)
        {
            healthText.text =
                "HP: "
                + player.stats.currentHealth
                + " / "
                + player.stats.maxHealth;
        }


        // =====================================================
        // ENERGIA
        // =====================================================

        if (energyText != null)
        {
            energyText.text =
                "Energia: "
                + player.stats.energy
                + " / "
                + player.stats.maxEnergy;
        }


        // =====================================================
        // FORÇA
        // =====================================================

        if (strengthText != null)
        {
            strengthText.text =
                "Força: "
                + player.stats.strength;
        }


        // =====================================================
        // AGILIDADE
        // =====================================================

        if (agilityText != null)
        {
            agilityText.text =
                "Agilidade: "
                + player.stats.agility;
        }


        // =====================================================
        // INTELIGÊNCIA
        // =====================================================

        if (intelligenceText != null)
        {
            intelligenceText.text =
                "Inteligência: "
                + player.stats.intelligence;
        }


        // =====================================================
        // PRECISÃO
        // =====================================================

        if (accuracyText != null)
        {
            accuracyText.text =
                "Precisão: "
                + player.stats.accuracy;
        }


        // =====================================================
        // CARISMA
        // =====================================================

        if (charismaText != null)
        {
            charismaText.text =
                "Carisma: "
                + player.stats.charisma;
        }


        // =====================================================
        // VITALIDADE
        // =====================================================

        if (vitalityText != null)
        {
            vitalityText.text =
                "Vitalidade: "
                + player.stats.vitality;
        }


        // =====================================================
        // DEBUG
        // =====================================================

        Debug.Log(
            "[PlayerHUD] HUD atualizado | "
            + "HP: "
            + player.stats.currentHealth
            + "/"
            + player.stats.maxHealth
            + " | Energia: "
            + player.stats.energy
            + "/"
            + player.stats.maxEnergy
        );
    }
}