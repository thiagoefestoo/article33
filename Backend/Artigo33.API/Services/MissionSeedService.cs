using Artigo33.API.Data;
using Artigo33.API.Models;

namespace Artigo33.API.Services;


public class MissionSeedService
{

    private readonly AppDbContext _context;


    public MissionSeedService(AppDbContext context)
    {
        _context = context;
    }



    public void Seed()
    {

        if(_context.Missions.Any())
        {
            return;
        }



        var missions = new List<Mission>
        {

            new Mission
            {
                Name = "Primeira Patrulha",
                Type = "Polícia",
                Description = "Realize uma patrulha preventiva e mantenha a ordem pública.",
                ExperienceReward = 100,
                MoneyReward = 200,
                ReputationReward = 10,
                RequiredLevel = 1,
                Active = true
            },


            new Mission
            {
                Name = "Abordagem Suspeita",
                Type = "Polícia",
                Description = "Investigue uma ocorrência envolvendo um indivíduo suspeito.",
                ExperienceReward = 250,
                MoneyReward = 500,
                ReputationReward = 20,
                RequiredLevel = 2,
                Active = true
            },


            new Mission
            {
                Name = "Entrega Ilegal",
                Type = "Crime",
                Description = "Faça uma entrega clandestina sem ser identificado.",
                ExperienceReward = 150,
                MoneyReward = 400,
                ReputationReward = -10,
                RequiredLevel = 1,
                Active = true
            },


            new Mission
            {
                Name = "Operação Noturna",
                Type = "Polícia",
                Description = "Participe de uma operação de combate ao crime.",
                ExperienceReward = 500,
                MoneyReward = 1000,
                ReputationReward = 50,
                RequiredLevel = 5,
                Active = true
            }

        };


        _context.Missions.AddRange(missions);

        _context.SaveChanges();

    }

}