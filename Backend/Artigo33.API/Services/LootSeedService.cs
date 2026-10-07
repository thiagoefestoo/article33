using Artigo33.API.Data;
using Artigo33.API.Models;


namespace Artigo33.API.Services;


public class LootSeedService

{

    private readonly AppDbContext _db;


    public LootSeedService(AppDbContext db)

    {
        _db = db;
    }





    public void Seed()

    {


        // Evita duplicação

        if(_db.ItemDrops.Any())

            return;






        var drops = new List<ItemDrop>

        {

            // =====================================
            // SUSPEITO PROCURADO
            // =====================================


            new ItemDrop

            {

                EnemyId = 1,

                ItemId = 1,

                DropChance = 50,

                Quantity = 1

            },


            new ItemDrop

            {

                EnemyId = 1,

                ItemId = 2,

                DropChance = 15,

                Quantity = 1

            },



            new ItemDrop

            {

                EnemyId = 1,

                ItemId = 3,

                DropChance = 80,

                Quantity = 1

            },







            // =====================================
            // CRIMINOSO ARMADO
            // =====================================


            new ItemDrop

            {

                EnemyId = 2,

                ItemId = 4,

                DropChance = 30,

                Quantity = 1

            },


            new ItemDrop

            {

                EnemyId = 2,

                ItemId = 5,

                DropChance = 40,

                Quantity = 1

            },


            new ItemDrop

            {

                EnemyId = 2,

                ItemId = 6,

                DropChance = 70,

                Quantity = 3

            },







            // =====================================
            // CHEFE DA ORGANIZAÇÃO
            // =====================================


            new ItemDrop

            {

                EnemyId = 5,

                ItemId = 7,

                DropChance = 10,

                Quantity = 1

            },


            new ItemDrop

            {

                EnemyId = 5,

                ItemId = 8,

                DropChance = 5,

                Quantity = 1

            }


        };







        _db.ItemDrops.AddRange(drops);


        _db.SaveChanges();


    }


}