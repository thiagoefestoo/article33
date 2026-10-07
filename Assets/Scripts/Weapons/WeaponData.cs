using System;



[Serializable]
public class WeaponData
{


    // =========================================================
    // IDENTIDADE
    // =========================================================

    public int Id;


    public string Name;


    public string Category;



    // =========================================================
    // COMBATE
    // =========================================================

    public int Damage;


    public int FireRate;


    public int Range;


    public int Accuracy;



    // =========================================================
    // MUNIÇÃO
    // =========================================================

    public int MagazineSize;


    public int CurrentAmmo;


    public int ReserveAmmo;



    // =========================================================
    // ECONOMIA
    // =========================================================

    public int BuyPrice;


    public int SellPrice;



    // =========================================================
    // PROGRESSÃO
    // =========================================================

    public int RequiredLevel;


    public bool IsIllegal;



    // =========================================================
    // ESTADO
    // =========================================================

    public bool Equipped;



    // =========================================================
    // FUNÇÕES
    // =========================================================


    public bool CanShoot()
    {

        return CurrentAmmo > 0;

    }



    public bool Shoot()
    {

        if (CurrentAmmo <= 0)
            return false;


        CurrentAmmo--;


        return true;

    }



    public void Reload()
    {


        int needed =
            MagazineSize - CurrentAmmo;


        if (ReserveAmmo <= 0)
            return;



        int amount =
            Math.Min(
                needed,
                ReserveAmmo
            );



        CurrentAmmo += amount;


        ReserveAmmo -= amount;


    }



    public void Equip()
    {

        Equipped = true;

    }



    public void Unequip()
    {

        Equipped = false;

    }



}