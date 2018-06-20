using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Collections;

public class ControllerHandler : MonoBehaviour

{
    private Vector3 g_vec3MovementVector;

    private Vector3 g_vec3DirectionalVector;

    private CharacterController characterController;

    enum tricks { KICKFLIP, INVERT_KICKFLIP, TREY_FLIP, INVERT_TREY_FLIP, FLIP, INVERT_FLIP, DEFAULT };

    private tricks m_tLastTrick;

    private tricks m_tCurrentTrick;

    private int m_iBoostersPerJump;

    private int m_iTricksPerformedThisJump;

    private int m_iTrickPointsEarned;

    public static int m_iOverallScore;

    private float g_fMovementSpeed;

    private float g_fJumpPower;

    private float g_fJumpTimer;

    private float g_fTimer;

    private float g_fCharacterDirection;

    private float g_fGravity;

    private bool m_bTryCheat;

    private bool m_bScreenShakeActive;

    private bool m_bAxisReset;

    public static bool m_bSlowMotionActivated;

    public static bool m_bIsPaused; // could do with a PlayerHandler class..

    public static bool m_bPlayerIsAlive; // could do with a PlayerHandler class..

    private bool m_bPlayerUsingCheat;

    private bool[] g_bPlayerJumping;

    private string[] m_sPlayerCheats;

    private int m_iCurrentCheatSequence;

    private bool[] m_bCheatEnabled; //0 == gravity cheat

    private bool m_bJumpCheatEnabled;

    private bool m_bJumpGravityEnabled;

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

    public static Animator anim;

    [SerializeField] private AudioClip m_acBoosterClip;

    [SerializeField] private AudioClip m_acCrashClip;

    [SerializeField] private AudioClip m_acJumpClip;

    private AudioSource m_asPlayAudio;

    public Text m_tCheatPopup;

    public CanvasGroup m_cgPopupDisplay;


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

    void DoMyWindow(int windowID)
    {
       
    }

    void OnGUI()
    {
        // Register the window. Notice the 3rd parameter
        if (m_bIsPaused && m_bPlayerIsAlive)
        {
            Rect windowRect = new Rect(225, 0, 250, 750);
            windowRect = GUI.Window(0, windowRect, DoMyWindow, "GAME PAUSED - OPTIONS MENU");
        }
    }

    void Start()
    {

        print("WIDTH: " + Screen.width + ", HEIGHT: " + Screen.height);

        // Jumping bool setup
        g_bPlayerJumping = new bool[8];
        for (int i = 0; i < 8; i++)
        {
            g_bPlayerJumping[i] = false;
        }

        m_sPlayerCheats = new string[8];
        for (int i = 0; i < 8; i++)
        {
            m_sPlayerCheats[i] = "";
        }

        m_bCheatEnabled = new bool[8];
        for (int i = 0; i < 8; i++)
        {
            m_bCheatEnabled[i] = false;
        }

        m_bTryCheat = false;

        m_bJumpCheatEnabled = false;
        m_bJumpGravityEnabled = false;

        m_bIsPaused = false;

        m_bPlayerIsAlive = true;

        m_bSetScreenShake = false;

        m_bScreenShakeActive = false;

        m_bAxisReset = false;

        m_bSlowMotionActivated = false;

        First_Person_Camera.gameObject.SetActive(false);
        Third_Person_Camera.gameObject.SetActive(true);
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

        m_iCurrentCheatSequence = 0;

        m_rotationSensitivity = 30.5f;

        g_fMovementSpeed = 32;

        g_fJumpPower = 7 * 3.5f;

        g_fJumpTimer = 1.5f;

        g_fTimer = 1.5f;

        // g_fGravity = 20;
        g_fGravity = 9.8f * 3; // g * mass

        m_iBoostersPerJump = 3;

        m_iTrickPointsEarned = 0;

        m_iTricksPerformedThisJump = 0;

        m_tLastTrick = tricks.DEFAULT;

        Third_Person_Camera.GetComponent<cameraChanges>().enabled = false;

        m_asPlayAudio = GetComponent<AudioSource>();

        //if (menucontroller.IsPlayingHard() == false)
        //{
            anim = GetComponent<Animator>();
       // }

    }

    int NumberOfOperations() // cheating
    {
        int size = 0;
        for (int i = 0; i < 8; i++)
        {
           if (m_sPlayerCheats[i] != "")
            {
                size += 1;
            }
        }
        return size;
    }

