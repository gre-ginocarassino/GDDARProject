using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    //public float speed;
   
    //time to wait until next random location selected
    private float waitTime;
    public float startWaitTime;

    //all waypoints
    public Transform[] moveLocation;
    private int randomLocation;

    NavMeshAgent _navMeshAgent;
    private void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        waitTime = startWaitTime;
        randomLocation = Random.Range(0, moveLocation.Length);

    }

    private void Update()
    {
        //call the method SetDestination in Navmeshagent to move towards it
        Vector3 targetLocation = moveLocation[randomLocation].position;
        _navMeshAgent.SetDestination(targetLocation);

        //if distance between the current location and destination is lower than 0.2, output following
        if (Vector3.Distance(transform.position, moveLocation[randomLocation].position) < 0.2f)
        {
            //if waittimes is less than or equal to zero
            if (waitTime <= 0)
            {
                //choose random waypoint and refill waittime
                randomLocation = Random.Range(0, moveLocation.Length);
                waitTime = startWaitTime;
            }
            //otherwise, reduce waittime by time.deltatime
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

    }
}
