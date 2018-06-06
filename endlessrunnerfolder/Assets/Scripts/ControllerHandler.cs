using UnityEngine;

using System.Collections;

public class ControllerHandler : MonoBehaviour

{

    private Vector3 g_vec3MovementVector;
    private Vector3 g_vec3DirectionalVector;

    private CharacterController characterController;

    private int m_iBoostersPerJump;

    private float g_fMovementSpeed;

    private float g_fJumpPower;

    private float g_fJumpTimer;

    private float g_fTimer;

    private float g_fCharacterDirection;

    private float gravity;

    private bool m_bScreenShakeActive;

    public static bool m_bIsPaused; // could do with a PlayerHandler class..

    public static bool m_bPlayerIsAlive; // could do with a PlayerHandler class..

    private bool[] g_bPlayerJumping;

    private float m_rotationSensitivity;

    public bool g_bFirstPersonCamera;

    public static bool m_bSetScreenShake;

    public Camera First_Person_Camera;

    public Camera Third_Person_Camera;

    public AudioSource m_sSiren;

    public AudioSource m_sBackgroundSound;

    public int joystickNumber;

    string[] g_sarrControllerID;

    public GameObject[] m_goWheels;

    Animator anim;

    [SerializeField] private AudioClip m_acBoosterClip;

    [SerializeField] private AudioClip m_acCrashClip;

    [SerializeField] private AudioClip m_acJumpClip;

    private AudioSource m_asPlayAudio;


    /***
     * 
     * 
     * 
     * g_bPlayerJumping[0] = INITIALIZE JUMP
     * g_bPlayerJumping[1] = OLLIE ATTEMPT
     * g_bPlayerJumping[2] = NOLLIE ATTEMPT
     * g_bPlayerJumping[3] = JUMPING G
     * g_bPlayerJumping[4] = TRICKING (IN EZY MODE)
     * g_bPlayerJumping[5] = noob val for prejump
     * g_bPlayerJumping[6] = ollie set
     * g_bPlayerJumping[7] = nollie set
     * 
     * 
     * 
     ***/

    void Start()

    {
        // Jumping bool setup
        g_bPlayerJumping = new bool[8];
        for (int i = 0; i < 8; i++)
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

        g_fMovementSpeed = 32;

        g_fJumpPower = 7 * 3.5f;

        g_fJumpTimer = 1.5f;

        g_fTimer = 1.5f;

        gravity = 20;

        m_iBoostersPerJump = 3;

        m_bIsPaused = false;

        m_bPlayerIsAlive = true;

        m_bSetScreenShake = false;

        m_bScreenShakeActive = false;

        Third_Person_Camera.GetComponent<cameraChanges>().enabled = false;

        m_asPlayAudio = GetComponent<AudioSource>();

        if (menucontroller.IsPlayingHard() == false)
        {
            anim = GetComponent<Animator>();
        }

    }

    private void PlaySound(string _soundToPlay)
    {
        if (_soundToPlay == "boost")
        {
            m_asPlayAudio.clip = m_acBoosterClip;
        }
        else if (_soundToPlay == "jump")
        {
            m_asPlayAudio.clip = m_acJumpClip;
            //edit
        }
        else if (_soundToPlay == "crash")
        {
            m_asPlayAudio.clip = m_acCrashClip;
        }
        else
        {
            print("ERROR");
        }
        m_asPlayAudio.Play();
    }

    void PushPlayerMovement(Vector3 _newPos)
    {
        characterController.Move(_newPos * Time.deltaTime);
        return;
    }

    void RotateWheels() // COULD USE THE LAYER SYSTEM WITHIN THE ANIMATOR TO DO THIS BUT CEEEEEEEEEEEEEBS SEEMING THIS IS A LOCALLY PLAYED GAME WITH ONLY ONE PLAYER!!!!
    {
        for (int i = 0; i < 4; i++)
        {
            var vec3 = new Vector3(20, 0, 0);
            m_goWheels[i].transform.Rotate(vec3);
        }
    }

    void HandleTricks()
    {
        if (Input.GetAxis("LeftJoystickX_P") < -0.44)
        {
            print("EZY TRYNA TRICK1");
            if (Input.GetAxis("LeftJoystickX_P") < -0.45)
            {
                anim.SetTrigger("invertKickFlipping");
                print("PLAYER IS KICK FLIPPING: " + Input.GetAxis("LeftJoystickX_P") + " || " + Input.GetAxis("LeftJoystickY_P"));
                g_bPlayerJumping[4] = true;
                Invoke("TrickRefresh", 1.0f);
            }
            else if (Input.GetAxis("LeftJoystickX_P") < -0.65 && (Input.GetAxis("LeftJoystickY_P") < -0.19))
            {
                anim.SetTrigger("isFlipping");
                print("PLAYER IS TREY FLIPPING: " + Input.GetAxis("LeftJoystickX_P") + " || " + Input.GetAxis("LeftJoystickY_P"));

                g_bPlayerJumping[4] = true;
                Invoke("TrickRefresh", 1.0f);
            }
        }
        else if (Input.GetAxis("LeftJoystickX_P") > 0.19)
        {
            print("EZY TRYNA TRICK2");
            if (Input.GetAxis("LeftJoystickX_P") > 0.45)
            {
                anim.SetTrigger("kickFlipping");
                g_bPlayerJumping[4] = true;
                Invoke("TrickRefresh", 0.3f);
            }
            else if (Input.GetAxis("LeftJoystickX_P") < 0.65 && (Input.GetAxis("LeftJoystickY_P") < 0.19))
            {
                anim.SetTrigger("isFlipping");
                print("HERE2");
                g_bPlayerJumping[4] = true;
                Invoke("TrickRefresh", 1.0f);
            }
        }

        else if (Input.GetAxis("LeftJoystickY_P") > 0.9f && Input.GetAxis("LeftJoystickX_P") < 0.19 && Input.GetAxis("LeftJoystickX_P") > -0.19)
        {
            anim.SetTrigger("vertFlipping");
            g_bPlayerJumping[4] = true;
            Invoke("TrickRefresh", 1.0f);
        }
    }

