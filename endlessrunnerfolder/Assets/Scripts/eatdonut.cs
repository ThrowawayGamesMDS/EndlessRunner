using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eatdonut : MonoBehaviour {
    public GameObject particle;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
