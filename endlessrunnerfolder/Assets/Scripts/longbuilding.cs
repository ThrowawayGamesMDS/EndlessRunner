using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class longbuilding : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var rot = transform.rotation;
        rot.y = 90;
        transform.rotation = rot;
	}
	
}
