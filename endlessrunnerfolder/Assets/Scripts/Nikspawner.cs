using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nikspawner : MonoBehaviour {
	public List<GameObject> RoadObjs;
	public List<GameObject> PathObjs;
	GameObject rando;
	int RoadA=0;
	int RoadB = 0;
	int pathA=0;
	int pathB=0;

	// Use this for initialization
	void Start () {
		 RoadA=Random.Range (10, 50);
		 RoadB=Random.Range (10, 50);
		 pathA=Random.Range (10, 50);
		 pathB=Random.Range (10, 50);
	}
	
	// Update is called once per frame
	void Update () {
		if (!ControllerHandler.m_bIsPaused && ControllerHandler.m_bPlayerIsAlive)
		{	
			if (RoadA == 0) {
				Rspawn ("RA");
				RoadA = Random.Range (30, 60);
			} else {
				RoadA--;
			}
			if (RoadB == 0) {
				Rspawn("RB");
				RoadB = Random.Range (30, 60);
			}else{
				RoadB--;
			}
			if (pathA == 0) {
				Pspawn("PA");
				pathA = Random.Range (30, 60);
			}else{
				pathA--;
			}
			if (pathB == 0) {
				Pspawn("PB");
				pathB = Random.Range (30, 60);
			}else{
				pathB--;
			}

		}
	}
	void Rspawn(string place){
		int rand = Random.Range (0,RoadObjs.Count);
		if ("RA" == place) {
			Instantiate (rando =RoadObjs [rand], new Vector3 (Random.Range (-14, 0), 6, 300), rando.transform.rotation);
		} else {
			Instantiate (rando =RoadObjs [rand], new Vector3 (Random.Range (0, 14), 6, 300), rando.transform.rotation);
		}
	}
	void Pspawn(string place){
		int rand = Random.Range (0,PathObjs.Count);
		if ("PA" == place) {
			Instantiate (rando =PathObjs [rand], new Vector3 (Random.Range (21, 14), 6, 300), rando.transform.rotation);
		} else {
			Instantiate (rando =PathObjs [rand], new Vector3 (Random.Range (-21, -14), 6, 300), rando.transform.rotation);
		}
	}
}
