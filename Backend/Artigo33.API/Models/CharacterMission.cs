using System.ComponentModel.DataAnnotations;

namespace Artigo33.API.Models;


public class CharacterMission
{

    [Key]
    public int Id { get; set; }



    // =====================================
    // PERSONAGEM
    // =====================================

    public int CharacterId { get; set; }

    public Character Character { get; set; } = null!;




    // =====================================
    // MISSÃO
    // =====================================

    public int MissionId { get; set; }

    public Mission Mission { get; set; } = null!;




    // =====================================
    // STATUS DA MISSÃO
    // =====================================

    // Accepted
    // Completed
    // Failed

    public string Status { get; set; } = "Accepted";





    // =====================================
    // DATAS DE CONTROLE
    // =====================================

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;


    public DateTime? CompletedAt { get; set; }





    // =====================================
    // RECOMPENSA
    // =====================================

    public bool RewardClaimed { get; set; } = false;


}