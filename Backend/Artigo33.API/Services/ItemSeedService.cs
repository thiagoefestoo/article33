using Artigo33.API.Data;
using Artigo33.API.Models;


namespace Artigo33.API.Services;



public class ItemSeedService

{

    private readonly AppDbContext _db;



    public ItemSeedService(AppDbContext db)

    {
        _db = db;
    }







    public void Seed()

    {


        // Evita duplicação

        if(_db.Items.Any())

            return;









        var items = new List<Item>

        {

            // =====================================
            // ITENS COMUNS
            // =====================================



            new Item

            {

                Name = "Celular Roubado",

                Description =
                "Um celular obtido de forma ilegal. Pode ser vendido.",


                Type = "Material",


                Rarity = "Common",


                BuyPrice = 0,


                SellPrice = 150,


                Weight = 0.2

            },







            new Item

            {

                Name = "Pistola Velha",


                Description =
                "Uma arma antiga com baixo poder de fogo.",


                Type = "Weapon",


                Rarity = "Common",


                Attack = 5,


                StrengthBonus = 2,


                EquipmentSlot = "Weapon",


                IsEquipment = true,


                BuyPrice = 500,


                SellPrice = 250,


                Weight = 1.5

            },







            new Item

            {

                Name = "Dinheiro Sujo",


                Description =
                "Dinheiro de origem desconhecida.",


                Type = "Material",


                Rarity = "Common",


                SellPrice = 300,


                Weight = 0.1

            },









            // =====================================
            // EQUIPAMENTOS
            // =====================================



            new Item

            {

                Name = "Colete Tático",


                Description =
                "Colete utilizado para proteção em combate.",


                Type = "Armor",


                Rarity = "Rare",


                Defense = 20,


                VitalityBonus = 5,


                HealthBonus = 50,


                EquipmentSlot = "Armor",


                IsEquipment = true,


                BuyPrice = 2000,


                SellPrice = 1000,


                Weight = 5

            },









            new Item

            {

                Name = "Pistola 9mm",


                Description =
                "Arma padrão de combate.",


                Type = "Weapon",


                Rarity = "Rare",


                Attack = 20,


                StrengthBonus = 8,


                AccuracyBonus = 5,


                EquipmentSlot = "Weapon",


                IsEquipment = true,


                BuyPrice = 3500,


                SellPrice = 1800,


                Weight = 2

            },









            new Item

            {

                Name = "Munição",


                Description =
                "Pacote de munição para armas.",


                Type = "Material",


                Rarity = "Common",


                SellPrice = 100,


                Weight = 1

            },









            // =====================================
            // ITENS RAROS
            // =====================================



            new Item

            {

                Name = "Arma Personalizada",


                Description =
                "Arma modificada com alto desempenho.",


                Type = "Weapon",


                Rarity = "Epic",


                Attack = 60,


                StrengthBonus = 20,


                AccuracyBonus = 15,


                EquipmentSlot = "Weapon",


                IsEquipment = true,


                BuyPrice = 15000,


                SellPrice = 8000,


                Weight = 2.5

            },









            new Item

            {

                Name = "Documento Secreto",


                Description =
                "Documento valioso ligado a uma investigação.",


                Type = "Quest",


                Rarity = "Legendary",


                ReputationBonus = 100,


                SellPrice = 5000,


                Weight = 0.1

            }

        };









        _db.Items.AddRange(items);



        _db.SaveChanges();



    }


}