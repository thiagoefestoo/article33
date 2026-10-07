using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Artigo33.API.Data;
using Artigo33.API.Services;
using Artigo33.API.Models.DTO;



namespace Artigo33.API.Controllers;



[ApiController]

[Route("api/equipment")]

public class EquipmentController : ControllerBase

{

    private readonly EquipmentService _equipmentService;

    private readonly AppDbContext _db;





    public EquipmentController(

        EquipmentService equipmentService,

        AppDbContext db

    )

    {

        _equipmentService = equipmentService;

        _db = db;

    }









    // =====================================
    // EQUIPAR ITEM
    // =====================================


    [HttpPost("equip")]

    public async Task<IActionResult> Equip(

        [FromBody] EquipItemRequest request

    )

    {

        var equipment = await _equipmentService.EquipItem(

            request.CharacterId,

            request.ItemId

        );





        if(equipment == null)

        {

            return BadRequest(new

            {

                message = "Não foi possível equipar o item"

            });

        }







        return Ok(new

        {

            message = "Equipamento equipado com sucesso",


            equipment = new

            {

                id = equipment.Id,

                characterId = equipment.CharacterId,

                itemId = equipment.ItemId,

                slot = equipment.Slot,

                equipped = equipment.Equipped,

                equippedAt = equipment.EquippedAt

            }

        });

    }















    // =====================================
    // DESEQUIPAR ITEM
    // =====================================


    [HttpPost("unequip")]

    public async Task<IActionResult> Unequip(

        [FromBody] UnequipItemRequest request

    )

    {

        var equipment = await _equipmentService.UnequipItem(

            request.CharacterId,

            request.ItemId

        );





        if(equipment == null)

        {

            return NotFound(new

            {

                message = "Equipamento não encontrado"

            });

        }







        return Ok(new

        {

            message = "Equipamento removido com sucesso",


            equipment = new

            {

                id = equipment.Id,

                characterId = equipment.CharacterId,

                itemId = equipment.ItemId,

                slot = equipment.Slot,

                equipped = equipment.Equipped

            }

        });

    }















    // =====================================
    // LISTAR EQUIPAMENTOS
    // DO PERSONAGEM
    // =====================================


    [HttpGet("{characterId}")]

    public async Task<IActionResult> GetEquipment(

        int characterId

    )

    {

        var equipments = await _db.CharacterEquipments

            .Include(x => x.Item)

            .Where(x =>

                x.CharacterId == characterId &&

                x.Equipped

            )

            .Select(x => new

            {

                id = x.Id,


                slot = x.Slot,


                equipped = x.Equipped,


                item = x.Item == null ? null : new

                {

                    id = x.Item.Id,


                    name = x.Item.Name,


                    description = x.Item.Description,


                    type = x.Item.Type,


                    rarity = x.Item.Rarity,


                    attack = x.Item.Attack,


                    defense = x.Item.Defense,


                    strengthBonus = x.Item.StrengthBonus,


                    intelligenceBonus = x.Item.IntelligenceBonus,


                    agilityBonus = x.Item.AgilityBonus,


                    healthBonus = x.Item.HealthBonus,


                    energyBonus = x.Item.EnergyBonus,


                    reputationBonus = x.Item.ReputationBonus

                }

            })

            .ToListAsync();







        return Ok(new

        {

            characterId,


            total = equipments.Count,


            equipments

        });

    }

}