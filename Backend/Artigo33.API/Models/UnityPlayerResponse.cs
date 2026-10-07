namespace Artigo33.API.Models;


public class UnityPlayerResponse
{
    public int CharacterId { get; set; }


    public string Name { get; set; } = "";


    public int Level { get; set; }


    public int Experience { get; set; }


    public int Money { get; set; }


    public int Reputation { get; set; }


    // =========================================================
    // STATUS
    // =========================================================

    public PlayerStatsResponse Stats { get; set; }
        = new();


    // =========================================================
    // INVENTÁRIO
    // =========================================================

    public List<UnityInventoryResponse> Inventory { get; set; }
        = new();


    // =========================================================
    // EQUIPAMENTOS
    // =========================================================

    public List<UnityEquipmentResponse> Equipment { get; set; }
        = new();
}



// =============================================================
// STATUS / ATRIBUTOS RPG
// =============================================================

public class PlayerStatsResponse
{
    public int CurrentHealth { get; set; }

    public int MaxHealth { get; set; }

    public int Energy { get; set; }

    public int MaxEnergy { get; set; }

    public int Strength { get; set; }

    public int Agility { get; set; }

    public int Intelligence { get; set; }

    public int Accuracy { get; set; }

    public int Charisma { get; set; }

    public int Vitality { get; set; }
}


// =============================================================
// INVENTÁRIO
// =============================================================

public class UnityInventoryResponse
{
    public int ItemId { get; set; }


    public string Name { get; set; } = "";


    public string Type { get; set; } = "";


    public string Rarity { get; set; } = "";
}



// =============================================================
// EQUIPAMENTO
// =============================================================

public class UnityEquipmentResponse
{
    public int ItemId { get; set; }


    public string Slot { get; set; } = "";


    public string Name { get; set; } = "";
}