using Microsoft.AspNetCore.Mvc;

using Artigo33.API.Services;



namespace Artigo33.API.Controllers;



[ApiController]

[Route("api/shop")]

public class ShopController : ControllerBase

{


    private readonly ShopService _shopService;



    public ShopController(ShopService shopService)

    {
        _shopService = shopService;
    }








    // =====================================
    // LISTAR LOJA
    // =====================================


    [HttpGet("items")]

    public async Task<IActionResult> Items()

    {

        var items = await _shopService.GetShopItems();


        return Ok(new

        {

            total = items.Count,

            items

        });

    }









    // =====================================
    // COMPRAR
    // =====================================


    [HttpPost("buy")]

    public async Task<IActionResult> Buy(

        BuyRequest request

    )

    {


        var result = await _shopService.BuyItem(

            request.CharacterId,

            request.ItemId

        );




        if(!result)

            return BadRequest(new

            {

                message="Não foi possível comprar o item"

            });





        return Ok(new

        {

            message="Item comprado com sucesso"

        });

    }









    // =====================================
    // VENDER
    // =====================================


    [HttpPost("sell")]

    public async Task<IActionResult> Sell(

        SellRequest request

    )

    {



        var result = await _shopService.SellItem(

            request.CharacterId,

            request.ItemId

        );




        if(!result)

            return BadRequest(new

            {

                message="Não foi possível vender o item"

            });





        return Ok(new

        {

            message="Item vendido com sucesso"

        });


    }



}







public record BuyRequest(

    int CharacterId,

    int ItemId

);





public record SellRequest(

    int CharacterId,

    int ItemId

);