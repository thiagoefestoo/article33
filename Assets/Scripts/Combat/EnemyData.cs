using System;


[Serializable]
public class EnemyData
{


    // =========================================================
    // IDENTIDADE
    // =========================================================

    public int Id;

    public string Name;



    // =========================================================
    // PROGRESSÃO
    // =========================================================

    public int Level;



    // =========================================================
    // STATUS
    // =========================================================

    public int Health = 100;

    public int MaxHealth = 100;


    public int Attack = 10;

    public int Defense = 0;



    // =========================================================
    // RECOMPENSAS
    // =========================================================

    public int ExperienceReward;

    public int MoneyReward;



    // =========================================================
    // ESTADO
    // =========================================================

    public bool IsDead()
    {

        return Health <= 0;

    }



    public bool IsAlive()
    {

        return Health > 0;

    }



    // =========================================================
    // RECEBER DANO
    // =========================================================

    public void TakeDamage(int damage)
    {


        if (damage <= 0)
            return;



        int finalDamage =
            damage - Defense;



        if (finalDamage < 1)
            finalDamage = 1;



        Health -= finalDamage;



        if (Health < 0)
            Health = 0;


    }



    // =========================================================
    // ATAQUE DO INIMIGO
    // =========================================================

    public int CalculateAttackDamage()
    {


        int damage =
            Attack;



        if (damage < 1)
            damage = 1;



        return damage;


    }



    // =========================================================
    // CURA
    // =========================================================

    public void Heal(int amount)
    {


        if (amount <= 0)
            return;



        Health += amount;



        if (Health > MaxHealth)
            Health = MaxHealth;


    }



    // =========================================================
    // RESTAURAR INIMIGO
    // =========================================================

    public void Restore()
    {


        Health = MaxHealth;


    }



    // =========================================================
    // PORCENTAGEM DE VIDA
    // =========================================================

    public float GetHealthPercent()
    {


        if (MaxHealth <= 0)
            return 0f;



        return (float)Health / MaxHealth;


    }



    // =========================================================
    // DEBUG
    // =========================================================

    public void PrintStats()
    {


        UnityEngine.Debug.Log(

            "========== ENEMY STATUS ==========\n"

            +

            "Nome: "
            +
            Name

            +

            "\nLevel: "
            +
            Level

            +

            "\nHP: "
            +
            Health
            +
            "/"
            +
            MaxHealth

            +

            "\nAtaque: "
            +
            Attack

            +

            "\nDefesa: "
            +
            Defense

            +

            "\nXP: "
            +
            ExperienceReward

            +

            "\nDinheiro: "
            +
            MoneyReward

            +

            "\n=================================="

        );


    }


}