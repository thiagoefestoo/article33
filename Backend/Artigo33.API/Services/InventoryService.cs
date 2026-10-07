using Microsoft.EntityFrameworkCore;

using Artigo33.API.Data;
using Artigo33.API.Models;


namespace Artigo33.API.Services;


public class InventoryService
{


    private readonly AppDbContext _db;



    public InventoryService(AppDbContext db)

    {

        _db = db;

    }







    // ==========================================
    // BUSCAR INVENTÁRIO
    // ==========================================


    public async Task<Inventory?> GetInventory(
        int characterId
    )

    {


        var inventory = await _db.Inventories

            .Include(x => x.Character)

            .FirstOrDefaultAsync(
                x => x.CharacterId == characterId
            );



        return inventory;

    }









    // ==========================================
    // CRIAR INVENTÁRIO
    // ==========================================


    public async Task<Inventory> CreateInventory(
        int characterId
    )

    {


        var exists = await _db.Inventories

            .FirstOrDefaultAsync(
                x => x.CharacterId == characterId
            );



        if(exists != null)

            return exists;





        var inventory = new Inventory

        {

            CharacterId = characterId,

            MaxWeight = 50,

            MaxSlots = 30,

            CurrentWeight = 0

        };





        _db.Inventories.Add(inventory);



        await _db.SaveChangesAsync();



        return inventory;

    }









    // ==========================================
    // ADICIONAR ITEM
    // ==========================================


    public async Task<CharacterInventory?> AddItem(

        int characterId,

        int itemId,

        int quantity

    )

    {


        var inventory = await CreateInventory(characterId);





        var item = await _db.Items

            .FirstOrDefaultAsync(
                x => x.Id == itemId
            );



        if(item == null)

            return null;







        // verifica peso

        double totalWeight =
            item.Weight * quantity;





        if(
            inventory.CurrentWeight 
            + totalWeight 
            > inventory.MaxWeight
        )

        {

            return null;

        }









        var existing = await _db.CharacterInventories

            .FirstOrDefaultAsync(x =>

                x.CharacterId == characterId &&

                x.ItemId == itemId

            );







        if(existing != null)

        {

            existing.Quantity += quantity;

        }

        else

        {


            existing = new CharacterInventory

            {

                CharacterId = characterId,

                ItemId = itemId,

                Quantity = quantity

            };



            _db.CharacterInventories.Add(existing);

        }







        inventory.CurrentWeight += totalWeight;





        await _db.SaveChangesAsync();





        return existing;


    }









    // ==========================================
    // REMOVER ITEM
    // ==========================================


    public async Task<bool> RemoveItem(

        int characterId,

        int itemId,

        int quantity

    )

    {



        var inventoryItem = await _db.CharacterInventories

            .Include(x => x.Item)

            .FirstOrDefaultAsync(x =>

                x.CharacterId == characterId &&

                x.ItemId == itemId

            );







        if(inventoryItem == null)

            return false;








        if(inventoryItem.Quantity < quantity)

            return false;









        inventoryItem.Quantity -= quantity;







        var inventory = await _db.Inventories

            .FirstAsync(
                x => x.CharacterId == characterId
            );







        inventory.CurrentWeight -=

            inventoryItem.Item!.Weight * quantity;







        if(inventoryItem.Quantity <= 0)

        {

            _db.CharacterInventories.Remove(
                inventoryItem
            );

        }







        await _db.SaveChangesAsync();



        return true;


    }









    // ==========================================
    // EQUIPAR ITEM
    // ==========================================


    public async Task<bool> EquipItem(

        int characterId,

        int itemId

    )

    {



        var item = await _db.CharacterInventories

            .Include(x => x.Item)

            .FirstOrDefaultAsync(x =>

                x.CharacterId == characterId &&

                x.ItemId == itemId

            );





        if(item == null)

            return false;







        if(item.Equipped)

            return true;









        var character = await _db.Characters

            .FirstOrDefaultAsync(
                x => x.Id == characterId
            );







        if(character == null)

            return false;









        character.Strength += item.Item!.Attack;

        character.Health += item.Item.HealthBonus;

        character.Energy += item.Item.EnergyBonus;







        item.Equipped = true;







        await _db.SaveChangesAsync();





        return true;


    }









    // ==========================================
    // DESEQUIPAR ITEM
    // ==========================================


    public async Task<bool> UnequipItem(

        int characterId,

        int itemId

    )

    {


        var item = await _db.CharacterInventories

            .Include(x => x.Item)

            .FirstOrDefaultAsync(x =>

                x.CharacterId == characterId &&

                x.ItemId == itemId

            );





        if(item == null)

            return false;







        if(!item.Equipped)

            return true;








        var character = await _db.Characters

            .FirstOrDefaultAsync(
                x => x.Id == characterId
            );






        if(character == null)

            return false;









        character.Strength -= item.Item!.Attack;

        character.Health -= item.Item.HealthBonus;

        character.Energy -= item.Item.EnergyBonus;







        item.Equipped = false;







        await _db.SaveChangesAsync();





        return true;


    }



}