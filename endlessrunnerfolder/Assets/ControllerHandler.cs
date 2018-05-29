using UnityEngine;

using System.Collections;

public class ControllerHandler : MonoBehaviour

{

    private Vector3 g_vec3MovementVector;
    private Vector3 g_vec3DirectionalVector;

    private CharacterController characterController;

    private float g_fMovementSpeed = 32;

    private float g_fJumpPower = 7 * 2.5f;

    private float g_fJumpTimer = 1.5f;

    private float g_fTimer = 1.5f;

    private float g_fCharacterDirection;

    private float gravity = 20;

    private bool[] g_bPlayerJumping;

    public bool g_bFirstPersonCamera;

    public Camera First_Person_Camera;

    public Camera Third_Person_Camera;

    public AudioSource m_sSiren;

    public AudioSource m_sBackgroundSound;

    public int joystickNumber;

    string[] g_sarrControllerID;

    void Start()

    {
        // Jumping bool setup
        g_bPlayerJumping = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            g_bPlayerJumping[i] = false;
        }
        //First_Person_Camera.gameObject.SetActive(false);
        //Third_Person_Camera.gameObject.SetActive(true);
        g_bFirstPersonCamera = false;

        //Player-Controller Allocation Initialization
        characterController = GetComponent<CharacterController>();
        //g_fCharacterDirection = characterController.transform.
        g_sarrControllerID = Input.GetJoystickNames();
        if (Input.GetJoystickNames().Length > 0)
        {
            print("Controller Connected");
            print(g_sarrControllerID[0]);
        }

    }

    void PushPlayerMovement(Vector3 _newPos)
    {
        characterController.Move(_newPos * Time.deltaTime);
        return;
    }

    void Update()

    {
        string joystickString = joystickNumber.ToString();

        /*if (Input.GetButtonDown("RightJoystickC_P"))
        {
            switch (g_bFirstPersonCamera)
            {
                case true:
                    First_Person_Camera.gameObject.SetActive(false);
                    Third_Person_Camera.gameObject.SetActive(true);
                    g_bFirstPersonCamera = false;
                    break;
                case false: // switch to first person
                    First_Person_Camera.gameObject.SetActive(true);
                    Third_Person_Camera.gameObject.SetActive(false);
                    g_bFirstPersonCamera = true;
                    break;
            }
        }*/

        if (!g_bPlayerJumping[3] && (Input.GetAxis("RightJoystickY_P") > 0.19 || Input.GetAxis("RightJoystickY_P") < 0.19)) // stops the player's x movement whilst in the air.
        {
            g_vec3MovementVector.x = Input.GetAxis("RightJoystickX_P") * g_fMovementSpeed;
            PushPlayerMovement(g_vec3MovementVector);
        }
        else
        {
            g_vec3MovementVector.x = Input.GetAxis("RightJoystickX_P") * (g_fMovementSpeed / 3);
            PushPlayerMovement(g_vec3MovementVector);
        }

        if (Input.GetAxis("RightJoystickY_P") > 0.19 || Input.GetAxis("RightJoystickY_P") < 0.19)
        {
            if (Input.GetAxis("RightJoystickY_P") > 0.7 && !g_bPlayerJumping[3]) // NOLLIE POSITIVE MOVEMENT
            {
                switch (g_bPlayerJumping[0])
                {
                    case true:
                        {
                            if (g_bPlayerJumping[2])
                            {
                                g_vec3MovementVector.y = g_fJumpPower;
                                g_bPlayerJumping[3] = true;
                            }
                            g_bPlayerJumping[0] = false;
                            break;
                        }
                    case false:
                        {
                            g_bPlayerJumping[0] = true;
                            g_bPlayerJumping[1] = true; // Player Nollie
                            break;
                        }
                }
            }

            if (Input.GetAxis("RightJoystickY_P") < 0.7 && !g_bPlayerJumping[3]) // NOLLIE POSITIVE MOVEMENT
            {
                switch (g_bPlayerJumping[0])
                {
                    case true:
                        {
                            if (g_bPlayerJumping[1])
                            {
                                g_vec3MovementVector.y = g_fJumpPower;
                                g_bPlayerJumping[3] = true;
                            }
                            g_bPlayerJumping[0] = false;
                            break;
                        }
                    case false:
                        {
                            g_bPlayerJumping[0] = true;
                            g_bPlayerJumping[2] = true; // Player Nollie
                            break;
                        }
                }
                characterController.Move(g_vec3MovementVector * Time.deltaTime);
            }
            PushPlayerMovement(g_vec3MovementVector);
        }

        if (characterController.isGrounded == true && g_bPlayerJumping[3])
        {
            for (int i = 0; i < 4; i++)
            {
                g_bPlayerJumping[i] = false;

            }
        }

        g_vec3MovementVector.y -= gravity * Time.deltaTime;
    }
  
}