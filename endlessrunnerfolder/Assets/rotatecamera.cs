using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatecamera : MonoBehaviour {
    public int rotationSpeed;
    public Transform center;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(center.position, Vector3.up, rotationSpeed * Time.deltaTime);
        transform.LookAt(center.position);
    }
}
