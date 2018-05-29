using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoring : MonoBehaviour {
	public static int score;
	public bool lockout;
	// Use this for initialization
	void Start () {
		score = 0;
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "building")
		{
			if (lockout == false) {
				if (other.gameObject.name == "Body") {
					score -= 5;
				}
				else
				{
					score += 2;
				}

				Debug.Log (score);
				lockout = true;
				StartCoroutine (lockoutf ());
			}


		}
	}
	IEnumerator lockoutf()
	{
		yield return new WaitForSeconds (1);
		lockout = false;
	}
}