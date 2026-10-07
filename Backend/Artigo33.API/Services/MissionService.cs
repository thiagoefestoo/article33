using Microsoft.EntityFrameworkCore;
using Artigo33.API.Data;
using Artigo33.API.Models;


namespace Artigo33.API.Services;


public class MissionService
{

    private readonly AppDbContext _db;

    private readonly ProgressionService _progressionService;



    public MissionService(
        AppDbContext db,
        ProgressionService progressionService
    )
    {
        _db = db;
        _progressionService = progressionService;
    }







    // ==========================================
    // LISTAR MISSÕES DISPONÍVEIS
    // ==========================================

    public async Task<List<Mission>> GetAvailableMissions()
    {

        return await _db.Missions

            .Where(x => x.Active)

            .OrderBy(x => x.RequiredLevel)

            .ToListAsync();

    }









    // ==========================================
    // ACEITAR MISSÃO
    // ==========================================

    public async Task<CharacterMission?> AcceptMission(

        int characterId,

        int missionId

    )
    {


        var character = await _db.Characters

            .FirstOrDefaultAsync(x => x.Id == characterId);



        var mission = await _db.Missions

            .FirstOrDefaultAsync(x => x.Id == missionId);





        if(character == null || mission == null)

            return null;







        // =====================================
        // VERIFICA NÍVEL MÍNIMO
        // =====================================

        if(character.Level < mission.RequiredLevel)

            return null;








        // =====================================
        // EVITA DUPLICIDADE
        // =====================================

        var exists = await _db.CharacterMissions

            .AnyAsync(x =>

                x.CharacterId == characterId &&

                x.MissionId == missionId &&

                x.Status != "Completed"

            );




        if(exists)

            return null;








        var characterMission = new CharacterMission

        {

            CharacterId = characterId,


            MissionId = missionId,


            Status = "Accepted",


            StartedAt = DateTime.UtcNow,


            RewardClaimed = false

        };







        _db.CharacterMissions.Add(characterMission);



        await _db.SaveChangesAsync();




        return characterMission;

    }















    // ==========================================
    // FINALIZAR MISSÃO
    // ==========================================

    public async Task<CharacterMission?> CompleteMission(

        int characterMissionId

    )

    {



        var cm = await _db.CharacterMissions


            .Include(x => x.Mission)


            .Include(x => x.Character)


            .FirstOrDefaultAsync(

                x => x.Id == characterMissionId

            );







        if(cm == null)

            return null;








        // =====================================
        // JÁ COMPLETADA
        // =====================================

        if(cm.Status == "Completed")

            return cm;








        cm.Status = "Completed";


        cm.CompletedAt = DateTime.UtcNow;







        // =====================================
        // ENTREGA RECOMPENSA
        // =====================================

        if(!cm.RewardClaimed)

        {



            // XP passa pelo sistema de progressão

            await _progressionService.AddExperience(

                cm.Character!.Id,

                cm.Mission!.ExperienceReward

            );





            // Economia

            cm.Character.Money +=

                cm.Mission.MoneyReward;






            // Reputação

            cm.Character.Reputation +=

                cm.Mission.ReputationReward;






            cm.RewardClaimed = true;


        }







        await _db.SaveChangesAsync();







        return cm;

    }



}