using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBase : MonoBehaviour
{
    //variables affecting the agent
    private float waitTime;
    public float startWaitTime;
    public float speed;
    

    //all waypoints
    public Transform[] moveLocation;
    private int randomLocation;

    NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        waitTime = startWaitTime;
        _navMeshAgent.speed = speed;
        randomLocation = Random.Range(0, moveLocation.Length);

    }

    private void Update()
    {
        //call the method SetDestination in Navmeshagent to move towards it
        Vector3 targetLocation = moveLocation[randomLocation].position;
        _animator.SetBool("shouldAnimating", true);
        _navMeshAgent.SetDestination(targetLocation);

        //if distance between the current location and destination is lower than 0.2, output following
        if (Vector3.Distance(transform.position, moveLocation[randomLocation].position) < 0.2f)
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


}
