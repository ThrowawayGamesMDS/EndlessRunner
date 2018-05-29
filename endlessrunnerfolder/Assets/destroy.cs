using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour {
    public GameObject remains;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(remains, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
