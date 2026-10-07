using UnityEngine;


public class PlayerMovementController : MonoBehaviour
{


    // ===============================
    // MOVIMENTO
    // ===============================

    public float WalkSpeed = 5f;

    public float RunSpeed = 9f;


    public float JumpForce = 3f;


    public float Gravity = -20f;



    // ===============================
    // CAMERA
    // ===============================

    public Transform CameraTransform;

    public float MouseSensitivity = 200f;



    // ===============================
    // PRIVADOS
    // ===============================

    private CharacterController controller;


    private Vector3 velocity;


    private float cameraRotation = 0f;



    // ===============================
    // INICIO
    // ===============================

    void Start()
    {

        controller =
            GetComponent<CharacterController>();


        Cursor.lockState =
            CursorLockMode.Locked;


    }





    // ===============================
    // UPDATE
    // ===============================

    void Update()
    {

        MovePlayer();

        ApplyGravity();

        CameraLook();

    }





    // ===============================
    // MOVIMENTO PLAYER
    // ===============================

    void MovePlayer()
    {


        float x =
            Input.GetAxis("Horizontal");


        float z =
            Input.GetAxis("Vertical");



        Vector3 move =
            transform.right * x +
            transform.forward * z;



        float speed =
            Input.GetKey(KeyCode.LeftShift)
            ?
            RunSpeed
            :
            WalkSpeed;



        controller.Move(
            move *
            speed *
            Time.deltaTime
        );


    }





    // ===============================
    // GRAVIDADE + PULO
    // ===============================

    void ApplyGravity()
    {


        bool grounded =
            controller.isGrounded;



        if (grounded && velocity.y < 0)
        {

            velocity.y = -2f;

        }




        // ESPAÇO = PULAR

        if (
            grounded &&
            Input.GetKeyDown(KeyCode.Space)
        )
        {

            velocity.y =
                Mathf.Sqrt(
                    JumpForce *
                    -2f *
                    Gravity
                );


            Debug.Log(
                "Pulo executado"
            );

        }




        velocity.y +=
            Gravity *
            Time.deltaTime;



        controller.Move(
            velocity *
            Time.deltaTime
        );


    }





    // ===============================
    // CAMERA
    // ===============================

    void CameraLook()
    {


        float mouseX =
            Input.GetAxis("Mouse X")
            *
            MouseSensitivity
            *
            Time.deltaTime;



        float mouseY =
            Input.GetAxis("Mouse Y")
            *
            MouseSensitivity
            *
            Time.deltaTime;




        // ROTACIONA PLAYER

        transform.Rotate(
            Vector3.up *
            mouseX
        );




        // ROTACIONA CAMERA

        cameraRotation -= mouseY;



        cameraRotation =
            Mathf.Clamp(
                cameraRotation,
                -80f,
                80f
            );



        CameraTransform.localRotation =
            Quaternion.Euler(
                cameraRotation,
                0,
                0
            );


    }


}