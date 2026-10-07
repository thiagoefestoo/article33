using Microsoft.EntityFrameworkCore;

using Artigo33.API.Data;
using Artigo33.API.Models;


namespace Artigo33.API.Services;


public class UnityService
{
    private readonly AppDbContext _db;


    public UnityService(AppDbContext db)
    {
        _db = db;
    }


    // =========================================================
    // CARREGAR PERSONAGEM POR CHARACTER ID
    // =========================================================

    public async Task<UnityPlayerResponse?> LoadPlayerByCharacterId(
        int characterId
    )
    {
        // =====================================================
        // BUSCAR PERSONAGEM
        // =====================================================

        var character = await _db.Characters
            .FirstOrDefaultAsync(
                x => x.Id == characterId
            );


        if (character == null)
        {
            return null;
        }


        // =====================================================
        // INVENTÁRIO
        // =====================================================

        var inventory = await _db.CharacterInventories
            .Include(x => x.Item)
            .Where(
                x => x.CharacterId == character.Id
            )
            .Select(
                x => new UnityInventoryResponse
                {
                    ItemId = x.ItemId,

                    Name = x.Item != null
                        ? x.Item.Name
                        : "",

                    Type = x.Item != null
                        ? x.Item.Type
                        : "",

                    Rarity = x.Item != null
                        ? x.Item.Rarity
                        : ""
                }
            )
            .ToListAsync();


        // =====================================================
        // EQUIPAMENTOS
        // =====================================================

        var equipment = await _db.CharacterEquipments
            .Include(x => x.Item)
            .Where(
                x =>
                    x.CharacterId == character.Id &&
                    x.Equipped
            )
            .Select(
                x => new UnityEquipmentResponse
                {
                    ItemId = x.ItemId,

                    Slot = x.Slot,

                    Name = x.Item != null
                        ? x.Item.Name
                        : ""
                }
            )
            .ToListAsync();


        // =====================================================
        // RESPOSTA UNITY
        // =====================================================

        return new UnityPlayerResponse
        {
            CharacterId = character.Id,

            Name = character.Name,

            Level = character.Level,

            Experience = character.Experience,

            Money = character.Money,

            Reputation = character.Reputation,


            Stats = new PlayerStatsResponse
            {
                CurrentHealth =
                    character.CurrentHealth,

                MaxHealth =
                    character.Health,

                Energy =
                    character.Energy,

                MaxEnergy =
                    character.Energy,

                Strength =
                    character.Strength,

                Agility =
                    character.Agility,

                Intelligence =
                    character.Intelligence,

                Accuracy =
                    character.Accuracy,

                Vitality =
                    character.Vitality,

                Charisma =
                    character.Charisma
            },


            Inventory = inventory,

            Equipment = equipment
        };
    }


    // =========================================================
    // MÉTODO ANTIGO
    // MANTIDO TEMPORARIAMENTE PARA COMPATIBILIDADE
    // =========================================================

    public async Task<UnityPlayerResponse?> LoadPlayer(
        int userId
    )
    {
        var character = await _db.Characters
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.Id)
            .FirstOrDefaultAsync();


        if (character == null)
        {
            return null;
        }


        return await LoadPlayerByCharacterId(
            character.Id
        );
    }
}