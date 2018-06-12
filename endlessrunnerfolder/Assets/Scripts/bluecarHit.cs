using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluecarHit : MonoBehaviour {
	cameraChanges camChange;
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			camChange = FindObjectOfType<cameraChanges> ();
			camChange.hitStop ();
			//statistics.g_globalPoints += 1000;
			//timer.gameTimer += 2;
			//Destroy(gameObject);
		}
	}
}
