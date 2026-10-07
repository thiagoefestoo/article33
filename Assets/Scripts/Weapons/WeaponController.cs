using UnityEngine;



public class WeaponController : MonoBehaviour
{


    public static WeaponController Instance;



    // =========================================================
    // ARMA ATUAL
    // =========================================================

    public WeaponData CurrentWeapon;



    // =========================================================
    // CONFIGURAÇÃO DO TIRO
    // =========================================================

    public Camera PlayerCamera;


    public float Range = 100f;




    // =========================================================
    // INICIALIZAÇÃO
    // =========================================================

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {

            Destroy(gameObject);
            return;

        }


        Instance = this;


    }





    // =========================================================
    // DISPARO REAL
    // =========================================================

    public void Shoot()
    {


        if (CurrentWeapon == null)
        {

            Debug.LogWarning(
                "[Weapon] Nenhuma arma equipada."
            );

            return;

        }




        if (!CurrentWeapon.CanShoot())
        {

            Debug.Log(
                "[Weapon] Sem munição."
            );


            return;

        }





        bool fired =
            CurrentWeapon.Shoot();




        if (!fired)
            return;





        Debug.Log(
            "Disparo realizado: "
            +
            CurrentWeapon.Name
        );




        // dispara o Raycast

        FireRay();



    }







    // =========================================================
    // RAYCAST DO TIRO
    // =========================================================

    private void FireRay()
    {


        if (PlayerCamera == null)
        {

            Debug.LogWarning(
                "[Weapon] Camera não configurada."
            );


            return;

        }




        Ray ray =
            new Ray(
                PlayerCamera.transform.position,
                PlayerCamera.transform.forward
            );




        RaycastHit hit;




        if (
            Physics.Raycast(
                ray,
                out hit,
                Range
            )
        )
        {


            Debug.Log(
                "Alvo atingido: "
                +
                hit.collider.name
            );




            DamageReceiver target =
                hit.collider.GetComponent<DamageReceiver>();





            if (target != null)
            {

                target.TakeDamage(
                    CurrentWeapon.Damage
                );


            }



        }
        else
        {

            Debug.Log(
                "Disparo não atingiu nenhum alvo."
            );


        }



    }






    // =========================================================
    // EQUIPAR ARMA
    // =========================================================

    public void EquipWeapon(WeaponData weapon)
    {


        if (weapon == null)
            return;




        CurrentWeapon = weapon;



        CurrentWeapon.Equip();




        Debug.Log(
            "Arma equipada: "
            +
            weapon.Name
        );



    }







    // =========================================================
    // REMOVER ARMA
    // =========================================================

    public void UnequipWeapon()
    {


        if (CurrentWeapon == null)
            return;




        CurrentWeapon.Unequip();




        Debug.Log(
            "Arma guardada."
        );




        CurrentWeapon = null;



    }







    // =========================================================
    // RECARREGAR
    // =========================================================

    public void Reload()
    {


        if (CurrentWeapon == null)
            return;





        CurrentWeapon.Reload();




        Debug.Log(
            "Recarregando "
            +
            CurrentWeapon.Name
        );



    }




}