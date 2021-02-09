using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPoints : MonoBehaviour
{
    public wayPoints previousWaypoint;
    public wayPoints nextWaypoint;

    [Range(0f, 5f)] //between 0 and 5
    public float width = 1f;

    public Vector3 GetPosition() //method to be called to return a random point between waypoint width
    {
        Vector3 minBound = transform.position + transform.right * width / 2f;
        Vector3 maxBound = transform.position - transform.right * width / 2f;

        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }
}
