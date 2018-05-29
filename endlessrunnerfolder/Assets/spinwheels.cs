using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinwheels : MonoBehaviour {

	public Transform tr;
	
	// Update is called once per frame
	void Update () {
		tr.Rotate (Vector3.right * Time.deltaTime);
	}
}
