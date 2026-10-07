using Microsoft.AspNetCore.Mvc;
using Artigo33.API.Services;
using Artigo33.API.Models.DTO;


namespace Artigo33.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CombatController : ControllerBase
{


    private readonly CombatService _combatService;



    public CombatController(
        CombatService combatService
    )
    {
        _combatService = combatService;
    }







    // =================================
    // INICIAR COMBATE
    // =================================


    [HttpPost("start")]
    public async Task<IActionResult> StartCombat(
        StartCombatRequest request
    )
    {


        var result =
            await _combatService.StartCombat(
                request.CharacterId,
                request.EnemyId
            );



        if(result == null)
            return BadRequest(
                "Combate não encontrado."
            );



        return Ok(result);

    }


}