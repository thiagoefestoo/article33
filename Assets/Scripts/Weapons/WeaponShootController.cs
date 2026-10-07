using UnityEngine;


public class WeaponShootController : MonoBehaviour
{

    public Camera PlayerCamera;


    public float Damage = 25f;


    public float ShootDistance = 100f;



    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {

            Shoot();

        }


    }





    void Shoot()
    {


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
                ShootDistance
            )
        )
        {


            Debug.Log(
                "Acertou: "
                +
                hit.collider.name
            );



            // futuro:
            // aplicar dano
            // sangue
            // impacto
            // multiplayer



        }
        else
        {

            Debug.Log(
                "Disparo sem alvo"
            );

        }


    }


}