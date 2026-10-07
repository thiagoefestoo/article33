using Microsoft.EntityFrameworkCore;
using Artigo33.API.Data;
using Artigo33.API.Models;


namespace Artigo33.API.Services;


public class ProgressionService
{

    private readonly AppDbContext _db;


    public ProgressionService(AppDbContext db)
    {
        _db = db;
    }






    // ==========================================
    // ADICIONAR EXPERIÊNCIA
    // ==========================================

    public async Task<Character?> AddExperience(
        int characterId,
        int experience
    )
    {

        if(experience <= 0)
            return null;



        var character = await _db.Characters

            .Include(x => x.Rank)

            .Include(x => x.CharacterClass)

            .FirstOrDefaultAsync(
                x => x.Id == characterId
            );



        if(character == null)
            return null;





        // adiciona XP recebido

        character.Experience += experience;





        // verifica evolução

        CheckLevelUp(character);





        // verifica promoção

        await CheckRankUp(character);





        await _db.SaveChangesAsync();





        return character;

    }













    // ==========================================
    // SISTEMA DE LEVEL
    // ==========================================

    private void CheckLevelUp(
        Character character
    )
    {



        int requiredXP =
            CalculateRequiredExperience(
                character.Level
            );





        while(character.Experience >= requiredXP)
        {



            character.Experience -= requiredXP;



            character.Level++;





            ApplyLevelReward(character);





            requiredXP =
                CalculateRequiredExperience(
                    character.Level
                );

        }


    }













    // ==========================================
    // CALCULA XP NECESSÁRIO
    // ==========================================

    private int CalculateRequiredExperience(
        int level
    )
    {

        return level * level * 100;

    }













    // ==========================================
    // RECOMPENSA DE LEVEL UP
    // ==========================================

    private void ApplyLevelReward(
        Character character
    )
    {


        // pontos para distribuir

        character.AttributePoints += 3;



        // evolução física

        character.Health += 20;



        character.Energy = 100;



        // pequenos ganhos naturais

        character.Strength += 1;

        character.Agility += 1;

        character.Intelligence += 1;


    }













    // ==========================================
    // SISTEMA DE PROMOÇÃO DE RANK
    // ==========================================

    private async Task CheckRankUp(
        Character character
    )
    {


        if(character.CharacterClass == null)

            return;





        var nextRank = await _db.Ranks

            .Where(x =>

                x.Type == character.CharacterClass.Type

                &&

                x.LevelRequired <= character.Level

            )

            .OrderByDescending(
                x => x.LevelRequired
            )

            .FirstOrDefaultAsync();







        if(nextRank == null)

            return;







        if(character.RankId != nextRank.Id)

        {

            character.RankId = nextRank.Id;

        }



    }



}