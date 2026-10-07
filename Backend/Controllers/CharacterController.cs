using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Artigo33.API.Data;
using Artigo33.API.Models;
using Artigo33.API.Models.DTO;


namespace Artigo33.API.Controllers;


[ApiController]
[Route("api/character")]
public class CharacterController : ControllerBase
{

    private readonly AppDbContext _db;


    public CharacterController(AppDbContext db)
    {
        _db = db;
    }



    // =====================================================
    // CRIAR PERSONAGEM
    // =====================================================


    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateCharacterDTO dto)
    {


        var character = new Character
        {

            UserId = dto.UserId,


            // IDENTIDADE

            Name = dto.Name,

            Gender = dto.Gender,

            Face = dto.Face,

            Hair = dto.Hair,

            Skin = dto.Skin,

            Faction = dto.Faction,



            // ==============================
            // PROGRESSÃO INICIAL
            // ==============================


            Level = 1,

            Experience = 0,

            AttributePoints = 0,



            // ==============================
            // ECONOMIA
            // ==============================


            Money = 500,

            Reputation = 0,



            // ==============================
            // STATUS
            // ==============================


            Health = 100,

            CurrentHealth = 100,

            Energy = 100,



            // ==============================
            // ATRIBUTOS RPG
            // ==============================


            Strength = 10,

            Agility = 10,

            Intelligence = 10,

            Vitality = 10,

            Accuracy = 10,

            Charisma = 10,



            // ==============================
            // ESTADO
            // ==============================


            IsAlive = true,


            CreatedAt = DateTime.UtcNow

        };



        _db.Characters.Add(character);


        await _db.SaveChangesAsync();



        return Ok(new
        {

            message = "Personagem criado com sucesso",

            characterId = character.Id,

            character

        });


    }






    // =====================================================
    // BUSCAR PERSONAGEM PELO USUÁRIO
    // =====================================================


    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(int userId)
    {


        var character = await _db.Characters

            .Include(x => x.CharacterClass)

            .Include(x => x.Rank)

            .FirstOrDefaultAsync(

                x => x.UserId == userId

            );



        if(character == null)

            return NotFound(

                "Personagem não encontrado."

            );




        return Ok(new
        {

            success = true,


            player = new
            {

                character.Id,

                character.Name,


                character.Level,

                character.Experience,


                character.Money,

                character.Reputation,


                Stats = new
                {

                    character.CurrentHealth,

                    character.Health,

                    character.Energy,


                    character.Strength,

                    character.Agility,

                    character.Intelligence,


                    character.Accuracy,

                    character.Charisma,

                    character.Vitality

                },


                Combat = new
                {

                    character.CriticalChance,

                    character.DodgeChance,


                    character.AttackPower,

                    character.DefensePower

                },


                CharacterClass = character.CharacterClass,

                Rank = character.Rank,


                character.Faction,


                character.CreatedAt

            }


        });


    }






    // =====================================================
    // ATUALIZAR STATUS DO PERSONAGEM
    // =====================================================


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        Character characterData
    )
    {


        var character = await _db.Characters

            .FirstOrDefaultAsync(

                x => x.Id == id

            );



        if(character == null)

            return NotFound();



        character.Name = characterData.Name;


        character.CurrentHealth = characterData.CurrentHealth;


        character.Energy = characterData.Energy;


        character.Money = characterData.Money;


        character.Experience = characterData.Experience;


        character.Level = characterData.Level;



        character.Strength = characterData.Strength;


        character.Agility = characterData.Agility;


        character.Intelligence = characterData.Intelligence;


        character.Vitality = characterData.Vitality;


        character.Accuracy = characterData.Accuracy;


        character.Charisma = characterData.Charisma;



        character.Reputation = characterData.Reputation;



        await _db.SaveChangesAsync();



        return Ok(new
        {

            message = "Personagem atualizado",

            character

        });


    }


}