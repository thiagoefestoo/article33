using Microsoft.AspNetCore.Mvc;

using Artigo33.API.Services;


namespace Artigo33.API.Controllers;



[ApiController]

[Route("api/mission")]

public class MissionController : ControllerBase

{


    private readonly MissionService _missionService;



    public MissionController(

        MissionService missionService

    )

    {

        _missionService = missionService;

    }







    // ==========================================
    // LISTAR MISSÕES DISPONÍVEIS
    // ==========================================


    [HttpGet("list")]

    public async Task<IActionResult> GetMissions()

    {


        var missions = await _missionService.GetAvailableMissions();



        return Ok(new

        {

            total = missions.Count,

            missions

        });

    }








    // ==========================================
    // ACEITAR MISSÃO
    // ==========================================


    [HttpPost("accept")]

    public async Task<IActionResult> AcceptMission(

        AcceptMissionRequest request

    )

    {


        var mission = await _missionService.AcceptMission(

            request.CharacterId,

            request.MissionId

        );



        if(mission == null)

        {

            return BadRequest(new

            {

                message = "Não foi possível aceitar a missão"

            });

        }



        return Ok(new

        {

            message = "Missão aceita com sucesso",

            characterMissionId = mission.Id,

            characterId = mission.CharacterId,

            missionId = mission.MissionId,

            startedAt = mission.StartedAt

        });

    }









    // ==========================================
    // COMPLETAR MISSÃO
    // ==========================================


    [HttpPost("complete")]

    public async Task<IActionResult> CompleteMission(

        CompleteMissionRequest request

    )

    {


        var mission = await _missionService.CompleteMission(

            request.CharacterMissionId

        );



        if(mission == null)

        {

            return NotFound(new

            {

                message = "Missão não encontrada"

            });

        }



        return Ok(new

        {

            message = "Missão concluída!",


            reward = new

            {

                xp = mission.Mission!.ExperienceReward,

                money = mission.Mission.MoneyReward,

                reputation = mission.Mission.ReputationReward

            },


            character = new

            {

                id = mission.Character!.Id,

                name = mission.Character.Name,

                level = mission.Character.Level,

                experience = mission.Character.Experience,

                money = mission.Character.Money,

                reputation = mission.Character.Reputation

            }


        });

    }


}









// ==========================================
// REQUESTS
// ==========================================


public record AcceptMissionRequest(

    int CharacterId,

    int MissionId

);



public record CompleteMissionRequest(

    int CharacterMissionId

);