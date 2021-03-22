using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    Spawn spawn;
    bool isSpawned;
    bool ShouldSpawn;
    public float wait;
    public float startWait;

    private void Start()
    {
        spawn = Spawn._Spawn;
        isSpawned = false;
        ShouldSpawn = this;
        wait = 2f;
    }

    public void startSpawn(bool shouldSpawn)
    {
        this.ShouldSpawn = shouldSpawn;
        //Debug.Log(ShouldSpawn);
    }

    private void Update()
    {
        if (ShouldSpawn == true)
        {
            if (isSpawned == true && wait <= 0)
            {
                spawn.SpawnFromPool("walker", transform.position, transform.rotation);
                wait = startWait;
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

    


}
