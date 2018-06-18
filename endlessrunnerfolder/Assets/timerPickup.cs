using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerPickup : MonoBehaviour {
    public GameObject particle;
	public GameObject popup;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            timer.gameTimer += 2;
            Instantiate(particle, transform.position, transform.rotation);
			Instantiate (popup, GameObject.Find("Canvas").transform);
            Destroy(gameObject);
        }
    }
}
