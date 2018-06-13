using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnParticle : MonoBehaviour {

    public GameObject particle;
    private bool isInstantiated;
    void Start()
    {
        isInstantiated = false;
    }
    void OnCollisionEnter(Collision other)
    {
        if (!isInstantiated)
        {
            if (other.gameObject.tag == "Player")
            {
                Instantiate(particle, transform.position, transform.rotation, transform);
                
                isInstantiated = true;
            }
        }

    }
}
