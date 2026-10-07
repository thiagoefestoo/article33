namespace Artigo33.API.Models;


public class PlayerProfile
{

    // IDENTIDADE

    public int CharacterId { get; set; }

    public string Name { get; set; } = "";

    public string Gender { get; set; } = "";



    // CLASSE

    public string CharacterClass { get; set; } = "";

    public string Rank { get; set; } = "";

    public string Faction { get; set; } = "";

    public string SubClass { get; set; } = "";



    // PROGRESSÃO

    public int Level { get; set; }

    public int Experience { get; set; }

    public int AttributePoints { get; set; }



    // STATUS

    public int Health { get; set; }

    public int Energy { get; set; }



    // ECONOMIA

    public int Money { get; set; }

    public int Reputation { get; set; }



    // ATRIBUTOS

    public int Strength { get; set; }

    public int Agility { get; set; }

    public int Intelligence { get; set; }

    public int Vitality { get; set; }

    public int Accuracy { get; set; }

    public int Charisma { get; set; }



    // ESTATÍSTICAS

    public int MissionsCompleted { get; set; }

    public int Arrests { get; set; }

    public int CrimesCommitted { get; set; }



    // EQUIPAMENTOS

    public List<PlayerEquipmentProfile> Equipment { get; set; }
        = new();

}



public class PlayerEquipmentProfile
{

    public int ItemId { get; set; }


    public string Name { get; set; } = "";


    public string Type { get; set; } = "";


    public string Rarity { get; set; } = "";


    public int Attack { get; set; }


    public int Defense { get; set; }


    public int StrengthBonus { get; set; }


    public int IntelligenceBonus { get; set; }


    public int AgilityBonus { get; set; }


    public int HealthBonus { get; set; }


    public int EnergyBonus { get; set; }

}