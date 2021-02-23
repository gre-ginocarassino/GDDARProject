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

    private float randomDistance;

    private float randomTime;


    NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        waitTime = startWaitTime;
        randomLocation = Random.Range(0, moveLocation.Length);
        randomDistance = Random.Range(0, 1f);
        randomTime = Random.Range(3, 7f);
    }

    private void Update()
    {
        //call the method SetDestination in Navmeshagent to move towards it
        Vector3 targetLocation = moveLocation[randomLocation].position;
        _animator.SetBool("shouldAnimating", true);
        _navMeshAgent.SetDestination(targetLocation);

        if (randomTime <= 0)
        {
            chooseAnotherRoute();
        }

        else
        {
            randomTime -= Time.deltaTime;
        }

        //if distance between the current location and destination is lower than 0.2, output following
        if (Vector3.Distance(transform.position, moveLocation[randomLocation].position) < randomDistance)
        {
            _animator.SetBool("shouldAnimating", false);
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

    private void chooseAnotherRoute()
    {
        randomLocation = Random.Range(0, moveLocation.Length);
        randomDistance = Random.Range(0, 1f);
        randomTime = Random.Range(3, 7f);
    }
}
