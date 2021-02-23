using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Spawn : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject[] charTypes;
        public int size;
    }

    public Transform parent;

    public static Spawn _Spawn;

    private void Awake()
    {
        _Spawn = this;
    }
   

    public List<Pool> pools;

    public Transform[] allDirection;

    public Dictionary<string, Queue<GameObject>> spawnPool;

    void Start()
    {
        spawnPool = new Dictionary<string, Queue<GameObject>>();

       //Instantiate during initialisation
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                
                GameObject obj = Instantiate(pool.charTypes[Random.Range(0, pool.charTypes.Length)]);
                obj.transform.SetParent(parent.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            spawnPool.Add(pool.tag, objectPool);
        }
    }

    //method to be called from Pooler, to "spawn" objects (set active)
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!spawnPool.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + "doesnt exist");
            return null;
        }

        GameObject objectToSpawn = spawnPool[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        spawnPool[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
