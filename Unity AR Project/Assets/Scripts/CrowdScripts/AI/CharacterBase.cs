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
        //targetLocation = endNode._endNode.nodePoint.transform;
        _animator = GetComponent<Animator>();

        if (targetLocation == null)
        {
            targetLocation = endNode._endNode.nodePoint.transform;
        }

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
    }

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
