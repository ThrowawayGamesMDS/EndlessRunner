using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraChanges : MonoBehaviour {
	bool Xdirection = false;//false is left //left is +
	bool Ydirection = false;//false is up //up is +
	float shak = 0;
	float delay = 0;
	float shakeX = 0;
	float shakeY = 0;
	float shakeZ = 0;
	float currnetx = 0;
	float currnety = 0;
	float yHit = 0;
	public float CarPostion = 0;
	public float delayedcam = 0;
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
			shakeX = Random.Range (0.02f, 0.06f);//+
			if (transform.position.y + shakeY <= 2.45f + shak) {
				shakeY = Random.Range (0.00f, 0.03f);//+
			} else {
				if (transform.position.y + shakeY <= 2.75f + shak) {
					shakeY = Random.Range (-0.03f, 0.00f);//+
				} else {
					shakeY = Random.Range (-0.03f, 0.03f);//+
				}

			}

			if (transform.position.z + shakeZ <= 12.4f + shak) {
				shakeY = Random.Range (0.00f, 0.03f);//+
			} else {
				if (transform.position.z + shakeZ >= 12.6f + shak) {
					shakeY = Random.Range (-0.03f, 0.00f);//+
				} else {
					shakeY = Random.Range (-0.03f, 0.03f);//+
				}
			}
				
			if (shak!=0) {
				shakeX= shakeX*shak;
			}

			if (transform.position.x + shakeX <= .3f+shak+CarPostion) {
				currnetx = currnetx + shakeX;
				transform.position = new Vector3 (transform.position.x + shakeX,transform.position.y+shakeY,transform.position.z);
			}
			if (delay == 4) {
				Xdirection = false;
				delay = 0;
			} else {
				delay++;
			}

			//}
		} else {
			//if (currnetx >= -0.5f) {
			if (transform.position.y + shakeY <= 2.45f + shak) {
				shakeY = Random.Range(0.00f,0.03f);//+
			}else{
				if (transform.position.y + shakeY >= 2.75f + shak) {
					shakeY = Random.Range (-0.03f, 0.00f);//+
				} else {
					shakeY = Random.Range (-0.03f, 0.03f);//+
				}
			}
				shakeX = Random.Range (-0.06f, -.02f);//-

			if (shak!=0) {
				shakeX= shakeX*shak;
			}	

			if (transform.position.x + shakeX >= -.3f-shak+CarPostion) {
				currnetx = currnetx + shakeX;
				transform.position = new Vector3 (transform.position.x + shakeX,transform.position.y+shakeY,transform.position.z);
			}
			if (delay == 4) {
				Xdirection = true;
				delay = 0;
			} else {
				delay++;
			}
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
		if (delayedcam != 0) {
			cameramove ();
		}
	}
	public void hit(){

		if (shak == 0) {
			shak = 1;	
		} else {
			shak = shak - 0.1f;
		}
			
	}
	void cameramove(){
		if (delayedcam == 1) {
			//transform.position = new Vector3 (transform.position.x +CarPostion,transform.position.y,transform.position.z);
			CarPostion = 0;
		}
		delayedcam--;
	}
}
