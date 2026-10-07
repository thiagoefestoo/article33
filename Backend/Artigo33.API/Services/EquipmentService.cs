using Microsoft.EntityFrameworkCore;

using Artigo33.API.Data;
using Artigo33.API.Models;



namespace Artigo33.API.Services;



public class EquipmentService

{

    private readonly AppDbContext _db;



    public EquipmentService(AppDbContext db)

    {

        _db = db;

    }









    // =====================================
    // EQUIPAR ITEM
    // =====================================


    public async Task<CharacterEquipment?> EquipItem(

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

            return null;









        var character = await _db.Characters

            .FirstOrDefaultAsync(x =>

                x.Id == characterId

            );





        if(character == null)

            return null;









        var item = inventory.Item;









        // =====================================
        // VERIFICA SE ITEM É EQUIPÁVEL
        // =====================================


        if(!item.IsEquipment)

            return null;









        // =====================================
        // SLOT DO ITEM
        // =====================================


        string slot = item.EquipmentSlot ?? "Weapon";









        // =====================================
        // REMOVE EQUIPAMENTO ANTIGO DO SLOT
        // =====================================


        var oldEquipment = await _db.CharacterEquipments

            .Include(x => x.Item)

            .FirstOrDefaultAsync(x =>

                x.CharacterId == characterId &&

                x.Slot == slot &&

                x.Equipped

            );





        if(oldEquipment != null)

        {

            RemoveBonus(

                character,

                oldEquipment.Item!

            );


            oldEquipment.Equipped = false;

        }









        // =====================================
        // APLICA NOVO BÔNUS
        // =====================================


        ApplyBonus(

            character,

            item

        );









        // =====================================
        // CRIA EQUIPAMENTO
        // =====================================


        var equipment = new CharacterEquipment

        {

            CharacterId = characterId,

            ItemId = itemId,

            Slot = slot,

            Equipped = true,

            EquippedAt = DateTime.UtcNow

        };





        _db.CharacterEquipments.Add(equipment);





        inventory.Equipped = true;









        await _db.SaveChangesAsync();









        return equipment;

    }

















    // =====================================
    // DESEQUIPAR ITEM
    // =====================================


    public async Task<CharacterEquipment?> UnequipItem(

        int characterId,

        int itemId

    )

    {


        var equipment = await _db.CharacterEquipments

            .Include(x => x.Item)

            .FirstOrDefaultAsync(x =>

                x.CharacterId == characterId &&

                x.ItemId == itemId &&

                x.Equipped

            );





        if(equipment == null || equipment.Item == null)

            return null;









        var character = await _db.Characters

            .FirstOrDefaultAsync(x =>

                x.Id == characterId

            );





        if(character == null)

            return null;









        RemoveBonus(

            character,

            equipment.Item

        );









        equipment.Equipped = false;









        var inventory = await _db.CharacterInventories

            .FirstOrDefaultAsync(x =>

                x.CharacterId == characterId &&

                x.ItemId == itemId

            );





        if(inventory != null)

            inventory.Equipped = false;









        await _db.SaveChangesAsync();









        return equipment;

    }



















    // =====================================
    // APLICAR ATRIBUTOS
    // =====================================


    private void ApplyBonus(

        Character character,

        Item item

    )

    {


        character.Strength += item.StrengthBonus;


        character.Intelligence += item.IntelligenceBonus;


        character.Agility += item.AgilityBonus;


        character.Health += item.HealthBonus;


        character.Energy += item.EnergyBonus;


        character.Reputation += item.ReputationBonus;

    }















    // =====================================
    // REMOVER ATRIBUTOS
    // =====================================


    private void RemoveBonus(

        Character character,

        Item item

    )

    {


        character.Strength -= item.StrengthBonus;


        character.Intelligence -= item.IntelligenceBonus;


        character.Agility -= item.AgilityBonus;


        character.Health -= item.HealthBonus;


        character.Energy -= item.EnergyBonus;


        character.Reputation -= item.ReputationBonus;









        // Segurança


        if(character.Strength < 0)

            character.Strength = 0;



        if(character.Intelligence < 0)

            character.Intelligence = 0;



        if(character.Agility < 0)

            character.Agility = 0;



        if(character.Health < 1)

            character.Health = 1;



        if(character.Energy < 0)

            character.Energy = 0;



        if(character.Reputation < 0)

            character.Reputation = 0;


    }



}