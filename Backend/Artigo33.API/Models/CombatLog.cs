using System.ComponentModel.DataAnnotations;


namespace Artigo33.API.Models;



public class CombatLog

{

    [Key]
    public int Id { get; set; }





    // =====================================
    // PARTICIPANTES
    // =====================================


    // Personagem jogador

    public int CharacterId { get; set; }



    // Inimigo enfrentado

    public int EnemyId { get; set; }








    // =====================================
    // RESULTADO DA BATALHA
    // =====================================


    /*
     
        Vitória
        Derrota

    */


    public string Result { get; set; } = "";








    // =====================================
    // DADOS DO COMBATE
    // =====================================


    // Dano total causado

    public int DamageDealt { get; set; } = 0;



    // Dano total recebido

    public int DamageReceived { get; set; } = 0;



    // Quantidade de rodadas

    public int Turns { get; set; } = 0;





    // =====================================
    // COMBATE AVANÇADO
    // =====================================


    // Quantidade de golpes críticos

    public int CriticalHits { get; set; } = 0;



    // Quantidade de ataques esquivados

    public int Dodges { get; set; } = 0;



    // Histórico detalhado da batalha

    public string BattleHistory { get; set; } = "";









    // =====================================
    // RECOMPENSAS
    // =====================================


    public int ExperienceGained { get; set; } = 0;



    public int MoneyGained { get; set; } = 0;



    public int ReputationGained { get; set; } = 0;









    // =====================================
    // DESCRIÇÃO
    // =====================================


    public string Description { get; set; } = "";











    // =====================================
    // DATA
    // =====================================


    public DateTime CreatedAt { get; set; }

        = DateTime.UtcNow;



}