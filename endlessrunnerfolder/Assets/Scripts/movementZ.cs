using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementZ : MonoBehaviour {
	public Transform tr;
	public float movementspeed;
    public static float m_fSlowdown;
	// Use this for initialization
	void Start () {
        m_fSlowdown = 1.0f;

    }
	
	// Update is called once per frame
	void Update () {
        if (!ControllerHandler.m_bIsPaused && ControllerHandler.m_bPlayerIsAlive)
        {
            if(ControllerHandler.m_bSlowMotionActivated)
            {
                tr.position = tr.position += new Vector3(0, 0, (-movementspeed * 0.1f));
            }
            else
            {
                tr.position = tr.position += new Vector3(0, 0, (-movementspeed * 1.0f));
            }
        }
	}
}
