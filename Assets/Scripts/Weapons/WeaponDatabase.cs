using System.Collections.Generic;



public static class WeaponDatabase
{


    public static List<WeaponData> Weapons =
        new List<WeaponData>()
        {


            // ==========================================
            // GLOCK 17
            // ==========================================

            new WeaponData
            {

                Id = 1,

                Name = "Glock 17",

                Category = "Pistola",

                Damage = 25,

                FireRate = 400,

                Range = 50,

                Accuracy = 85,


                MagazineSize = 17,

                CurrentAmmo = 17,

                ReserveAmmo = 51,


                BuyPrice = 5000,

                SellPrice = 2500,


                RequiredLevel = 1,

                IsIllegal = false,

                Equipped = false

            },




            // ==========================================
            // AK-47
            // ==========================================

            new WeaponData
            {

                Id = 2,

                Name = "AK-47",

                Category = "Fuzil",

                Damage = 45,

                FireRate = 600,

                Range = 150,

                Accuracy = 70,


                MagazineSize = 30,

                CurrentAmmo = 30,

                ReserveAmmo = 120,


                BuyPrice = 30000,

                SellPrice = 15000,


                RequiredLevel = 10,

                IsIllegal = true,

                Equipped = false

            },




            // ==========================================
            // MP5
            // ==========================================

            new WeaponData
            {

                Id = 3,

                Name = "MP5",

                Category = "Submetralhadora",

                Damage = 30,

                FireRate = 800,

                Range = 100,

                Accuracy = 80,


                MagazineSize = 30,

                CurrentAmmo = 30,

                ReserveAmmo = 150,


                BuyPrice = 20000,

                SellPrice = 10000,


                RequiredLevel = 5,

                IsIllegal = true,

                Equipped = false

            }



        };




    public static WeaponData GetWeapon(int id)
    {

        foreach (WeaponData weapon in Weapons)
        {

            if (weapon.Id == id)
                return weapon;

        }


        return null;

    }



}