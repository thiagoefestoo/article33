using UnityEngine;



public class WeaponTestController : MonoBehaviour
{


    private WeaponController weaponController;



    private void Start()
    {

        weaponController =
            WeaponController.Instance;


        if (weaponController == null)
        {

            Debug.LogError(
                "[WeaponTest] WeaponController não encontrado."
            );

        }


    }




    private void Update()
    {


        if (weaponController == null)
            return;



        // =====================================
        // EQUIPAR ARMAS
        // =====================================


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            EquipWeapon(1);

        }



        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            EquipWeapon(2);

        }



        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            EquipWeapon(3);

        }




        // =====================================
        // DISPARO
        // =====================================


        if (Input.GetMouseButton(0))
        {

            weaponController.Shoot();

        }




        // =====================================
        // RECARGA
        // =====================================


        if (Input.GetKeyDown(KeyCode.R))
        {

            weaponController.Reload();

        }



    }




    private void EquipWeapon(int id)
    {


        WeaponData weapon =
            WeaponDatabase.GetWeapon(id);



        if (weapon == null)
        {

            Debug.LogWarning(
                "Arma não encontrada."
            );

            return;

        }



        weaponController.EquipWeapon(
            weapon
        );



    }



}