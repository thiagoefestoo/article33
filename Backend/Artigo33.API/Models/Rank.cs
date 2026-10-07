using System.ComponentModel.DataAnnotations;

namespace Artigo33.API.Models;


public class Rank
{

    [Key]
    public int Id { get; set; }


    public string Name { get; set; } = "";


    // POLICIAL ou BANDIDO
    public string Type { get; set; } = "";


    public int LevelRequired { get; set; }


    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;

}