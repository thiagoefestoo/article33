using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Artigo33.API.Data;
using Artigo33.API.Models;


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


    // =====================================
    // CRIAR PERSONAGEM
    // =====================================

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateCharacterRequest request)
    {
        // Verifica usuário
        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.Id == request.UserId);


        if (user == null)
        {
            return BadRequest(new
            {
                message = "Usuário não encontrado"
            });
        }


        // Verifica classe
        var characterClass = await _db.CharacterClasses
            .FirstOrDefaultAsync(
                x => x.Id == request.CharacterClassId
            );


        if (characterClass == null)
        {
            return BadRequest(new
            {
                message = "Classe inválida"
            });
        }


        // Verifica Rank
        var rank = await _db.Ranks
            .FirstOrDefaultAsync(
                x => x.Id == request.RankId
            );


        if (rank == null)
        {
            return BadRequest(new
            {
                message = "Rank inválido"
            });
        }


        // Confere compatibilidade
        if (rank.Type != characterClass.Type)
        {
            return BadRequest(new
            {
                message = "Esse Rank não pertence a essa classe"
            });
        }


        // Criação do personagem
        var character = new Character
        {
            UserId = request.UserId,

            // Identidade
            Name = request.Name,
            Gender = request.Gender,

            // Aparência
            Face = request.Face,
            Hair = request.Hair,
            Skin = request.Skin,

            // Classe
            CharacterClassId = request.CharacterClassId,
            RankId = request.RankId,

            // Especialização
            SubClass = request.SubClass,

            // Facção
            Faction = request.Faction,

            // Progressão
            Level = 1,
            Experience = 0,

            // Economia
            Money = characterClass.InitialMoney,

            // Status
            Health = characterClass.InitialHealth,
            Energy = 100,

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


    // =====================================
    // BUSCAR PRIMEIRO PERSONAGEM DO USUÁRIO
    // Mantido para compatibilidade com o sistema atual
    // =====================================

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> Get(int userId)
    {
        var character = await _db.Characters
            .Include(x => x.CharacterClass)
            .Include(x => x.Rank)
            .FirstOrDefaultAsync(
                x => x.UserId == userId
            );


        if (character == null)
        {
            return NotFound(new
            {
                message = "Personagem não encontrado"
            });
        }


        return Ok(character);
    }


    // =====================================
    // LISTAR TODOS OS PERSONAGENS DO USUÁRIO
    // Usado pela tela CharacterSelection
    // =====================================

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetCharactersByUser(int userId)
    {
        // Verifica se o usuário existe
        var userExists = await _db.Users
            .AnyAsync(x => x.Id == userId);


        if (!userExists)
        {
            return NotFound(new
            {
                message = "Usuário não encontrado"
            });
        }


        var characters = await _db.Characters
            .Where(x => x.UserId == userId)

            .Include(x => x.CharacterClass)
            .Include(x => x.Rank)

            .OrderBy(x => x.Id)

            .Select(x => new
            {
                id = x.Id,

                userId = x.UserId,

                name = x.Name,

                gender = x.Gender,

                face = x.Face,

                hair = x.Hair,

                skin = x.Skin,

                faction = x.Faction,

                subClass = x.SubClass,

                characterClassId = x.CharacterClassId,

                characterClassName =
                    x.CharacterClass != null
                        ? x.CharacterClass.Name
                        : "",

                rankId = x.RankId,

                rankName =
                    x.Rank != null
                        ? x.Rank.Name
                        : "",

                level = x.Level,

                experience = x.Experience,

                money = x.Money,

                health = x.Health,

                energy = x.Energy,

                createdAt = x.CreatedAt
            })

            .ToListAsync();


        return Ok(new
        {
            count = characters.Count,

            characters
        });
    }
}


// =====================================
// REQUEST CRIAÇÃO PERSONAGEM
// =====================================

public record CreateCharacterRequest(
    int UserId,

    string Name,

    string Gender,

    string Face,

    string Hair,

    string Skin,

    string Faction,

    string SubClass,

    int CharacterClassId,

    int RankId
);