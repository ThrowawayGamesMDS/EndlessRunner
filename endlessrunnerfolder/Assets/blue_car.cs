using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue_car : MonoBehaviour {
	public AudioSource crash;
	// Use this for initialization
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.GetComponent<PointManager> () && !ControllerHandler.m_bIsPaused) {
			if (other.gameObject.GetComponent<PointManager> ().isWorthPoints == true) {
				if (other.gameObject.GetComponent<PointManager> ().objName == "Player") {
					crash.Play ();
				}
			}
		}
	}
}

