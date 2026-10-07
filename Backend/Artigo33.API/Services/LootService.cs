using Microsoft.EntityFrameworkCore;

using Artigo33.API.Data;
using Artigo33.API.Models;


namespace Artigo33.API.Services;


public class LootService

{

    private readonly AppDbContext _db;



    public LootService(AppDbContext db)

    {
        _db = db;
    }







    // =====================================
    // GERAR LOOT
    // =====================================


    public async Task<List<Item>> GenerateLoot(int enemyId)

    {

        var drops = await _db.ItemDrops

            .Include(x => x.Item)

            .Where(x => x.EnemyId == enemyId)

            .ToListAsync();





        var loot = new List<Item>();


        Random random = new Random();





        foreach(var drop in drops)

        {

            double roll = random.NextDouble() * 100;



            if(roll <= drop.DropChance)

            {

                if(drop.Item != null)

                {

                    loot.Add(drop.Item);

                }

            }


        }





        return loot;

    }

}