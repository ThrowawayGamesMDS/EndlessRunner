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

    private float m_rotationSensitivity;

    public bool g_bFirstPersonCamera;

    public Camera First_Person_Camera;

    public Camera Third_Person_Camera;

    public AudioSource m_sSiren;

    public AudioSource m_sBackgroundSound;

    public int joystickNumber;

    string[] g_sarrControllerID;

    Animator anim;


    /***
     * 
     * 
     * 
     * g_bPlayerJumping[0] = INITIALIZE JUMP
     * g_bPlayerJumping[1] = OLLIE ATTEMPT
     * g_bPlayerJumping[2] = NOLLIE ATTEMPT
     * g_bPlayerJumping[3] = JUMPING G
     * g_bPlayerJumping[4] = TRICKING (IN EZY MODE)
     * 
     * 
     * 
     ***/

    void Start()

    {
        // Jumping bool setup
        g_bPlayerJumping = new bool[5];
        for (int i = 0; i < 5; i++)
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

        m_rotationSensitivity = 30.5f;

        if (menucontroller.IsPlayingHard() == false)
        {
           anim = GetComponent<Animator>();
        }

    }

    void PushPlayerMovement(Vector3 _newPos)
    {
        characterController.Move(_newPos * Time.deltaTime);
        return;
    }

    void Update()

    {

        if (!g_bPlayerJumping[3] && (Input.GetAxis("RightJoystickX_P") > 0.19 || Input.GetAxis("RightJoystickX_P") < 0.19)) // stops the player's x movement whilst in the air.
        {
            g_vec3MovementVector.x = Input.GetAxis("RightJoystickX_P") * g_fMovementSpeed;
            PushPlayerMovement(g_vec3MovementVector);
        }
        else
        {
            g_vec3MovementVector.x = Input.GetAxis("RightJoystickX_P") * (g_fMovementSpeed / 3);
            PushPlayerMovement(g_vec3MovementVector);
        }

        if (Input.GetAxis("LeftJoystickY_P") > 0.19 || Input.GetAxis("LeftJoystickY_P") < 0.19) // player jumping
        {
            if (Input.GetAxis("LeftJoystickY_P") > 0.7 && !g_bPlayerJumping[3]) 
            {
                switch (g_bPlayerJumping[0])
                {
                    case true:
                        {
                            if (g_bPlayerJumping[2]) // Nollie
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

            if (Input.GetAxis("LeftJoystickY_P") < 0.7 && !g_bPlayerJumping[3]) 
            {
                switch (g_bPlayerJumping[0])
                {
                    case true:
                        {
                            if (g_bPlayerJumping[1]) // Ollie
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

        if (menucontroller.IsPlayingHard() == false && g_bPlayerJumping[3] && !g_bPlayerJumping[4]) // PLAYING EZY
        {
        
            if (Input.GetAxis("LeftJoystickX_P") < 0.19)
            {
                print("EZY TRYNA TRICK1");
                if (Input.GetAxis("LeftJoystickX_P") < 0.19)
                {
                    //trey flip
                    // var animator = this.GetComponent<Animator>();
                    anim.SetTrigger("isFlipping");
                    print("HERE1");
                }
                else
                {
                    // anim.Play("horizontal_flip");
                    //kick flip
                }
                g_bPlayerJumping[4] = true;
            }
            else if (Input.GetAxis("LeftJoystickX_P") > 0.19)
            {
                print("EZY TRYNA TRICK2");
                if (Input.GetAxis("LeftJoystickX_P") > 0.19)
                {
                    //inverse trey flip
                    //anim.Play("trey_flip");
                    //var animator = this.GetComponent<Animator>();
                    anim.SetTrigger("isFlipping");
                    print("HERE2");
                }
                else
                {
                    //inverse kick flip
                    // anim.Play("horizontal_flip");
                }
                g_bPlayerJumping[4] = true;
            }
            // run animations
        }
        else // HARD AS FUCK
        {
            if (Input.GetAxis("LeftJoystickX_P") < 0.19 || Input.GetAxis("LeftJoystickX_P") > 0.19)
            {
                var vec3 = new Vector3(0, 0, (Input.GetAxis("LeftJoystickX_P")) * m_rotationSensitivity);
                this.transform.Rotate(vec3);
            }
        }

        /*
        switch (menucontroller.IsPlayingHard() && g_bPlayerJumping[3] && g_bPlayerJumping[2] || g_bPlayerJumping[1]) // could set to a variable but cbf wasting mem
        {
            case true:
                {
                    print("WEGOTHERE1");
                    if (Input.GetAxis("LeftJoystickX_P") < 0.19 || Input.GetAxis("LeftJoystickX_P") > 0.19)
                    {
                        var vec3 = new Vector3(0, 0, (Input.GetAxis("LeftJoystickX_P")) * m_rotationSensitivity);
                        this.transform.Rotate(vec3);
                    }
                    break;
                }
            case false:
                {
                    print("WEGOTHERE2");
                    if (Input.GetAxis("LeftJoystickX_P") < 0.19)
                    {
                        if (Input.GetAxis("LeftJoystickY_P") < 0.19)
                        {
                            //trey flip
                            anim.Play("trey_flip");
                        }
                        else
                        {
                           // anim.Play("horizontal_flip");
                            //kick flip
                        }
                    }
                    else if (Input.GetAxis("LeftJoystickX_P") > 0.19)
                    {
                        if (Input.GetAxis("LeftJoystickY_P") > 0.19)
                        {
                            //inverse trey flip
                            anim.Play("trey_flip");
                        }
                        else
                        {
                            //inverse kick flip
                           // anim.Play("horizontal_flip");
                        }
                    }
                    // run animations
                    break;
                }
        }*/

        if (characterController.isGrounded == true && g_bPlayerJumping[3])
        {
            for (int i = 0; i < 5; i++)
            {
                g_bPlayerJumping[i] = false;

            }
        }

        g_vec3MovementVector.y -= gravity * Time.deltaTime;
    }
  
}