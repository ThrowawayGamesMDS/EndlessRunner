using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menucontroller : MonoBehaviour {
    public Image option1;
    public Image option2;
    public Image option3;
    public Image firstperson;
    public Image thirdperson;
    public int selector;
    public GameObject mainmenu;
    public GameObject perspective;
    public GameObject options;
    public int whatMenu;
    int mainmenuint = 1;
    int perspectivemenu = 2;
    int optionsmenu = 3;
	// Use this for initialization
	void Start () {
        selector = 1;
        whatMenu = mainmenuint;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (whatMenu == mainmenuint)
            {
                switch(selector)
                {
                    case 1:
                        mainmenu.SetActive(false);
                        perspective.SetActive(true);
                        whatMenu = perspectivemenu;
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
        


        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            selector -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selector += 1;
        }
        if(whatMenu == perspectivemenu)
        {
            if(Input.GetKeyDown(KeyCode.Backspace))
            {
                mainmenu.SetActive(true);
                perspective.SetActive(false);
                whatMenu = mainmenuint;
            }
        }
        else if (whatMenu == optionsmenu)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
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
                    option1.rectTransform.localScale = new Vector3(3, 3, 0);
                    option2.rectTransform.localScale = new Vector3(2, 2, 0);
                    option3.rectTransform.localScale = new Vector3(2, 2, 0);
                    break;
                case 2:
                    option2.rectTransform.localScale = new Vector3(3, 3, 0);
                    option1.rectTransform.localScale = new Vector3(2, 2, 0);
                    option3.rectTransform.localScale = new Vector3(2, 2, 0);
                    break;
                case 3:
                    option3.rectTransform.localScale = new Vector3(3, 3, 0);
                    option2.rectTransform.localScale = new Vector3(2, 2, 0);
                    option1.rectTransform.localScale = new Vector3(2, 2, 0);
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
                    firstperson.rectTransform.localScale = new Vector3(3, 3, 0);
                    thirdperson.rectTransform.localScale = new Vector3(2, 2, 0);
                    break;
                case 2:
                    thirdperson.rectTransform.localScale = new Vector3(3, 3, 0);
                    firstperson.rectTransform.localScale = new Vector3(2, 2, 0);
                    break;
                default:
                    break;
            }
        }
    }
}
