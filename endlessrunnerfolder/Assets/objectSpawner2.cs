using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSpawner2 : MonoBehaviour
{
    public GameObject obj;
    public GameObject obj2;
    public int spawnrate;
    void Update()
    {
        if (!ControllerHandler.m_bIsPaused && ControllerHandler.m_bPlayerIsAlive && ControllerHandler.SpawnerSystem == 1)
        {
            spawnRoad();
        }
        else if (!ControllerHandler.m_bIsPaused && ControllerHandler.m_bPlayerIsAlive && ControllerHandler.SpawnerSystem == 2)
        {
            spawnRoad2();
        }
    }

    void spawnRoad()
    {
        int temp = Random.Range(1, spawnrate);
        if ((temp == 2))
        {
            Instantiate(obj, new Vector3(Random.Range(-15, 15), 1, 500), obj.transform.rotation);
        }
    }

    void spawnRoad2()
    {
        int temp = Random.Range(1, spawnrate * 2);
        if ((temp == 2))
        {
            Instantiate(obj2, new Vector3(Random.Range(-15, 15), 1, 500), obj2.transform.rotation);
        }
    }

}
