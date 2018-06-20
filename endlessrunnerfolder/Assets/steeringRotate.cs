using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steeringRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(0, -45 * Input.GetAxis("LeftJoystickX_P") + 180, 0);	
	}
}
