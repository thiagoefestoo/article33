namespace Artigo33.API.Models.DTO;

public class CreateCharacterDTO
{
    public int UserId { get; set; }

    public string Name { get; set; } = "";

    public string Gender { get; set; } = "";

    public string Race { get; set; } = "";

    public string Class { get; set; } = "";

    public string Face { get; set; } = "";

    public string Hair { get; set; } = "";

    public string Skin { get; set; } = "";

    public string Faction { get; set; } = "";
}