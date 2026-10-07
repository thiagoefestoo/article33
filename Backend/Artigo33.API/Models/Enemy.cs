using System.ComponentModel.DataAnnotations;


namespace Artigo33.API.Models;


public class Enemy

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
    // TIPO DO INIMIGO
    // =====================================

    /*
     
     Bandit
     Police
     Gang
     Boss
     NPC

    */

    public string Type { get; set; } = "Bandit";




    // =====================================
    // STATUS DE COMBATE
    // =====================================


    public int Health { get; set; } = 100;


    public int Attack { get; set; } = 10;


    public int Defense { get; set; } = 5;




    // =====================================
    // DIFICULDADE
    // =====================================


    public int Level { get; set; } = 1;



    // =====================================
    // RECOMPENSAS
    // =====================================


    public int ExperienceReward { get; set; } = 50;


    public int MoneyReward { get; set; } = 100;


    public int ReputationReward { get; set; } = 0;




    // =====================================
    // ESTADO
    // =====================================


    public bool Active { get; set; } = true;



    public DateTime CreatedAt { get; set; }

        = DateTime.UtcNow;


}