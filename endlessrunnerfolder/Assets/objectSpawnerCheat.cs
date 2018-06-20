using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectspawnerCheat : MonoBehaviour
{
    public GameObject obj;
   
    void Update()
    {
        if (!ControllerHandler.m_bIsPaused && ControllerHandler.m_bPlayerIsAlive)
        {
            spawnRoad();
        }
    }

    void spawnRoad()
    {
        int temp = Random.Range(1, 15);
        if ((temp == 2))
        {
            Instantiate(obj, new Vector3(Random.Range(-15, 15), 1, 500), obj.transform.rotation);
        }
    }
    
}
