using System.ComponentModel.DataAnnotations;


namespace Artigo33.API.Models;


public class Inventory
{


    [Key]
    public int Id { get; set; }



    // Dono

    public int CharacterId { get; set; }


    public Character? Character { get; set; }





    // Capacidade

    public double MaxWeight { get; set; }
        = 50;



    public double CurrentWeight { get; set; }
        = 0;





    // Slots

    public int MaxSlots { get; set; }
        = 30;




    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;



}