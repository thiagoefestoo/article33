using System.ComponentModel.DataAnnotations;


namespace Artigo33.API.Models;


public class ShopTransaction
{

    [Key]
    public int Id { get; set; }



    // Personagem

    public int CharacterId { get; set; }

    public Character? Character { get; set; }




    // Item negociado

    public int ItemId { get; set; }

    public Item? Item { get; set; }





    // Compra ou Venda

    // Buy
    // Sell

    public string Type { get; set; } = "";





    // Valor

    public int Value { get; set; }





    public DateTime CreatedAt { get; set; }

        = DateTime.UtcNow;


}