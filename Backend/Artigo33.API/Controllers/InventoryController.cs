using Microsoft.AspNetCore.Mvc;

using Artigo33.API.Services;



namespace Artigo33.API.Controllers;



[ApiController]

[Route("api/inventory")]

public class InventoryController : ControllerBase

{


    private readonly InventoryService _inventoryService;



    public InventoryController(

        InventoryService inventoryService

    )

    {

        _inventoryService = inventoryService;

    }









    // ==========================================
    // BUSCAR INVENTÁRIO
    // ==========================================


    [HttpGet("{characterId}")]

    public async Task<IActionResult> GetInventory(

        int characterId

    )

    {


        var inventory = await _inventoryService.GetInventory(

            characterId

        );



        if(inventory == null)

        {

            return NotFound(new

            {

                message = "Inventário não encontrado"

            });

        }





        return Ok(inventory);

    }













    // ==========================================
    // ADICIONAR ITEM
    // ==========================================


    [HttpPost("add")]

    public async Task<IActionResult> AddItem(

        AddItemRequest request

    )

    {


        var item = await _inventoryService.AddItem(

            request.CharacterId,

            request.ItemId,

            request.Quantity

        );





        if(item == null)

        {

            return BadRequest(new

            {

                message = "Não foi possível adicionar item"

            });

        }





        return Ok(new

        {

            message = "Item adicionado",

            itemId = item.ItemId,

            quantity = item.Quantity

        });

    }












    // ==========================================
    // REMOVER ITEM
    // ==========================================


    [HttpPost("remove")]

    public async Task<IActionResult> RemoveItem(

        RemoveItemRequest request

    )

    {



        var result = await _inventoryService.RemoveItem(

            request.CharacterId,

            request.ItemId,

            request.Quantity

        );





        if(!result)

        {

            return BadRequest(new

            {

                message = "Não foi possível remover item"

            });

        }





        return Ok(new

        {

            message = "Item removido"

        });

    }













    // ==========================================
    // EQUIPAR ITEM
    // ==========================================


    [HttpPost("equip")]

    public async Task<IActionResult> EquipItem(

        EquipItemRequest request

    )

    {


        var result = await _inventoryService.EquipItem(

            request.CharacterId,

            request.ItemId

        );





        if(!result)

        {

            return BadRequest(new

            {

                message = "Não foi possível equipar item"

            });

        }





        return Ok(new

        {

            message = "Item equipado"

        });

    }













    // ==========================================
    // DESEQUIPAR ITEM
    // ==========================================


    [HttpPost("unequip")]

    public async Task<IActionResult> UnequipItem(

        EquipItemRequest request

    )

    {



        var result = await _inventoryService.UnequipItem(

            request.CharacterId,

            request.ItemId

        );





        if(!result)

        {

            return BadRequest(new

            {

                message = "Não foi possível desequipar item"

            });

        }





        return Ok(new

        {

            message = "Item desequipado"

        });

    }


}











// ==========================================
// REQUESTS
// ==========================================


public record AddItemRequest(

    int CharacterId,

    int ItemId,

    int Quantity

);





public record RemoveItemRequest(

    int CharacterId,

    int ItemId,

    int Quantity

);





public record EquipItemRequest(

    int CharacterId,

    int ItemId

);