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
		Vector3 temp = new Vector3(0,0,-movementspeed);
		rb.velocity += temp;

	}
}
