using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deletecollider : MonoBehaviour {

	void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "building")
        {
            Destroy(other.gameObject);
        }
    }
}
