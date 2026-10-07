using System.ComponentModel.DataAnnotations;

namespace Artigo33.API.Models;


public class Character
{

    [Key]
    public int Id { get; set; }



    // Dono do personagem

    [Required]
    public int UserId { get; set; }




    // ==========================
    // IDENTIDADE
    // ==========================

    [Required]
    public string Name { get; set; } = "";

    public string Gender { get; set; } = "Humano";





    // ==========================
    // CLASSE
    // ==========================

    public int CharacterClassId { get; set; }

    public CharacterClass? CharacterClass { get; set; }





    // ==========================
    // PATENTE
    // ==========================

    public int RankId { get; set; }

    public Rank? Rank { get; set; }





    // ==========================
    // SUBCLASSE
    // ==========================

    public string SubClass { get; set; } = "";





    // ==========================
    // APARÊNCIA
    // ==========================

    public string Hair { get; set; } = "";

    public string Skin { get; set; } = "";

    public string Face { get; set; } = "";





    // ==========================
    // FACÇÃO
    // ==========================

    public string Faction { get; set; } = "";





    // ==========================
    // PROGRESSÃO RPG
    // ==========================

    public int Level { get; set; } = 1;

    public int Experience { get; set; } = 0;


    public int AttributePoints { get; set; } = 0;





    // ==========================
    // ECONOMIA
    // ==========================

    public int Money { get; set; } = 500;





    // ==========================
    // STATUS
    // ==========================

    // Vida máxima

    public int Health { get; set; } = 100;


    // Vida atual em combate

    public int CurrentHealth { get; set; } = 100;


    public int Energy { get; set; } = 100;





    // ==========================
    // ATRIBUTOS
    // ==========================


    public int Strength { get; set; } = 10;


    public int Agility { get; set; } = 10;


    public int Intelligence { get; set; } = 10;


    public int Vitality { get; set; } = 10;


    public int Accuracy { get; set; } = 10;


    public int Charisma { get; set; } = 10;



    public int Reputation { get; set; } = 0;






    // ==========================
    // COMBATE AVANÇADO
    // ==========================


    // chance % de crítico

    public int CriticalChance { get; set; } = 5;


    // chance % de esquiva

    public int DodgeChance { get; set; } = 5;



    public int AttackPower
    {
        get
        {
            return Strength * 2;
        }
    }



    public int DefensePower
    {
        get
        {
            return Vitality + Agility;
        }
    }







    // ==========================
    // ESTATÍSTICAS
    // ==========================

    public int MissionsCompleted { get; set; } = 0;


    public int Arrests { get; set; } = 0;


    public int CrimesCommitted { get; set; } = 0;






    // ==========================
    // ESTADO
    // ==========================

    public bool IsAlive { get; set; } = true;






    // ==========================
    // DATA
    // ==========================

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;


}