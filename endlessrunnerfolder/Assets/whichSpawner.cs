using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whichSpawner : MonoBehaviour {
    public objectspawner spawner1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(ControllerHandler.SpawnerSystem != 0)
        {
            spawner1.enabled = false;

        }
        else
        {
            spawner1.enabled = true;
        }
	}
}
