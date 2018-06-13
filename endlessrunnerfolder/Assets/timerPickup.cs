using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerPickup : MonoBehaviour {
    public GameObject particle;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            timer.gameTimer += 2;
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
