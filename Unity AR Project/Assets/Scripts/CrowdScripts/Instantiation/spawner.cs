using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    //private GameObject spawnoject;
    public GameObject instantiateobj;
    public GameObject chars;
    bool isspawned = false;
    bool isspawned2 = false;
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (isspawned == false)
            {
                spawn();
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if (isspawned2 == false)
            {
                spawnChar();
            }
        }
    }

    
    void spawn()
    {
        
        Instantiate(instantiateobj, transform.position, transform.rotation);
        isspawned = true;
        
    }

    void spawnChar()
    {
        Instantiate(chars, transform.position, transform.rotation);
        isspawned2 = true;
    }
}
