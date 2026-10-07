using Microsoft.EntityFrameworkCore;

using Artigo33.API.Data;

using Artigo33.API.Models;



namespace Artigo33.API.Services;



public class ProfileService
{

    private readonly AppDbContext _db;



    public ProfileService(AppDbContext db)
    {
        _db = db;
    }







    // =====================================
    // BUSCAR PERFIL COMPLETO
    // =====================================


    public async Task<PlayerProfile?> GetProfile(
        int characterId
    )
    {


        var character = await _db.Characters

            .Include(x => x.CharacterClass)

            .Include(x => x.Rank)

            .FirstOrDefaultAsync(
                x => x.Id == characterId
            );




        if(character == null)
            return null;








        var equipments = await _db.CharacterEquipments

            .Include(x => x.Item)

            .Where(x =>

                x.CharacterId == characterId &&

                x.Equipped

            )

            .Select(x => new PlayerEquipmentProfile

            {

                ItemId = x.Item!.Id,


                Name = x.Item.Name,


                Type = x.Item.Type,


                Rarity = x.Item.Rarity,


                Attack = x.Item.Attack,


                Defense = x.Item.Defense,


                StrengthBonus = x.Item.StrengthBonus,


                IntelligenceBonus = x.Item.IntelligenceBonus,


                AgilityBonus = x.Item.AgilityBonus,


                HealthBonus = x.Item.HealthBonus,


                EnergyBonus = x.Item.EnergyBonus


            })

            .ToListAsync();









        var profile = new PlayerProfile

        {

            CharacterId = character.Id,


            Name = character.Name,


            Gender = character.Gender,



            CharacterClass = character.CharacterClass != null

                ? character.CharacterClass.Name

                : "",



            Rank = character.Rank != null

                ? character.Rank.Name

                : "",



            Faction = character.Faction,


            SubClass = character.SubClass,



            Level = character.Level,


            Experience = character.Experience,


            AttributePoints = character.AttributePoints,



            Health = character.Health,


            Energy = character.Energy,



            Money = character.Money,


            Reputation = character.Reputation,



            Strength = character.Strength,


            Agility = character.Agility,


            Intelligence = character.Intelligence,


            Vitality = character.Vitality,


            Accuracy = character.Accuracy,


            Charisma = character.Charisma,



            MissionsCompleted = character.MissionsCompleted,


            Arrests = character.Arrests,


            CrimesCommitted = character.CrimesCommitted,



            Equipment = equipments

        };







        return profile;

    }



}