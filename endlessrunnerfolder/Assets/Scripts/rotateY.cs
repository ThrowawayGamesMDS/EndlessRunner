﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateY : MonoBehaviour {
    public int speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
	}
}
