using System.ComponentModel.DataAnnotations;


namespace Artigo33.API.Models;


public class ItemDrop

{

    [Key]
    public int Id { get; set; }



    // =====================================
    // INIMIGO
    // =====================================


    public int EnemyId { get; set; }


    public Enemy? Enemy { get; set; }





    // =====================================
    // ITEM DROPPADO
    // =====================================


    public int ItemId { get; set; }


    public Item? Item { get; set; }





    // =====================================
    // CHANCE DE DROP
    // =====================================


    /*
     
     Exemplo:

     20 = 20%

     75 = 75%

    */


    public double DropChance { get; set; }





    // Quantidade recebida

    public int Quantity { get; set; } = 1;





    // =====================================
    // DATA
    // =====================================


    public DateTime CreatedAt { get; set; }

        = DateTime.UtcNow;


}