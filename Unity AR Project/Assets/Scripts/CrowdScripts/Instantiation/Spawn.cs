using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

    public Transform poolerPosition;

    public Transform parent;

    public static Spawn _Spawn;


    //public CharacterBase characterBase;

    //public NavMeshSurface surface;
    //public NavMeshData[] data;
    //NavMeshDataInstance navMeshInstance = new NavMeshDataInstance();

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> spawnPool;


    private void Awake()
    {
        _Spawn = this;
        spawnPool = new Dictionary<string, Queue<GameObject>>();
       
    }
    public void PrepareCharacters()
    {
        //bake navmesh
        //surface.BuildNavMesh();
        
        //Debug.Log(surface);
        //navMeshInstance = NavMesh.AddNavMeshData(data[0]);
        //navMeshInstance.Remove();
                
        NavMeshHit closestHit;

        if (spawnPool == null)
        {
            spawnPool = new Dictionary<string, Queue<GameObject>>();
        }

       //Instantiate during initialisation
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.charTypes[Random.Range(0, pool.charTypes.Length)], poolerPosition.position, Quaternion.identity);
                obj.transform.SetParent(parent.transform);
                obj.GetComponent<NavMeshAgent>().speed = Random.Range(1.5f, 3f);
                obj.GetComponent<NavMeshAgent>().avoidancePriority = Random.Range(45, 70);
                obj.GetComponent<characterAI>().personality = (CharacterScript.characterTypes.Personality)Random.Range(-2,3);
                int h = Random.Range(0, obj.GetComponent<characterAI>().Hairstyles.Length);
                int a = Random.Range(0, obj.GetComponent<characterAI>().Accessories.Length);
                obj.GetComponent<characterAI>().Hairstyles[h].SetActive(true);
                obj.GetComponent<characterAI>().Accessories[a].SetActive(true);


                //obj.GetComponent<NavMeshAgent>().Warp(poolerPosition.transform.position);

                if (NavMesh.SamplePosition(poolerPosition.transform.position, out closestHit, 500, NavMesh.AllAreas))
                {
                    obj.transform.position = closestHit.position;

                    //obj.AddComponent<NavMeshAgent>().speed = Random.Range(1f, 3f);
                    //obj.GetComponent<NavMeshAgent>().radius = 0.09f;
                    //obj.GetComponent<NavMeshAgent>().height = 0.4f;
                }
                else
                {
                    Debug.LogError("could not find position on NavMesh");

                }

                //NavMeshAgent agentobj = obj.GetComponent<NavMeshAgent>();

                //if (!agentobj.isOnNavMesh)
                //{
                //    agentobj.transform.position = poolerPosition.transform.position;
                //    agentobj.enabled = false;
                //    agentobj.enabled = true;
                //    Debug.Log("agent not on navmesh");
                //}

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
        //    Debug.LogWarning("Pool with tag" + tag + "doesnt exist");
            return null;
        }
        
        GameObject objectToSpawn = spawnPool[tag].Dequeue(); //remove from item at the start of queue 

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;


        objectToSpawn.SetActive(true);
        
        //spawnPool[tag].Enqueue(objectToSpawn); //put item in the end of queue
        
        return objectToSpawn;
    }

    public void ReturnObjToQueue(GameObject obj, String tag)
    {
        spawnPool[tag].Enqueue(obj);
    }
}
