using Microsoft.EntityFrameworkCore;

using Artigo33.API.Data;
using Artigo33.API.Models;



namespace Artigo33.API.Services;



public class CombatService

{

    private readonly AppDbContext _db;

    private readonly ProgressionService _progression;



    public CombatService(

        AppDbContext db,

        ProgressionService progression

    )

    {

        _db = db;

        _progression = progression;

    }








    // =====================================
    // INICIAR COMBATE
    // =====================================


    public async Task<CombatLog?> StartCombat(

        int characterId,

        int enemyId

    )

    {


        var character = await _db.Characters

            .FirstOrDefaultAsync(

                x => x.Id == characterId

            );




        var enemy = await _db.Enemies

            .FirstOrDefaultAsync(

                x => x.Id == enemyId

            );





        if(character == null || enemy == null)

            return null;








        // ================================
        // STATUS INICIAL
        // ================================


        int enemyHealth = enemy.Health;


        int characterHealth = character.CurrentHealth > 0

            ? character.CurrentHealth

            : character.Health;




        int damageDealt = 0;

        int damageReceived = 0;


        int turns = 0;


        int criticalHits = 0;


        int dodges = 0;



        List<string> history = new();




        Random random = new();









        // =====================================
        // LOOP DE BATALHA
        // =====================================


        while(

            characterHealth > 0 &&

            enemyHealth > 0

        )

        {

            turns++;





            // =====================================
            // ATAQUE DO PERSONAGEM
            // =====================================


            bool critical =

                random.Next(0,100)

                < character.CriticalChance;




            int playerDamage =

                character.AttackPower

                - enemy.Defense;




            if(playerDamage < 1)

                playerDamage = 1;





            if(critical)

            {

                playerDamage *= 2;

                criticalHits++;


                history.Add(

                    $"ROUND {turns}: ATAQUE CRÍTICO! Dano causado: {playerDamage}"

                );

            }

            else

            {

                history.Add(

                    $"ROUND {turns}: Personagem atacou causando {playerDamage} dano"

                );

            }





            enemyHealth -= playerDamage;


            damageDealt += playerDamage;








            if(enemyHealth <= 0)

                break;









            // =====================================
            // ATAQUE DO INIMIGO
            // =====================================



            bool dodge =

                random.Next(0,100)

                < character.DodgeChance;




            if(dodge)

            {

                dodges++;


                history.Add(

                    $"ROUND {turns}: Personagem esquivou do ataque inimigo"

                );


                continue;

            }






            int enemyDamage =

                enemy.Attack

                - character.DefensePower;





            if(enemyDamage < 1)

                enemyDamage = 1;






            characterHealth -= enemyDamage;


            damageReceived += enemyDamage;





            history.Add(

                $"ROUND {turns}: Inimigo causou {enemyDamage} dano"

            );



        }









        // =====================================
        // RESULTADO
        // =====================================



        bool victory = characterHealth > 0;





        character.CurrentHealth =

            characterHealth;









        var log = new CombatLog

        {

            CharacterId = characterId,


            EnemyId = enemyId,



            Result = victory

                ? "Vitória"

                : "Derrota",




            DamageDealt = damageDealt,


            DamageReceived = damageReceived,


            Turns = turns,



            CriticalHits = criticalHits,


            Dodges = dodges,



            BattleHistory =

                string.Join(

                    "\n",

                    history

                ),




            Description = victory

                ?

                $"O personagem derrotou {enemy.Name}"

                :

                $"O personagem foi derrotado por {enemy.Name}"

        };












        // =====================================
        // RECOMPENSAS
        // =====================================


        if(victory)

        {


            log.ExperienceGained =

                enemy.ExperienceReward;



            log.MoneyGained =

                enemy.MoneyReward;



            log.ReputationGained =

                enemy.ReputationReward;





            character.Money +=

                enemy.MoneyReward;




            character.Reputation +=

                enemy.ReputationReward;





            await _progression.AddExperience(

                characterId,

                enemy.ExperienceReward

            );


        }









        // =====================================
        // SALVAR COMBATE
        // =====================================


        _db.CombatLogs.Add(log);



        await _db.SaveChangesAsync();





        return log;


    }



}