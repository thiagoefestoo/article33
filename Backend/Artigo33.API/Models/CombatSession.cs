using System.ComponentModel.DataAnnotations;

namespace Artigo33.API.Models;

public class CombatSession
{

    [Key]
    public int Id { get; set; }


    // Participantes

    public int CharacterId { get; set; }

    public Character? Character { get; set; }


    public int EnemyId { get; set; }

    public Enemy? Enemy { get; set; }



    // Estado atual da batalha

    public int CharacterHealth { get; set; }

    public int EnemyHealth { get; set; }



    // Controle

    public int CurrentTurn { get; set; } = 1;


    // PLAYER ou ENEMY

    public string TurnOwner { get; set; } = "PLAYER";


    // ACTIVE / VICTORY / DEFEAT

    public string Status { get; set; } = "ACTIVE";



    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;


}