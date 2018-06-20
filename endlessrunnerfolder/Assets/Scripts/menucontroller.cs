using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menucontroller : MonoBehaviour {
    public List<Sprite> buttons;
    public Image option1;
    public Image option2;
    public Image option3;
    public Image firstperson;
    public Image thirdperson;
    public int selector;
    public GameObject mainmenu;
    public GameObject perspective;
    public GameObject options;

    private bool m_bAllowControllerMovement;

    public int whatMenu;
    enum mode { EASY, HARD};
    static mode g_mPlayerDifficulty;
    int mainmenuint = 1;
    int perspectivemenu = 2;
    int optionsmenu = 3;
	// Use this for initialization
	void Start () {
        selector = 1;
        whatMenu = mainmenuint;
        g_mPlayerDifficulty = mode.EASY;
        m_bAllowControllerMovement = true;

    }

    void RefreshControllerSelection()
    {
        m_bAllowControllerMovement = true;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonUp("XBOXAButton"))
        {
            if (whatMenu == mainmenuint)
            {
                switch(selector)
                {
                    case 1:
                        SceneManager.LoadScene("sidescroll");
                        break;
                    case 2:
                        mainmenu.SetActive(false);
                        options.SetActive(true);
                        whatMenu = optionsmenu;
                        break;
                    case 3:
                        Debug.Log("quit");
                        Application.Quit();
                        break;
                    default:
                        break;
                }
                
            }
            else if(whatMenu == perspectivemenu)
            {
                switch (selector)
                {
                    case 1:
                        SceneManager.LoadScene("sidescroll");
                        break;
                    case 2:
                        Debug.Log("thirdperson");
                        break;
                    default:
                        break;
                }
            }
        }
        

        if (Input.GetAxis("LeftJoystickY_P") > -0.19f && Input.GetAxis("LeftJoystickY_P") < 0.19f)
        {
            m_bAllowControllerMovement = true;
        }


        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("LeftJoystickY_P") < -0.9 && m_bAllowControllerMovement)
        {
            selector -= 1;
            m_bAllowControllerMovement = false;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("LeftJoystickY_P") > 0.9 && m_bAllowControllerMovement)
        {
            selector += 1;
            m_bAllowControllerMovement = false;
        }
        if(whatMenu == perspectivemenu)
        {
            if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetButtonUp("XBOXBButton"))
            {
                mainmenu.SetActive(true);
                perspective.SetActive(false);
                whatMenu = mainmenuint;

            }
        }
        else if (whatMenu == optionsmenu)
        {
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetButtonUp("XBOXBButton"))
            {
                mainmenu.SetActive(true);
                options.SetActive(false);
                whatMenu = mainmenuint;
            }
        }

        if(whatMenu == mainmenuint)
        {
            if (selector > 3)
            {
                selector = 1;
            }
            else if (selector < 1)
            {
                selector = 3;
            }
        }
        else if(whatMenu == perspectivemenu)
        {
            if (selector > 2)
            {
                selector = 1;
            }
            else if (selector < 1)
            {
                selector = 2;
            }
        }



        if (whatMenu == mainmenuint)
        {
            switch (selector)
            {
                case 1:
                    option1.sprite = buttons[1];
                    option2.sprite = buttons[0];
                    option3.sprite = buttons[0];

                    break;
                case 2:
                    option1.sprite = buttons[0];
                    option2.sprite = buttons[1];
                    option3.sprite = buttons[0];
                    break;
                case 3:
                    option1.sprite = buttons[0];
                    option2.sprite = buttons[0];
                    option3.sprite = buttons[1];
                    break;
                default:
                    break;
            }
        }
        else if(whatMenu == perspectivemenu)
        {
            switch (selector)
            {
                case 1:
                    firstperson.sprite = buttons[1];
                    thirdperson.sprite = buttons[0];
                    break;
                case 2:
                    thirdperson.sprite = buttons[1];
                    firstperson.sprite = buttons[0];
                    break;
                default:
                    break;
            }
        }
    }
    
    public static bool IsPlayingHard()
    {
        if (g_mPlayerDifficulty == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
        /*
        switch(g_mPlayerDifficulty)
        {
            case mode.HARD:
                {
                    return true;
                }
            case mode.EASY:
                {
                    return true;
                }
            default: break;
        }
        return true;*/
    }
}
