using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eatdonut : MonoBehaviour {
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            timer.gameTimer += 2;
            Destroy(gameObject);
        }
    }
}
