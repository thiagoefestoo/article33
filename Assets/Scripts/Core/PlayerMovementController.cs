using FishNet.Object;
using UnityEngine;

public class PlayerMovementController : NetworkBehaviour
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

    private bool networkModeActive = false;

    // ===============================
    // START LOCAL / LEGADO
    // ===============================

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        /*
         * Enquanto o personagem ainda for criado pelo
         * PlayerSpawner antigo usando Instantiate(),
         * ele continua funcionando normalmente.
         */
        if (!IsSpawned)
        {
            networkModeActive = false;

            EnableLocalControl();

            Debug.Log(
                "[MOVEMENT] Modo local temporário ativo: "
                + gameObject.name
            );
        }
    }

    // ===============================
    // START FISHNET
    // ===============================

    public override void OnStartClient()
    {
        base.OnStartClient();

        networkModeActive = true;

        if (controller == null)
        {
            controller =
                GetComponent<CharacterController>();
        }

        if (!IsOwner)
        {
            Debug.Log(
                "[MOVEMENT] Jogador remoto detectado. "
                + "Input local bloqueado: "
                + gameObject.name
            );

            return;
        }

        EnableLocalControl();

        Debug.Log(
            "[MOVEMENT] Jogador FishNet local habilitado: "
            + gameObject.name
        );
    }

    // ===============================
    // CONTROLE LOCAL
    // ===============================

    private void EnableLocalControl()
    {
        Cursor.lockState =
            CursorLockMode.Locked;

        Cursor.visible = false;
    }

    // ===============================
    // PODE CONTROLAR?
    // ===============================

    private bool CanControl()
    {
        /*
         * Ainda não foi spawnado pelo FishNet:
         * mantém funcionamento atual.
         */
        if (!networkModeActive)
        {
            return true;
        }

        /*
         * Quando FishNet assumir:
         * somente o owner controla.
         */
        return IsOwner;
    }

    // ===============================
    // UPDATE
    // ===============================

    private void Update()
    {
        if (!CanControl())
            return;

        MovePlayer();
        ApplyGravity();
        CameraLook();
    }

    // ===============================
    // MOVIMENTO
    // ===============================

    private void MovePlayer()
    {
        if (controller == null)
            return;

        float x =
            Input.GetAxis("Horizontal");

        float z =
            Input.GetAxis("Vertical");

        Vector3 move =
            transform.right * x +
            transform.forward * z;

        float speed =
            Input.GetKey(KeyCode.LeftShift)
                ? RunSpeed
                : WalkSpeed;

        controller.Move(
            move *
            speed *
            Time.deltaTime
        );
    }

    // ===============================
    // GRAVIDADE + PULO
    // ===============================

    private void ApplyGravity()
    {
        if (controller == null)
            return;

        bool grounded =
            controller.isGrounded;

        if (
            grounded &&
            velocity.y < 0
        )
        {
            velocity.y = -2f;
        }

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
                "[MOVEMENT] Pulo executado."
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

    private void CameraLook()
    {
        if (CameraTransform == null)
            return;

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

        transform.Rotate(
            Vector3.up *
            mouseX
        );

        cameraRotation -=
            mouseY;

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