using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandler : MonoBehaviour
{
    public GameObject g_ObjectOnCol;
	// Use this for initialization
	void Start ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "roadTile" )
        {
            ControllerHandler.m_bPlayerIsAlive = false;
            ControllerHandler.m_bIsPaused = true;
            timer.gameTimer = 0.0f;
            statistics.g_globalPoints += ControllerHandler.m_iOverallScore;
            print("done");
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
