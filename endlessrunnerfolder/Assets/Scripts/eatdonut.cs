using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eatdonut : MonoBehaviour {
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            statistics.g_globalPoints += 1000;
            timer.gameTimer += 2;
            Destroy(gameObject);
        }
    }
}
