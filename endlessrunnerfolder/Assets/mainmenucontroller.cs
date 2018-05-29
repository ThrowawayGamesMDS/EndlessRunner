using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainmenucontroller : MonoBehaviour {
    public int menuchoice;
	public bool isPersp = false;
    public Transform selector;
	public GameObject mainmenu;
	public GameObject perspective;
	public bool timeout = false;
	// Use this for initialization
	void Start () {
        int menuchoice = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("RightJoystickX_P") <= -0.9f)
        {
            if(menuchoice == 2)
            {
                selector.position = selector.position += new Vector3(-248.1f, 0, 0);

                menuchoice -= 1;
            }
        }
		if (Input.GetAxis("RightJoystickX_P") >= 0.9f)
        {
            if (menuchoice == 1)
            {
                selector.position = selector.position += new Vector3(248.1f, 0, 0);
                menuchoice += 1;
            }
        }
		if (Input.GetAxis("RightJoystickY_P") >= 0.9f)
        {
            if (menuchoice == 1)
            {    
				if (isPersp == false) {
					mainmenu.SetActive (false);
					perspective.SetActive (true);
					isPersp = true;
					timeout = true;
					StartCoroutine (timerf ());
				}                
            }
            if(menuchoice == 2)
            {
				
				if (isPersp == false) {
					Debug.Log("quit!");
					Application.Quit();
				}

                
            }
        }
		if (Input.GetAxis("RightJoystickY_P") <= -0.9f)
		{
			if (menuchoice == 1)
			{    
				if (isPersp == true) {
					SceneManager.LoadScene (1);
				}                
			}
			if(menuchoice == 2)
			{

				if (isPersp == true) {
					SceneManager.LoadScene (2);
				}


			}
		}
	}
	IEnumerator timerf(){
		yield return new WaitForSeconds (2);
		timeout = false;
	}
}
