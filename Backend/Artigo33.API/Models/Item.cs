using System.ComponentModel.DataAnnotations;


namespace Artigo33.API.Models;


public class Item

{

    [Key]
    public int Id { get; set; }



    // =====================================
    // IDENTIDADE
    // =====================================

    [Required]
    public string Name { get; set; } = "";


    public string Description { get; set; } = "";





    // =====================================
    // TIPO DO ITEM
    // =====================================

    /*
     
     Weapon       = Arma
     Armor        = Armadura
     Helmet       = Capacete
     Consumable   = Consumível
     Quest        = Missão
     Document     = Documento
     Material     = Material
     Equipment    = Equipamento

    */


    public string Type { get; set; } = "Misc";





    // =====================================
    // RARIDADE
    // =====================================

    /*
     
     Common
     Uncommon
     Rare
     Epic
     Legendary

    */


    public string Rarity { get; set; } = "Common";





    // =====================================
    // ATRIBUTOS DE COMBATE
    // =====================================


    // Dano da arma

    public int Attack { get; set; } = 0;



    // Defesa da armadura

    public int Defense { get; set; } = 0;





    // =====================================
    // BÔNUS DE ATRIBUTOS
    // =====================================


    public int StrengthBonus { get; set; } = 0;


    public int AgilityBonus { get; set; } = 0;


    public int IntelligenceBonus { get; set; } = 0;


    public int VitalityBonus { get; set; } = 0;


    public int AccuracyBonus { get; set; } = 0;


    public int CharismaBonus { get; set; } = 0;


    public int HealthBonus { get; set; } = 0;


    public int EnergyBonus { get; set; } = 0;


    public int ReputationBonus { get; set; } = 0;





    // =====================================
    // EQUIPAMENTO
    // =====================================


    /*
     
     Weapon
     Armor
     Helmet
     Accessory

    */


    public string EquipmentSlot { get; set; } = "";



    public bool IsEquipment { get; set; } = false;





    // =====================================
    // ECONOMIA
    // =====================================


    public int BuyPrice { get; set; } = 0;


    public int SellPrice { get; set; } = 0;





    // =====================================
    // PESO
    // =====================================


    public double Weight { get; set; } = 0;





    // =====================================
    // ESTADO
    // =====================================


    public bool Active { get; set; } = true;



    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;


}