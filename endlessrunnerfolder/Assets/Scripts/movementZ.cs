using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementZ : MonoBehaviour {
	public Transform tr;
	public float movementspeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!ControllerHandler.m_bIsPaused && ControllerHandler.m_bPlayerIsAlive)
        {
            tr.position = tr.position += new Vector3(0, 0, -movementspeed);
        }
	}
}