    void TurnOffScreenShake()
    {
        Third_Person_Camera.GetComponent<cameraChanges>().enabled = false;

        if (m_bSetScreenShake)
        {
            m_bSetScreenShake = false;
            m_bScreenShakeActive = false;
        }
    }

    void RefreshShit()
    {
        if (!g_bPlayerJumping[3])
        {
            for (int i = 0; i < 8; i++)
            {
                g_bPlayerJumping[i] = false;

            }
        }
    }

    void TrickRefresh()
    {
        g_bPlayerJumping[4] = false;
    }

    void Update()

    {
        if (!m_bIsPaused)
        {

            if (m_bSetScreenShake && !m_bScreenShakeActive)
            {
                Third_Person_Camera.GetComponent<cameraChanges>().enabled = true;
                m_bScreenShakeActive = true;
                Invoke("TurnOffScreenShake", 0.4f);
            }

            /***
        * 
        * Handle side-to-side movement
        * 
        ***/
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

            /***
             * 
             * Handle player jumping
             * 
             ***/

            if (Input.GetAxis("LeftJoystickY_P") > 0.7 && !g_bPlayerJumping[3] && !g_bPlayerJumping[5]) // ++
            {
                if (g_bPlayerJumping[0] && !g_bPlayerJumping[7])
                {
                    g_bPlayerJumping[2] = true; // nollie
                    g_bPlayerJumping[5] = true;
                    print("posy jump");
                }
                else
                {
                    g_bPlayerJumping[7] = true;
                    g_bPlayerJumping[0] = true;
                    print("posy attempt jump");
                    Invoke("RefreshShit", 1.0f);
                }
                // Invoke("RefreshShit", 0.1f);
            }

            else if (Input.GetAxis("LeftJoystickY_P") < -0.7 && !g_bPlayerJumping[3] && !g_bPlayerJumping[5]) // ++
            {
                if (g_bPlayerJumping[0] == true && !g_bPlayerJumping[6])
                {
                    g_bPlayerJumping[1] = true; // ollie
                    g_bPlayerJumping[5] = true;
                    print("negy jump");
                }
                else
                {
                    g_bPlayerJumping[0] = true;
                    g_bPlayerJumping[6] = true;
                    print("negy attempt jump");
                    Invoke("RefreshShit", 1.0f);
                }
                //Invoke("RefreshShit", 0.1f);
            }

            if (g_bPlayerJumping[5])
            {
                g_bPlayerJumping[3] = true;
                g_bPlayerJumping[5] = false;
                g_vec3MovementVector.y = g_fJumpPower;
                PushPlayerMovement(g_vec3MovementVector);
                characterController.Move(g_vec3MovementVector * Time.deltaTime);
                PlaySound("jump");
            }

            /***
             * 
             * Handle tricks whilst player is jumping by difficulty
             * 
             ***/

            if (menucontroller.IsPlayingHard() == false && g_bPlayerJumping[3] && !g_bPlayerJumping[4]) // PLAYING EZY
            {
                HandleTricks();
            }
            else // HARD AS FUCK
            {
                if (Input.GetAxis("LeftJoystickX_P") < 0.19 || Input.GetAxis("LeftJoystickX_P") > 0.19)
                {
                    var vec3 = new Vector3(0, 0, (Input.GetAxis("LeftJoystickX_P")) * m_rotationSensitivity);
                    this.transform.Rotate(vec3);
                }
            }

            /***
             * 
             * Handle other shit - like rotation of wheels and reseting bools when player is grounded..
             * 
             ***/
            RotateWheels();

            if (characterController.isGrounded == true && g_bPlayerJumping[3])
            {
                Third_Person_Camera.GetComponent<cameraChanges>().enabled = true;
                Invoke("TurnOffScreenShake", 0.5f);


                for (int i = 0; i < 8; i++)
                {
                    if (i != 4)
                    {
                        g_bPlayerJumping[i] = false;
                    }
                    else
                    {
                        if (g_bPlayerJumping[i] == true)
                        {
                            print("CRASH");
                            PlaySound("crash");
                            m_bPlayerIsAlive = false;
                            m_bIsPaused = true;
                            //this.gameObject.SetActive(false);
                        }
                    }

                }
                m_iBoostersPerJump = 3; // + some integer value (like donuts collected)
            }

            if (Input.GetButtonUp("RightJoystickC_P"))
            {
                if (g_bPlayerJumping[3] && m_iBoostersPerJump > 0)
                {
                    g_vec3MovementVector.y = 10.0f;
                    PushPlayerMovement(g_vec3MovementVector);
                    characterController.Move(g_vec3MovementVector * Time.deltaTime);
                    m_iBoostersPerJump -= 1;
                    print("gap it");
                    PlaySound("boost");
                }
            }
            g_vec3MovementVector.y -= gravity * Time.deltaTime;
        }
        if (Input.GetButtonUp("XBOXStartButton"))
        {
            switch (m_bIsPaused)
            {
                case false:
                    m_bIsPaused = true;
                    break;
                case true:
                    m_bIsPaused = false;
                    break;
            }
        }
        if (Input.GetButtonUp("XBOXSelectButton"))
        {
            this.gameObject.SetActive(true);
        }

   
    }

}