using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoring : MonoBehaviour {
    private string lastObject;
    public GameObject stopSignPopup;
    public GameObject donutPopup;
    public GameObject roadBlockPopup;
    public GameObject roadConePopup;
    public GameObject blueCarPopup;

    public Canvas canvas;
	void OnCollisionEnter(Collision other)
	{
        if(other.gameObject.GetComponent<PointManager>() && !ControllerHandler.m_bIsPaused)
        {
            if(other.gameObject.GetComponent<PointManager>().isWorthPoints == true)
            {
                statistics.g_globalPoints += other.gameObject.GetComponent<PointManager>().worth;
                other.gameObject.GetComponent<PointManager>().isWorthPoints = false;
                ControllerHandler.m_bSetScreenShake = true;
                switch (other.gameObject.GetComponent<PointManager>().objName)
                {
                    case "cone":
                        Instantiate(roadConePopup, canvas.transform);
                        break;
                    case "roadblock":
                        Instantiate(roadBlockPopup, canvas.transform);
                        break;
                    case "bluecar":
                       // ControllerHandler.PlaySound("crash"); // could be like a car horn?
                        Instantiate(blueCarPopup, canvas.transform);
                        break;
                    case "donut":
                        Instantiate(donutPopup, canvas.transform);
                        break;
                    case "stopsign":
                        Instantiate(stopSignPopup, canvas.transform);
                        break;
                    default:
                        break;
                }

                    
            }
            
        }
	}
}