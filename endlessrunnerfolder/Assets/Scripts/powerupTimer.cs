using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupTimer : MonoBehaviour {
	timer timingz;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			timingz = FindObjectOfType<timer>();
			timingz.timeIncrease ();
			Destroy(gameObject);
		}
	}
}
