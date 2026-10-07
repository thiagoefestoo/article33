using Artigo33.API.Data;
using Artigo33.API.Models;


namespace Artigo33.API.Services;


public class EnemySeedService
{

    private readonly AppDbContext _db;



    public EnemySeedService(AppDbContext db)
    {
        _db = db;
    }







    public void Seed()
    {

        // Evita duplicar inimigos
        if(_db.Enemies.Any())
            return;







        var enemies = new List<Enemy>
        {

            // =================================
            // NÍVEL 1
            // =================================

            new Enemy
            {
                Name = "Suspeito Procurado",

                Description =
                "Um criminoso comum procurado pela polícia.",

                Type = "Bandit",

                Level = 1,

                Health = 80,

                Attack = 10,

                Defense = 3,

                ExperienceReward = 50,

                MoneyReward = 100,

                ReputationReward = 2,

                Active = true,

                CreatedAt = DateTime.UtcNow
            },






            // =================================
            // NÍVEL 2
            // =================================

            new Enemy
            {
                Name = "Olheiro Rival",

                Description =
                "Membro de uma organização rival observando território.",

                Type = "Gang",

                Level = 2,

                Health = 120,

                Attack = 18,

                Defense = 8,

                ExperienceReward = 100,

                MoneyReward = 200,

                ReputationReward = 5,

                Active = true,

                CreatedAt = DateTime.UtcNow
            },








            // =================================
            // NÍVEL 3
            // =================================

            new Enemy
            {
                Name = "Criminoso Armado",

                Description =
                "Um indivíduo perigoso envolvido em crimes violentos.",

                Type = "Bandit",

                Level = 3,

                Health = 150,

                Attack = 25,

                Defense = 10,

                ExperienceReward = 150,

                MoneyReward = 300,

                ReputationReward = 8,

                Active = true,

                CreatedAt = DateTime.UtcNow
            },








            // =================================
            // NÍVEL 5
            // =================================

            new Enemy
            {
                Name = "Traficante Iniciante",

                Description =
                "Começando sua carreira dentro de uma organização criminosa.",

                Type = "Gang",

                Level = 5,

                Health = 250,

                Attack = 40,

                Defense = 15,

                ExperienceReward = 300,

                MoneyReward = 700,

                ReputationReward = 20,

                Active = true,

                CreatedAt = DateTime.UtcNow
            },









            // =================================
            // CHEFE
            // =================================

            new Enemy
            {
                Name = "Chefe da Organização",

                Description =
                "Líder de uma grande organização criminosa.",

                Type = "Boss",

                Level = 20,

                Health = 1500,

                Attack = 120,

                Defense = 60,

                ExperienceReward = 2000,

                MoneyReward = 5000,

                ReputationReward = 100,

                Active = true,

                CreatedAt = DateTime.UtcNow
            }


        };







        _db.Enemies.AddRange(enemies);


        _db.SaveChanges();

    }


}