using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiate : MonoBehaviour {

	public GameObject Building1;
    public float timeDelay;

    void OnTriggerExit(Collider other)
	{
		if(other.tag == "buildingprefab")
        {
            StartCoroutine(instant());
        }
		
	}
	IEnumerator instant()
	{
		yield return new WaitForSeconds (timeDelay);
		Instantiate(Building1, transform.position, Building1.transform.rotation);	
	}
  
}
