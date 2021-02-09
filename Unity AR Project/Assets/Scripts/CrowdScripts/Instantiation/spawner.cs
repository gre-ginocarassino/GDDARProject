using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    //private GameObject spawnoject;
    public GameObject instantiateobj;
    bool isspawned = false;
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (isspawned == false)
            {
                spawn();
            }
        }
    }

    
    void spawn()
    {
        
        Instantiate(instantiateobj, transform.position, transform.rotation);
        isspawned = true;
        
    }
}
