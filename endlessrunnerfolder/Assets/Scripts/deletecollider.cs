using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deletecollider : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "building" || other.gameObject.tag == "buildingprefab")
        {
            Destroy(other.gameObject);
        }
    }
}
