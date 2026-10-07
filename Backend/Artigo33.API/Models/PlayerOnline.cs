namespace Artigo33.API.Models;


public class PlayerOnline
{

    public int Id { get; set; }



    // Personagem conectado
    public int CharacterId { get; set; }



    // Relacionamento com Character
    public Character Character { get; set; } = null!;



    // Última comunicação recebida do Unity
    public DateTime LastHeartbeat { get; set; }



    // Status atual
    public bool IsOnline { get; set; }



    // Data de entrada no servidor
    public DateTime ConnectedAt { get; set; }



    public PlayerOnline()
    {

        LastHeartbeat = DateTime.UtcNow;

        ConnectedAt = DateTime.UtcNow;

        IsOnline = true;

    }


}