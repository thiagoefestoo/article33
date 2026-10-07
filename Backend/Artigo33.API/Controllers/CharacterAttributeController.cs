using Microsoft.AspNetCore.Mvc;
using Artigo33.API.Services;


namespace Artigo33.API.Controllers;



[ApiController]

[Route("api/character/attributes")]

public class CharacterAttributeController : ControllerBase
{


    private readonly CharacterAttributeService _service;



    public CharacterAttributeController(
        CharacterAttributeService service
    )
    {
        _service = service;
    }






    // ==========================================
    // DISTRIBUIR ATRIBUTOS
    // ==========================================


    [HttpPost("upgrade")]

    public async Task<IActionResult> Upgrade(

        UpgradeAttributeRequest request

    )
    {


        var character = await _service.UpgradeAttributes(

            request.CharacterId,

            request.Strength,

            request.Agility,

            request.Intelligence,

            request.Vitality,

            request.Accuracy,

            request.Charisma

        );





        if(character == null)
        {

            return BadRequest(new
            {
                message =
                "Não foi possível distribuir atributos"
            });

        }







        return Ok(new
        {

            message =
            "Atributos atualizados com sucesso",



            remainingPoints =
            character.AttributePoints,



            attributes = new
            {

                strength = character.Strength,

                agility = character.Agility,

                intelligence = character.Intelligence,

                vitality = character.Vitality,

                accuracy = character.Accuracy,

                charisma = character.Charisma

            }

        });

    }


}





// ==========================================
// REQUEST
// ==========================================


public record UpgradeAttributeRequest(

    int CharacterId,

    int Strength,

    int Agility,

    int Intelligence,

    int Vitality,

    int Accuracy,

    int Charisma

);