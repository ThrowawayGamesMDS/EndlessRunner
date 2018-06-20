using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movegorund : MonoBehaviour 
{
    public float movementspeed;
	public Rigidbody rb;
	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}
	void Update () 
	{
		//Vector3 pos = new Vector3(0,0,-movementspeed);
		//transform.position += pos;
        if (!ControllerHandler.m_bIsPaused && ControllerHandler.m_bPlayerIsAlive)
        {
            if (ControllerHandler.m_bSlowMotionActivated)
            {
                Vector3 temp = new Vector3(0, 0, (-movementspeed * 0.1f));
                rb.velocity += temp;
            }
            else
            {
                Vector3 temp = new Vector3(0, 0, (-movementspeed * 1.0f));
                rb.velocity += temp;
            }
        }
        else if (ControllerHandler.m_bIsPaused)
        {
            Vector3 temp = new Vector3(0, 0, 0);
            rb.velocity = temp;
        }


    }
}
