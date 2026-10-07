using System.ComponentModel.DataAnnotations;


namespace Artigo33.API.Models;



public class CharacterEquipment

{

    [Key]

    public int Id { get; set; }






    // =====================================
    // PERSONAGEM
    // =====================================


    public int CharacterId { get; set; }


    public Character? Character { get; set; }







    // =====================================
    // ITEM EQUIPADO
    // =====================================


    public int ItemId { get; set; }


    public Item? Item { get; set; }







    // =====================================
    // SLOT DO EQUIPAMENTO
    // =====================================


    /*
     
     Weapon
     Armor
     Helmet
     Accessory
     Special

    */


    public string Slot { get; set; } = "Weapon";







    // =====================================
    // STATUS
    // =====================================


    public bool Equipped { get; set; } = true;







    // =====================================
    // DATA
    // =====================================


    public DateTime EquippedAt { get; set; }

        = DateTime.UtcNow;



}