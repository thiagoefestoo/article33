using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Artigo33.API.Data;
using Artigo33.API.Models;
using Artigo33.API.Services;


namespace Artigo33.API.Controllers;


[ApiController]
[Route("api/unity")]
public class UnityController : ControllerBase
{
    private readonly UnityService _unityService;

    private readonly AppDbContext _context;


    public UnityController(
        UnityService unityService,
        AppDbContext context
    )
    {
        _unityService = unityService;

        _context = context;
    }


    // =====================================
    // CARREGAR JOGADOR PELO USER ID
    // LEGADO / COMPATIBILIDADE
    // =====================================

    [HttpGet("load/{userId:int}")]
    public async Task<IActionResult> LoadPlayer(
        int userId
    )
    {
        var player =
            await _unityService.LoadPlayer(
                userId
            );


        if (player == null)
        {
            return NotFound(new
            {
                success = false,

                message =
                    "Nenhum personagem encontrado"
            });
        }


        return Ok(new
        {
            success = true,

            player
        });
    }


    // =====================================
    // CARREGAR PERSONAGEM SELECIONADO
    // =====================================

    [HttpGet("load-character/{characterId:int}")]
    public async Task<IActionResult> LoadCharacter(
        int characterId
    )
    {
        var player =
            await _unityService
                .LoadPlayerByCharacterId(
                    characterId
                );


        if (player == null)
        {
            return NotFound(new
            {
                success = false,

                message =
                    "Personagem não encontrado"
            });
        }


        return Ok(new
        {
            success = true,

            player
        });
    }


    // =====================================
    // HEARTBEAT ONLINE
    // =====================================

    [HttpPost("heartbeat")]
    public async Task<IActionResult> Heartbeat(
        HeartbeatRequest request
    )
    {
        var characterExists =
            await _context.Characters
                .AnyAsync(
                    x =>
                        x.Id ==
                        request.CharacterId
                );


        if (!characterExists)
        {
            return NotFound(new
            {
                success = false,

                message =
                    "Personagem não encontrado"
            });
        }


        var onlinePlayer =
            await _context.PlayersOnline
                .FirstOrDefaultAsync(
                    x =>
                        x.CharacterId ==
                        request.CharacterId
                );


        if (onlinePlayer == null)
        {
            onlinePlayer =
                new PlayerOnline
                {
                    CharacterId =
                        request.CharacterId,

                    LastHeartbeat =
                        DateTime.UtcNow,

                    IsOnline =
                        true
                };


            _context.PlayersOnline.Add(
                onlinePlayer
            );
        }
        else
        {
            onlinePlayer.LastHeartbeat =
                DateTime.UtcNow;

            onlinePlayer.IsOnline =
                true;
        }


        await _context.SaveChangesAsync();


        return Ok(new
        {
            success = true,

            message =
                "Jogador online atualizado",

            characterId =
                request.CharacterId,

            serverTime =
                DateTime.UtcNow
        });
    }
}