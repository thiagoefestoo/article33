using System;


[Serializable]
public class PlayerStats
{


    // =========================================================
    // EVENTO DE ALTERAÇÃO
    // =========================================================

    [NonSerialized]
    private Action onStatsChanged;


    public event Action OnStatsChanged
    {
        add
        {
            onStatsChanged += value;
        }

        remove
        {
            onStatsChanged -= value;
        }
    }



    private void NotifyChange()
    {

        onStatsChanged?.Invoke();

    }




    // =========================================================
    // VIDA
    // =========================================================

    public int currentHealth = 0;

    public int maxHealth = 100;



    // =========================================================
    // ENERGIA
    // =========================================================

    public int energy = 100;

    public int maxEnergy = 100;



    // =========================================================
    // ATRIBUTOS RPG
    // =========================================================

    public int strength = 0;

    public int agility = 0;

    public int intelligence = 0;

    public int accuracy = 0;

    public int charisma = 0;

    public int vitality = 0;



    // =========================================================
    // DANO
    // =========================================================

    public void TakeDamage(int damage)
    {

        if (damage <= 0)
            return;



        currentHealth -= damage;



        if (currentHealth < 0)
            currentHealth = 0;



        NotifyChange();

    }





    // =========================================================
    // CURA
    // =========================================================

    public void Heal(int amount)
    {

        if (amount <= 0)
            return;



        currentHealth += amount;



        if (currentHealth > maxHealth)
            currentHealth = maxHealth;



        NotifyChange();

    }





    // =========================================================
    // ENERGIA
    // =========================================================

    public bool ConsumeEnergy(int amount)
    {

        if (amount <= 0)
            return true;



        if (energy < amount)
            return false;



        energy -= amount;



        NotifyChange();



        return true;

    }





    public void RestoreEnergy(int amount)
    {

        if (amount <= 0)
            return;



        energy += amount;



        if (energy > maxEnergy)
            energy = maxEnergy;



        NotifyChange();

    }





    // =========================================================
    // RESTAURAR COMPLETAMENTE
    // =========================================================

    public void FullRestore()
    {

        currentHealth = maxHealth;

        energy = maxEnergy;



        NotifyChange();

    }





    // =========================================================
    // ESTADO
    // =========================================================

    public bool IsDead()
    {

        return currentHealth <= 0;

    }





    public bool IsAlive()
    {

        return currentHealth > 0;

    }





    // =========================================================
    // PERCENTUAIS
    // =========================================================

    public float GetHealthPercent()
    {

        if (maxHealth <= 0)
            return 0f;



        return (float)currentHealth / maxHealth;

    }





    public float GetEnergyPercent()
    {

        if (maxEnergy <= 0)
            return 0f;



        return (float)energy / maxEnergy;

    }





    // =========================================================
    // AUMENTAR ATRIBUTOS
    // =========================================================

    public void AddStrength(int value)
    {

        strength += value;

        NotifyChange();

    }





    public void AddAgility(int value)
    {

        agility += value;

        NotifyChange();

    }





    public void AddIntelligence(int value)
    {

        intelligence += value;

        NotifyChange();

    }





    public void AddAccuracy(int value)
    {

        accuracy += value;

        NotifyChange();

    }





    public void AddCharisma(int value)
    {

        charisma += value;

        NotifyChange();

    }





    public void AddVitality(int value)
    {

        vitality += value;

        NotifyChange();

    }





    // =========================================================
    // DEBUG
    // =========================================================

    public void PrintStats()
    {

        UnityEngine.Debug.Log(
            "========== PLAYER STATUS ==========\n" +

            "HP: "
            + currentHealth
            + "/"
            + maxHealth

            +

            "\nEnergia: "
            + energy
            + "/"
            + maxEnergy

            +

            "\nForça: "
            + strength

            +

            "\nAgilidade: "
            + agility

            +

            "\nInteligência: "
            + intelligence

            +

            "\nPrecisão: "
            + accuracy

            +

            "\nCarisma: "
            + charisma

            +

            "\nVitalidade: "
            + vitality

            +

            "\n===================================="
        );

    }



}