using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAround : MonoBehaviour
{
    public int numObjects;
    public GameObject walker;

    public float radius;

    void Start()
    {

        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 pos = RandomCircle(center,radius);
            Instantiate(walker, pos, Quaternion.identity);
        }
    }

    Vector3 RandomCircle(Vector3 center,float _radius)
    {
        Vector3 pos;
        pos.x = Random.Range(-_radius, _radius);
        pos.y = center.y;
        pos.z = Random.Range(-_radius, _radius);

        return pos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
