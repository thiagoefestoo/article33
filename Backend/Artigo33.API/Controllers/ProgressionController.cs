using Microsoft.AspNetCore.Mvc;
using Artigo33.API.Services;


namespace Artigo33.API.Controllers;


[ApiController]
[Route("api/progression")]
public class ProgressionController : ControllerBase
{

    private readonly ProgressionService _progressionService;


    public ProgressionController(
        ProgressionService progressionService
    )
    {
        _progressionService = progressionService;
    }



    // ==========================================
    // ADICIONAR EXPERIÊNCIA
    // POST: api/progression/add-xp
    // ==========================================

    [HttpPost("add-xp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddExperience(
        [FromBody] AddExperienceRequest request
    )
    {

        if(request.CharacterId <= 0)
        {
            return BadRequest(new
            {
                message = "CharacterId inválido"
            });
        }


        if(request.Experience <= 0)
        {
            return BadRequest(new
            {
                message = "A experiência deve ser maior que zero"
            });
        }



        var character = await _progressionService.AddExperience(
            request.CharacterId,
            request.Experience
        );



        if(character == null)
        {
            return NotFound(new
            {
                message = "Personagem não encontrado"
            });
        }



        return Ok(new
        {

            message = "Experiência adicionada com sucesso",


            character = new
            {
                id = character.Id,

                name = character.Name,


                progression = new
                {
                    level = character.Level,

                    experience = character.Experience,

                    rank = character.Rank?.Name,

                    className = character.CharacterClass?.Name
                },


                status = new
                {
                    health = character.Health,

                    energy = character.Energy
                },


                economy = new
                {
                    money = character.Money
                }

            }

        });

    }


}



// ==========================================
// REQUEST
// ==========================================

public record AddExperienceRequest
(
    int CharacterId,

    int Experience
);