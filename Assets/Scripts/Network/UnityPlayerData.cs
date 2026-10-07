using System;


[Serializable]
public class UnityPlayerData
{
    public bool success;

    public ServerPlayerData player;
}


[Serializable]
public class ServerPlayerData
{
    public int characterId;

    public string name;

    public int level;

    public int experience;

    public int money;

    public ServerPlayerStats stats;
}


[Serializable]
public class ServerPlayerStats
{
    public int currentHealth;

    public int maxHealth;

    public int energy;

    public int maxEnergy;

    public int strength;

    public int agility;

    public int intelligence;

    public int accuracy;

    public int charisma;

    public int vitality;
}