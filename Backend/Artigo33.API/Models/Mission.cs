using System.ComponentModel.DataAnnotations;

namespace Artigo33.API.Models;

public class Mission
{
    [Key]
    public int Id { get; set; }


    // Nome da missão
    [Required]
    public string Name { get; set; } = "";


    // Tipo:
    // Polícia
    // Crime
    public string Type { get; set; } = "";


    // Descrição
    public string Description { get; set; } = "";


    // Recompensas

    public int ExperienceReward { get; set; }

    public int MoneyReward { get; set; }

    public int ReputationReward { get; set; }


    // Nível mínimo

    public int RequiredLevel { get; set; } = 1;


    // Controle

    public bool Active { get; set; } = true;


    public DateTime CreatedAt { get; set; } =
        DateTime.UtcNow;
}