    bool CheckIfTricking()
    {
       for (int i = 0; i < 8; i ++)
        {
            if (m_bCheatEnabled[i] == true)
            {
                return true;
            }
        }
        return false;
    }

    void UpdateCheatPopupText(string _sDisplayPopup)
    {
        //GameObject obj = GameObject.CreatePrimitive(Cheat_Popup_Text)

        m_tCheatPopup.text = _sDisplayPopup;
        m_cgPopupDisplay.alpha = 1;
    }

    void CheckPlayerEnteringCheat()
    {
     bool cheat = false;
              for (int i = 0;i < NumberOfOperations(); i++)
              {
                    if (i == 0)
                    {
                        if (m_sPlayerCheats[i] == "B") // B
                          {
                                if (m_sPlayerCheats[i + 1] == "A") // B | A-B-X
                                {
                                     if (m_sPlayerCheats[i + 2] == "B")
                                     {
                                           if (m_sPlayerCheats[i + 3] == "X")
                                           {
                                                cheat = true;
                                                switch (m_bJumpCheatEnabled)
                                                {
                                                    case true:
                                                        {
                                                            m_bJumpCheatEnabled = false;
                                                            UpdateCheatPopupText("Super Jump Disabled!");
                                                            g_fJumpPower = 7 * 3.5f;
                                                            break;
                                                        }
                                                    case false:
                                                        {
                                                            m_bJumpCheatEnabled = true;
                                                            UpdateCheatPopupText("Super Jump Enabled!");
                                                            g_fJumpPower = 7 * 4.5f;
                                                            break;
                                                        }
                                                }
                                                m_bTryCheat = false;
                                                return;
                                           }
                                     }
                                }
                          }
                        else if (m_sPlayerCheats[i] == "A") // A
                        {
                            if (m_sPlayerCheats[i + 1] == "A")// A | A-A-A
                            {
                                if (m_sPlayerCheats[i + 2] == "A")
                                {
                                    if (m_sPlayerCheats[i + 3] == "A")
                                    {
                                        cheat = true;
                                        timer.gameTimer += 60.0f;
                                        UpdateCheatPopupText("Bonus 60 Second Cheat");
                                        m_bTryCheat = false;
                                        return;
                                    }
                                }
                            }
                            else if (m_sPlayerCheats[i + 1] == "X") // A | X-X-A
                            {
                                if (m_sPlayerCheats[i + 2] == "X")
                                {
                                    if (m_sPlayerCheats[i + 3] == "A")
                                    {
                                        cheat = true;

                                        switch (m_bJumpGravityEnabled)
                                        {
                                            case true:
                                                {
                                                    m_bJumpGravityEnabled = false;
                                                    g_fGravity = 9.8f * 3.0f;
                                                    UpdateCheatPopupText("Gravity cheat disabled!");
                                                   // UpdatePop
                                            break;
                                                }
                                            case false:
                                                {
                                                    m_bJumpGravityEnabled = true;
                                                    g_fGravity = 9.8f * 2.0f;
                                                    UpdateCheatPopupText("Gravity cheat enabled!");
                                                    break;
                                                }
                                        }
                                        m_bTryCheat = false;
                                        return;
                                    }
                                }
                            }
                            else if (m_sPlayerCheats[i + 1] == "Y")
                            {
                                if (m_sPlayerCheats[i + 2] == "X")
                                {
                                    if (m_sPlayerCheats[i + 3] == "A")
                                    {
                                        cheat = true;

                                        switch (m_bSlowMotionActivated)
                                        {
                                            case true:
                                                {
                                                    m_bSlowMotionActivated = false;
                                                    UpdateCheatPopupText("Slow Motion Disabled!");
                                                    break;
                                                }
                                            case false:
                                                {
                                                    m_bSlowMotionActivated = true;
                                                    UpdateCheatPopupText("Slow Motion Enabled!");
                                                    break;
                                                }
                                        }
                                        m_bTryCheat = false;
                                        return;
                                    }
                                }
                            } // A | Y-X-A
                }
                        

            }
              }
        if (cheat) //successful cheat entered
        {
            CheatRefresh();
        }
    }

