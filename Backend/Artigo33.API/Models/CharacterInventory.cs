using System.ComponentModel.DataAnnotations;


namespace Artigo33.API.Models;


public class CharacterInventory
{


    [Key]

    public int Id { get; set; }





    // ==========================
    // PERSONAGEM
    // ==========================


    public int CharacterId { get; set; }


    public Character? Character { get; set; }







    // ==========================
    // ITEM
    // ==========================


    public int ItemId { get; set; }


    public Item? Item { get; set; }







    // ==========================
    // QUANTIDADE
    // ==========================


    public int Quantity { get; set; }
        = 1;





    // Equipado?

    public bool Equipped { get; set; }
        = false;





    public DateTime AddedAt { get; set; }

        = DateTime.UtcNow;



}