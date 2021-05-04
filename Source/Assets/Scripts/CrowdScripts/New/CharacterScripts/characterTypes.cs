using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CharacterScript
{
    public class characterTypes : MonoBehaviour
    {
        [Header ("Hairstyles and accessories")]
        public GameObject[] Hairstyles;
        public GameObject[] Accessories;
        [Header("Group Cohesion")]
        public int cohesion;
        public int TotalCohesion;

        [Header("TargetLocation")]
        public Transform targetLocation;

        //Personalities
        public enum Personality : int { Tense = -2, Enthusiastic = 2, Shy = -1, Kind = 1, Indifferent = 0 }
        public enum CharacaterType { Passenger, Tourist, Wanderer, Leader, Follower }

        [Header("Personality")]
        public Personality personality;

        [Header("Character Type")]
        public CharacaterType currentCharacterType;

        //Declare NavmeshAgent, NavmeshPath and Animator
        protected NavMeshAgent navmeshAgent;
        protected NavMeshPath navmeshPath;
        protected Animator animator;

        protected virtual void Start()
        {
            navmeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            navmeshPath = new NavMeshPath();

            if (targetLocation == null)
            {
                targetLocation = endNode._endNode.nodePoint.transform;
            }

            cohesion = (int)personality;
            personality = (Personality)cohesion;

        }

        protected virtual void Update()
        {
            
            if (navmeshAgent.hasPath == false)
            {
                NavMesh.CalculatePath(transform.position, targetLocation.position, NavMesh.AllAreas, navmeshPath);
            }
            //Debug.Log(navmeshAgent.navMeshOwner);
            ////the outcome is false in phone 
            //Debug.Log(navmeshAgent.hasPath + " for path");
            //Debug.Log(navmeshAgent.isOnNavMesh + " is on navmesh?");

            //if (navmeshPath.status == NavMeshPathStatus.PathComplete)
            //    Debug.Log("Path complete");
            //else if (navmeshPath.status == NavMeshPathStatus.PathInvalid)
            //    Debug.Log("Path invalid");
            //else if (navmeshPath.status == NavMeshPathStatus.PathPartial)
            //    Debug.Log("Path partial");

            //Debug.Log(targetLocation + "null or yes");
        }

    }
}