    public void PlaySound(string _soundToPlay)
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
            return;
        }
        m_asPlayAudio.Play();
    }

    void ReturnToMain()
    {
        SceneManager.LoadScene("mainmenu");
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
        //if (Input.GetAxis("LeftJoystickX_P") < -0.44)
        if (Input.GetAxis("RightJoystickX_P") < -0.44)
        {
            print("EZY TRYNA TRICK1");
            // if (Input.GetAxis("LeftJoystickX_P") < -0.45)
            if (Input.GetAxis("RightJoystickX_P") < -0.45)
            {
                anim.SetTrigger("invertKickFlipping");
                print("PLAYER IS KICK FLIPPING: " + Input.GetAxis("LeftJoystickX_P") + " || " + Input.GetAxis("LeftJoystickY_P"));
                g_bPlayerJumping[4] = true;
                Invoke("TrickRefresh", 1.0f);
                m_tCurrentTrick = tricks.INVERT_KICKFLIP;
                m_iTrickPointsEarned += (50 * m_iTricksPerformedThisJump);
            }
            //else if (Input.GetAxis("LeftJoystickX_P") < -0.65 && (Input.GetAxis("LeftJoystickY_P") < -0.19))
            else if (Input.GetAxis("RightJoystickX_P") < -0.65 && (Input.GetAxis("RightJoystickY_P") < -0.19))
            {
                anim.SetTrigger("isFlipping");
                print("PLAYER IS TREY FLIPPING: " + Input.GetAxis("RightJoystickX_P") + " , " + Input.GetAxis("RightJoystickY_P"));

                g_bPlayerJumping[4] = true;
                Invoke("TrickRefresh", 1.0f);
                m_tCurrentTrick = tricks.TREY_FLIP;
                m_iTrickPointsEarned += (75 * m_iTricksPerformedThisJump);
            }
        }
        // else if (Input.GetAxis("LeftJoystickX_P") > 0.19)
        else if (Input.GetAxis("RightJoystickX_P") > 0.19)
        {
            print("EZY TRYNA TRICK2");
            //if (Input.GetAxis("LeftJoystickX_P") > 0.45)
            if (Input.GetAxis("RightJoystickX_P") > 0.45)
            {
                anim.SetTrigger("kickFlipping");
                g_bPlayerJumping[4] = true;
                Invoke("TrickRefresh", 0.3f);
                m_tCurrentTrick = tricks.KICKFLIP;
                m_iTrickPointsEarned += (50 * m_iTricksPerformedThisJump);
            }
            // else if (Input.GetAxis("LeftJoystickX_P") < 0.65 && (Input.GetAxis("LeftJoystickY_P") < 0.19))
            else if (Input.GetAxis("RightJoystickX_P") < 0.65 && (Input.GetAxis("RightJoystickY_P") < 0.19))
            {
                anim.SetTrigger("isFlipping");
                print("HERE2");
                g_bPlayerJumping[4] = true;
                Invoke("TrickRefresh", 1.0f);
                m_tCurrentTrick = tricks.INVERT_TREY_FLIP;
                m_iTrickPointsEarned += (75 * m_iTricksPerformedThisJump);
            }
        }

        //if (Input.GetAxis("LeftJoystickY_P") > 0.9f && Input.GetAxis("LeftJoystickX_P") < 0.19 && Input.GetAxis("LeftJoystickX_P") > -0.19)
        if (Input.GetAxis("RightJoystickY_P") > 0.9f && Input.GetAxis("RightJoystickX_P") < 0.19 && Input.GetAxis("RightJoystickX_P") > -0.19)
        {
            anim.SetTrigger("vertFlipping");
            g_bPlayerJumping[4] = true;
            Invoke("TrickRefresh", 1.0f);
            m_tCurrentTrick = tricks.FLIP;
            m_iTrickPointsEarned += (25 * m_iTricksPerformedThisJump);
        }
       // if (Input.GetAxis("LeftJoystickY_P") < -0.9f && Input.GetAxis("LeftJoystickX_P") < 0.19 && Input.GetAxis("LeftJoystickX_P") > -0.19)
        if (Input.GetAxis("RightJoystickY_P") < -0.9f && Input.GetAxis("RightJoystickX_P") < 0.19 && Input.GetAxis("RightJoystickX_P") > -0.19)
            {
            anim.SetTrigger("isInvertFlipping");
            g_bPlayerJumping[4] = true;
            Invoke("TrickRefresh", 1.0f);
            m_tCurrentTrick = tricks.INVERT_FLIP;
            m_iTrickPointsEarned += (25 * m_iTricksPerformedThisJump);
        }
        
        if (m_tCurrentTrick != m_tLastTrick && g_bPlayerJumping[4])
        {
            m_bAxisReset = true;
            timer.gameTimer += (2.5f * m_iTricksPerformedThisJump);
            m_iTricksPerformedThisJump += 1; // multiplier
        }
        
    }

    void SetScreenShake()
    {   
        switch(g_bFirstPersonCamera)
        {
            case true:
                First_Person_Camera.GetComponent<cameraChanges>().enabled = true;
                break;
            case false:
                Third_Person_Camera.GetComponent<cameraChanges>().enabled = true;
                break;
        }

        m_bScreenShakeActive = true;
        Invoke("TurnOffScreenShake", 0.4f);
    }

    void TurnOffScreenShake()
    {
        switch(g_bFirstPersonCamera)
        {
            case true:
                {
                    First_Person_Camera.GetComponent<cameraChanges>().enabled = false;

                    if (m_bSetScreenShake)
                    {
                        m_bSetScreenShake = false;
                        m_bScreenShakeActive = false;
                    }
                    break;
                }
            case false:
                {
                    Third_Person_Camera.GetComponent<cameraChanges>().enabled = false;

                    if (m_bSetScreenShake)
                    {
                        m_bSetScreenShake = false;
                        m_bScreenShakeActive = false;
                    }
                    break;
                }
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

    void CheatRefresh()
    {
        m_bPlayerUsingCheat = false;
        m_iCurrentCheatSequence = 0;
        for (int i = 0; i < 8; i++)
        {
            m_sPlayerCheats[i] = "";
        }
        m_bTryCheat = false;
        print("DISABLING CHEATS");
    }

    void ResetingPlayerTricks()
    {
        for (int i = 0; i < 8; i++)
        {
            m_bCheatEnabled[i] = false;
        }
    }

    void Update()

    {
        if (!m_bIsPaused)
        {
            if (Input.GetButtonUp("XBOXSelectButton"))
            {
                // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                switch(g_bFirstPersonCamera)
                {
                    case true:
                        First_Person_Camera.gameObject.SetActive(false);
                        Third_Person_Camera.gameObject.SetActive(true);
                        g_bFirstPersonCamera = false;
                        break;
                    case false:
                        First_Person_Camera.gameObject.SetActive(true);
                        Third_Person_Camera.gameObject.SetActive(false);
                        g_bFirstPersonCamera = true;
                        break;
                }
            }

            /***
             * 
             * Cheat shit
             * 
             ***/

            if (Input.GetButtonUp("XBOXL1Button"))
            {
                if(m_bPlayerUsingCheat)
                {
                    CheatRefresh();
                    m_bPlayerUsingCheat = false;
                }
                else
                {
                    m_bPlayerUsingCheat = true;
                    print("ENABLING CHEATS");
                    m_bTryCheat = true;
                    Invoke("CheatRefresh", 5.0f);
                }
                /*
                //if (!m_bJumpCheatEnabled ||)
                    m_bPlayerUsingCheat = true;
                    print("ENABLING CHEATS");
                    m_bTryCheat = true;
                    Invoke("CheatRefresh", 5.0f);*/
                 
                }

            if (m_bPlayerUsingCheat)
            {
                if (Input.GetButtonUp("XBOXAButton"))
                {
                   m_sPlayerCheats[m_iCurrentCheatSequence] = "A";
                   m_iCurrentCheatSequence += 1;
                }
                if (Input.GetButtonUp("XBOXBButton"))
                {
                    m_sPlayerCheats[m_iCurrentCheatSequence] = "B";
                    m_iCurrentCheatSequence += 1;
                }
                if (Input.GetButtonUp("XBOXXButton"))
                {
                    m_sPlayerCheats[m_iCurrentCheatSequence] = "X";
                    m_iCurrentCheatSequence += 1;
                }
                if (Input.GetButtonUp("XBOXYButton"))
                {
                    m_sPlayerCheats[m_iCurrentCheatSequence] = "Y";
                    m_iCurrentCheatSequence += 1;
                }

                //if (NumberOfOperations() >= 4 && !CheckIfTricking())
                if(NumberOfOperations() >= 4 && m_bTryCheat)
                {
                    CheckPlayerEnteringCheat();
                }
                else if (NumberOfOperations() >= 4 && !m_bTryCheat)
                {
                    // cancel invoke?
                    // call CheatReset
                }
            }
           
            /** End of cheat shit **/


            // if (Input.GetAxis("LeftJoystickX_P") < 0.19 && Input.GetAxis("LeftJoystickX_P") > -0.19 && Input.GetAxis("LeftJoystickY_P") < 0.19 && Input.GetAxis("LeftJoystickY_P") > -0.19)
            if (Input.GetAxis("RightJoystickX_P") < 0.19 && Input.GetAxis("RightJoystickX_P") > -0.19 && Input.GetAxis("RightJoystickY_P") < 0.19 && Input.GetAxis("RightJoystickY_P") > -0.19)
            {
                m_bAxisReset = false;
            }

            if (m_bSetScreenShake && !m_bScreenShakeActive)
            {
                SetScreenShake();
            }

        /***
        * 
        * Handle side-to-side movement
        * 
        ***/
            // if (!g_bPlayerJumping[3] && (Input.GetAxis("RightJoystickX_P") > 0.19 || Input.GetAxis("RightJoystickX_P") < 0.19)) // stops the player's x movement whilst in the air.
            if (!g_bPlayerJumping[3] && (Input.GetAxis("LeftJoystickX_P") > 0.19 || Input.GetAxis("LeftJoystickX_P") < 0.19)) // stops the player's x movement whilst in the air.
            {
                //g_vec3MovementVector.x = Input.GetAxis("RightJoystickX_P") * g_fMovementSpeed;
                g_vec3MovementVector.x = Input.GetAxis("LeftJoystickX_P") * g_fMovementSpeed;
                PushPlayerMovement(g_vec3MovementVector);
            }
            else
            {
                //g_vec3MovementVector.x = Input.GetAxis("RightJoystickX_P") * (g_fMovementSpeed / 3);
                g_vec3MovementVector.x = Input.GetAxis("LeftJoystickX_P") * (g_fMovementSpeed / 3);
                PushPlayerMovement(g_vec3MovementVector);
            }

            /***
             * 
             * Handle player jumping
             * 
             ***/

            // if (Input.GetAxis("LeftJoystickY_P") > 0.7 && !g_bPlayerJumping[3] && !g_bPlayerJumping[5]) // ++
            if (Input.GetAxis("RightJoystickY_P") > 0.7 && !g_bPlayerJumping[3] && !g_bPlayerJumping[5])
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

            //else if (Input.GetAxis("LeftJoystickY_P") < -0.7 && !g_bPlayerJumping[3] && !g_bPlayerJumping[5]) // ++
            else if (Input.GetAxis("RightJoystickY_P") < -0.7 && !g_bPlayerJumping[3] && !g_bPlayerJumping[5])
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
                m_bAxisReset = true;
            }

            /***
             * 
             * Handle tricks whilst player is jumping by difficulty
             * 
             ***/

            if (menucontroller.IsPlayingHard() == false && g_bPlayerJumping[3] && !g_bPlayerJumping[4] && !m_bAxisReset) // PLAYING EZY
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
                //Third_Person_Camera.GetComponent<cameraChanges>().enabled = true;
                //Invoke("TurnOffScreenShake", 0.5f);
                SetScreenShake();

                /***
                 * 
                 * TRICK POINT SYSTEM
                 * 
                 ***/

                statistics.g_globalPoints += (m_iTrickPointsEarned * m_iTricksPerformedThisJump);
                print(m_iTrickPointsEarned * m_iTricksPerformedThisJump);

                m_iTricksPerformedThisJump = 1; // default one for the multiplier so it doesnt give 0 points...
                m_iTrickPointsEarned = 0;
                m_tLastTrick = m_tCurrentTrick;
                m_tCurrentTrick = tricks.DEFAULT;

                /***
                 * 
                 * RESETING NOOBY JUMP BOOL SYSTEM
                 * 
                 ***/

                for (int i = 0; i < 8; i++)
                {
                    if (i == 4)
                    {
                        if (g_bPlayerJumping[i] == true)
                        {
                            PlaySound("crash");
                        }
                    }
                    g_bPlayerJumping[i] = false;

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
                    PlaySound("boost");
                }
            }
            g_vec3MovementVector.y -= g_fGravity * Time.deltaTime;
        }

        // 

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
        /*
        if (Input.GetButtonUp("XBOXSelectButton"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetButtonDown("XBOXSelectButton"))
        {
            Invoke("ReturnToMain", 0.4f);
        }*/


    }

}