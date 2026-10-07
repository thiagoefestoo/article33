using System.ComponentModel.DataAnnotations;

namespace Artigo33.API.Models;


public class CharacterClass
{
    [Key]
    public int Id { get; set; }


    public string Name { get; set; } = "";


    // POLICIAL ou BANDIDO
    public string Type { get; set; } = "";


    public string Description { get; set; } = "";


    public int InitialHealth { get; set; } = 100;


    public int InitialMoney { get; set; } = 500;


    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;
}