using System;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{

    public static PlayerManager Instance;


    public PlayerData CurrentPlayer { get; private set; }


    public bool IsPlayerLoaded { get; private set; }



    public event Action<PlayerData> OnPlayerLoaded;

    public event Action<PlayerData> OnPlayerStateChanged;



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
    // CARREGAR PLAYER
    // =====================================================

    public void SetPlayer(PlayerData player)
    {

        if (player == null)
        {
            Debug.LogError(
                "[PlayerManager] Player vazio."
            );

            return;
        }



        CurrentPlayer = player;

        IsPlayerLoaded = true;



        if (CurrentPlayer.stats == null)
        {
            Debug.LogError(
                "[PlayerManager] Stats inexistente."
            );

            return;
        }



        Debug.Log("==============================");

        Debug.Log("[PlayerManager] PLAYER CARREGADO");

        Debug.Log(
            "Nome: " + CurrentPlayer.name
        );


        Debug.Log(
            "Level: " + CurrentPlayer.level
        );


        Debug.Log(
            "XP: " + CurrentPlayer.experience
        );


        Debug.Log(
            "Dinheiro: " + CurrentPlayer.money
        );



        Debug.Log(
            "HP: "
            + CurrentPlayer.stats.currentHealth
            + "/"
            + CurrentPlayer.stats.maxHealth
        );



        Debug.Log(
            "Energia: "
            + CurrentPlayer.stats.energy
            + "/"
            + CurrentPlayer.stats.maxEnergy
        );



        Debug.Log("==============================");



        OnPlayerLoaded?.Invoke(
            CurrentPlayer
        );


        OnPlayerStateChanged?.Invoke(
            CurrentPlayer
        );

    }




    public bool HasPlayer()
    {

        return
            IsPlayerLoaded &&
            CurrentPlayer != null;

    }



    // =====================================================
    // COMBATE
    // =====================================================


    public void TakeDamage(int damage)
    {

        if (!HasPlayer())
            return;


        CurrentPlayer.stats.TakeDamage(
            damage
        );


        NotifyStateChanged();

    }




    public void HealPlayer(int amount)
    {

        if (!HasPlayer())
            return;


        CurrentPlayer.stats.Heal(
            amount
        );


        NotifyStateChanged();

    }




    public bool IsPlayerAlive()
    {

        if (!HasPlayer())
            return false;


        return CurrentPlayer.stats.IsAlive();

    }




    public bool IsPlayerDead()
    {

        if (!HasPlayer())
            return false;


        return CurrentPlayer.stats.IsDead();

    }




    // =====================================================
    // ENERGIA
    // =====================================================


    public bool ConsumeEnergy(int amount)
    {

        if (!HasPlayer())
            return false;



        bool result =
            CurrentPlayer.stats.ConsumeEnergy(
                amount
            );


        if (result)
            NotifyStateChanged();



        return result;

    }




    public void RestoreEnergy(int amount)
    {

        if (!HasPlayer())
            return;



        CurrentPlayer.stats.RestoreEnergy(
            amount
        );


        NotifyStateChanged();

    }




    public void FullRestore()
    {

        if (!HasPlayer())
            return;



        CurrentPlayer.stats.FullRestore();


        NotifyStateChanged();

    }




    // =====================================================
    // ACESSO RÁPIDO
    // =====================================================


    public string GetPlayerName()
    {

        if (!HasPlayer())
            return "";


        return CurrentPlayer.name;

    }



    public int GetLevel()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.level;

    }



    public int GetExperience()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.experience;

    }



    public int GetMoney()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.money;

    }




    public int GetCurrentHealth()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.stats.currentHealth;

    }




    public int GetMaxHealth()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.stats.maxHealth;

    }




    public int GetEnergy()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.stats.energy;

    }




    public int GetMaxEnergy()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.stats.maxEnergy;

    }




    public int GetStrength()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.stats.strength;

    }



    public int GetAgility()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.stats.agility;

    }



    public int GetIntelligence()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.stats.intelligence;

    }



    public int GetAccuracy()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.stats.accuracy;

    }



    public int GetCharisma()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.stats.charisma;

    }



    public int GetVitality()
    {

        if (!HasPlayer())
            return 0;


        return CurrentPlayer.stats.vitality;

    }




    private void NotifyStateChanged()
    {

        if (!HasPlayer())
            return;


        OnPlayerStateChanged?.Invoke(
            CurrentPlayer
        );

    }

}