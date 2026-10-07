using Microsoft.EntityFrameworkCore;
using Artigo33.API.Data;
using Artigo33.API.Models;


namespace Artigo33.API.Services;


public class CharacterAttributeService
{

    private readonly AppDbContext _db;


    public CharacterAttributeService(AppDbContext db)
    {
        _db = db;
    }





    // ==========================================
    // DISTRIBUIR PONTOS DE ATRIBUTO
    // ==========================================

    public async Task<Character?> UpgradeAttributes(

        int characterId,

        int strength,

        int agility,

        int intelligence,

        int vitality,

        int accuracy,

        int charisma

    )
    {


        var character = await _db.Characters

            .FirstOrDefaultAsync(x => x.Id == characterId);



        if(character == null)
            return null;





        // ===============================
        // VALIDA VALORES
        // ===============================

        if(
            strength < 0 ||
            agility < 0 ||
            intelligence < 0 ||
            vitality < 0 ||
            accuracy < 0 ||
            charisma < 0
        )
        {
            return null;
        }





        int totalPoints =

            strength +

            agility +

            intelligence +

            vitality +

            accuracy +

            charisma;





        // Não pode gastar mais do que possui

        if(totalPoints > character.AttributePoints)
        {
            return null;
        }







        // ===============================
        // APLICA ALTERAÇÕES
        // ===============================


        character.Strength += strength;

        character.Agility += agility;

        character.Intelligence += intelligence;

        character.Vitality += vitality;

        character.Accuracy += accuracy;

        character.Charisma += charisma;




        // Remove pontos usados

        character.AttributePoints -= totalPoints;





        await _db.SaveChangesAsync();



        return character;

    }



}