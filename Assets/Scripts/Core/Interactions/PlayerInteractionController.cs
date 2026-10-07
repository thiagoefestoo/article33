using UnityEngine;


public class PlayerInteractionController : MonoBehaviour
{


    public float InteractionDistance = 3f;


    public Camera PlayerCamera;



    void Update()
    {


        if (Input.GetKeyDown(KeyCode.E))
        {

            Interact();

        }


    }





    void Interact()
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
                InteractionDistance
            )
        )
        {


            Debug.Log(
                "Interagindo com: "
                +
                hit.collider.name
            );


        }


    }



}