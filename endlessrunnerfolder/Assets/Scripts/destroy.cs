using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour {
    public GameObject remains;
    private bool isInstantiated;
    void Start()
    {
        isInstantiated = false;
    }
    void OnCollisionEnter(Collision other)
    {
        if(!isInstantiated)
        {
            if (other.gameObject.tag == "Player")
            {
                Instantiate(remains, transform.position, transform.rotation);
                Destroy(gameObject);
                isInstantiated = true;
            }
        }
        
    }
}
