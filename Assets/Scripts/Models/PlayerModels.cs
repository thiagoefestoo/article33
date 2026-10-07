using System;
using System.Collections.Generic;


// =====================================
// RESPOSTA DA API
// =====================================

[Serializable]
public class PlayerResponse
{
    public bool success;

    public PlayerData player;
}



// =====================================
// DADOS DO JOGADOR
// =====================================

[Serializable]
public class PlayerData
{

    // =================================
    // IDENTIDADE
    // =================================

    public int characterId;

    public string name;



    // =================================
    // PROGRESSÃO
    // =================================

    public int level;

    public int experience;

    public int attributePoints;



    // =================================
    // ECONOMIA
    // =================================

    public int money;

    public int reputation;



    // =================================
    // STATUS
    // =================================

    public PlayerStats stats;



    // =================================
    // INVENTÁRIO
    // =================================

    public List<PlayerItem> inventory =
        new List<PlayerItem>();



    // =================================
    // EQUIPAMENTOS
    // =================================

    public List<PlayerEquipment> equipment =
        new List<PlayerEquipment>();

}



// =====================================
// ITEM DO INVENTÁRIO
// =====================================

[Serializable]
public class PlayerItem
{

    public int id;

    public string name;

    public string type;

    public string rarity;

}



// =====================================
// EQUIPAMENTO
// =====================================

[Serializable]
public class PlayerEquipment
{

    public int itemId;

    public string name;

    public string type;

    public string rarity;



    // =================================
    // COMBATE
    // =================================

    public int attack;

    public int defense;



    // =================================
    // BÔNUS
    // =================================

    public int strengthBonus;

    public int intelligenceBonus;

    public int agilityBonus;

    public int healthBonus;

    public int energyBonus;

}