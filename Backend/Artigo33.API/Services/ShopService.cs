using Microsoft.EntityFrameworkCore;

using Artigo33.API.Data;

using Artigo33.API.Models;



namespace Artigo33.API.Services;



public class ShopService
{


    private readonly AppDbContext _db;



    public ShopService(AppDbContext db)

    {
        _db = db;
    }






    // =====================================
    // LISTAR ITENS DA LOJA
    // =====================================


    public async Task<List<Item>> GetShopItems()

    {

        return await _db.Items

            .Where(x => x.Active)

            .OrderBy(x => x.Rarity)

            .ToListAsync();

    }








    // =====================================
    // COMPRAR ITEM
    // =====================================


    public async Task<bool> BuyItem(

        int characterId,

        int itemId

    )

    {



        var character = await _db.Characters

            .FirstOrDefaultAsync(x => x.Id == characterId);




        var item = await _db.Items

            .FirstOrDefaultAsync(x => x.Id == itemId);





        if(character == null || item == null)

            return false;







        if(character.Money < item.BuyPrice)

            return false;








        character.Money -= item.BuyPrice;






        var inventory = new CharacterInventory

        {

            CharacterId = characterId,

            ItemId = itemId,

            Quantity = 1,

            Equipped = false

        };





        _db.CharacterInventories.Add(inventory);







        _db.ShopTransactions.Add(

            new ShopTransaction

            {

                CharacterId = characterId,

                ItemId = itemId,

                Type = "Buy",

                Value = item.BuyPrice

            }

        );







        await _db.SaveChangesAsync();




        return true;

    }













    // =====================================
    // VENDER ITEM
    // =====================================



    public async Task<bool> SellItem(

        int characterId,

        int itemId

    )

    {



        var inventory = await _db.CharacterInventories

            .Include(x => x.Item)

            .FirstOrDefaultAsync(x =>

                x.CharacterId == characterId &&

                x.ItemId == itemId

            );





        if(inventory == null || inventory.Item == null)

            return false;







        var character = await _db.Characters

            .FirstOrDefaultAsync(x => x.Id == characterId);






        if(character == null)

            return false;







        character.Money += inventory.Item.SellPrice;






        _db.CharacterInventories.Remove(inventory);







        _db.ShopTransactions.Add(

            new ShopTransaction

            {

                CharacterId = characterId,

                ItemId = itemId,

                Type = "Sell",

                Value = inventory.Item.SellPrice

            }

        );







        await _db.SaveChangesAsync();




        return true;

    }






}