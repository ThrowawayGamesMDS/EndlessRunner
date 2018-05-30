using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraChanges : MonoBehaviour {
	bool Xdirection = false;//false is left //left is +
	bool Ydirection = false;//false is up //up is +
	float shak = 0;
	float shakeX = 0;
	float shakeY = 0;
	float currnetx = 0;
	float currnety = 0;
	float yHit = 0;
	int hitTimer = 0;
	//float max = 0.5f;
	//float min = -0.5f;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Xdirection == true) {
			//if (currnetx <= 0.5f) {
				shakeX = Random.Range(.1f,0.3f);//+
				
			if (shak!=0) {
				shakeX= shakeX*shak;
			}

			if (transform.position.x + shakeX <= .5f+shak) {
				currnetx = currnetx + shakeX;
				transform.position = new Vector3 (transform.position.x + shakeX,transform.position.y+yHit,transform.position.z);
			}
			Xdirection =false;
			//}
		} else {
			//if (currnetx >= -0.5f) {
				shakeX = Random.Range (-0.3f, -.1f);//-

			if (shak!=0) {
				shakeX= shakeX*shak;
			}	

			if (transform.position.x + shakeX >= -.5f-shak) {
				currnetx = currnetx + shakeX;
				transform.position = new Vector3 (transform.position.x + shakeX,transform.position.y+yHit,transform.position.z);
			}
			Xdirection = true;
			//}
		}
		if (hitTimer!=0) {
			hit ();
		}
		if (shak!=0) {
			hitStop ();
		}
		//Debug.Log ("move");

		//Debug.Log ("move");
	}
	public void hitStop(){
		hitTimer++;
		if ((hitTimer <= 120)&& (hitTimer!=0)) {
			yHit = 0.1f;
			//hitTimer++;
		}
		if ((hitTimer >= 120)&& (hitTimer!=0)) {
			yHit = -1/3f;
			//hitTimer++;
		}
		if ((hitTimer == 300)) {
			hitTimer = 0;
		}
	}
	public void hit(){

		if (shak == 0) {
			shak = 1;	
		} else {
			shak = shak - 0.1f;
		}
			
	}
}
