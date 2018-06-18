using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scoring : MonoBehaviour {
    private string lastObject;
    public GameObject stopSignPopup;
    public GameObject donutPopup;
    public GameObject roadBlockPopup;
    public GameObject roadConePopup;
    public GameObject blueCarPopup;
    public GameObject explosionParticle;
    public float comboTimer;
    public static int comboInt;
    public Text comboTxt;
    public Canvas canvas;
    public AudioSource scream;
    public AudioSource crate;
    public AudioSource horn;

    private void Start()
    {
        comboInt = 1;
        comboTimer = 3;
    }
    



    void OnCollisionEnter(Collision other)
	{
        if(other.gameObject.GetComponent<PointManager>() && !ControllerHandler.m_bIsPaused)
        {
            if(other.gameObject.GetComponent<PointManager>().isWorthPoints == true)
            {
                statistics.g_globalPoints += other.gameObject.GetComponent<PointManager>().worth * comboInt;
                other.gameObject.GetComponent<PointManager>().isWorthPoints = false;
                ControllerHandler.m_bSetScreenShake = true;
                switch (other.gameObject.GetComponent<PointManager>().objName)
                {
				case "cone":
						popupManager.popupQueue.Add(roadConePopup);
                        comboInt += 1;
                        comboTimer = 3;
                        break;
                    case "roadblock":
						popupManager.popupQueue.Add(roadBlockPopup);
                        comboInt += 1;
                        comboTimer = 3;
                        crate.Play();
                        break;
                    case "bluecar":
                        // ControllerHandler.PlaySound("crash"); // could be like a car horn?
                        comboInt = 1;
                        comboTimer = 3;
						popupManager.popupQueue.Add(blueCarPopup);
                        horn.Play();
                        break;
                    case "donut":
                        comboInt += 1;
                        comboTimer = 3;
						popupManager.popupQueue.Add(donutPopup);
                        break;
                    case "stopsign":
                        comboInt += 1;
                        comboTimer = 3;
						popupManager.popupQueue.Add(stopSignPopup);
                        break;
                    case "body":
                        comboInt += 1;
                        comboTimer = 3;
                        scream.Play();
						popupManager.popupQueue.Add(roadConePopup);
                        break;
                    case "box":
                        comboInt += 1;
                        comboTimer = 3;
                        crate.Play();
						popupManager.popupQueue.Add(roadConePopup);
                        break;
                    default:
                        break;
                }

                    
            }
            
        }
	}


    private void Update()
    {
        if(comboInt > 8)
        {
            comboInt = 8;
        }
        if (!ControllerHandler.m_bIsPaused && ControllerHandler.m_bPlayerIsAlive)
        {
            comboTimer -= Time.deltaTime;
            comboTxt.text = Mathf.RoundToInt(comboInt).ToString();
        }
        if(comboTimer < 0)
        {
            comboInt = 1;
            comboTimer = 3;
        }
    }
}