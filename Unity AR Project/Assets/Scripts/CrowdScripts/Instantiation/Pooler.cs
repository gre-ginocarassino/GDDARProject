using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    Spawn spawn;
    bool isSpawned;
    private float wait;

    private void Start()
    {
        spawn = Spawn._Spawn;
        isSpawned = false;
        wait = 2f;
    }
    private void Update()
    {
        if (isSpawned == true && wait <= 0)
        {
            spawn.SpawnFromPool("walker", transform.position, Quaternion.identity);
            wait = .5f;
        }
        else
        {
           if (wait <= 0)
            {
                isSpawned = true;
            }
           else
            {
                wait -= Time.deltaTime;
            }
        }
    }

    


}
