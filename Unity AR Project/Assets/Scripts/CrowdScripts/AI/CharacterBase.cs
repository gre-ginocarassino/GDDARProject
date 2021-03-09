using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBase : MonoBehaviour
{
    //time to wait until next random location selected
    private float waitTime;
    public float startWaitTime;

    //target location and variables for random range
    public Transform targetLocation;
    private int randomLocation;

    private float randomDistance;

    private float randomTime;

    //Navmesh and animator
    NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private NavMeshPath path;
    private void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        if (targetLocation == null)
        {
            targetLocation = endNode._endNode.nodePoint.transform;
        }
        //randomLocation = Random.Range(0, Spawn._Spawn.allDirection.Length);
        //randomDistance = Random.Range(0, 1f);
        //randomTime = Random.Range(3, 7f);

        path = new NavMeshPath();
    }

    private void Update()
    {
        if (_navMeshAgent.hasPath == true)
        {
            //Debug.LogError("yes it has path");
        }
        else
        {
            NavMesh.CalculatePath(transform.position, targetLocation.position, NavMesh.AllAreas, path);
            //Debug.LogError("no it does not have path");
        }

        _navMeshAgent.SetDestination(targetLocation.position);
        _animator.SetBool("shouldAnimating", true);

        //if (randomTime <= 0)
        //{
        //    chooseAnotherRoute();
        //}

        //else
        //{
        //    randomTime -= Time.deltaTime;
        //}
        ////if distance less random distance, execute following lines
        //if (Vector3.Distance(transform.position, Spawn._Spawn.allDirection[randomLocation].position) <= randomDistance)
        //{
        //    _animator.SetBool("shouldAnimating", false);

        //    if (waitTime <= 0)
        //    {
                
        //        randomLocation = Random.Range(0, Spawn._Spawn.allDirection.Length);
        //        waitTime = startWaitTime;
        //    }
            
        //    else
        //    {
        //        waitTime -= Time.deltaTime;
        //    }
        //}

        
    }

    //private void chooseAnotherRoute()
    //{
    //    randomLocation = Random.Range(0, Spawn._Spawn.allDirection.Length);
    //    randomDistance = Random.Range(0, 1f);
    //    randomTime = Random.Range(3, 7f);
    //}

    public void chooseAction(int action)
    {
        switch (action)
        {
            case 0:
                targetLocation = Spawn._Spawn.allDirection[Random.Range(0, Spawn._Spawn.allDirection.Length)].transform;

                break;
            case 1: 
                break;
        }
    }

    public void setInactive()
    {
        gameObject.SetActive(false);

        
    }

    

}
