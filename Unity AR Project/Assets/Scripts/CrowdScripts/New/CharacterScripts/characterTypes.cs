using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CharacterScript
{
    public class characterTypes : MonoBehaviour
    {
        [Header("Group Cohesion")]
        public int cohesion;
        public int TotalCohesion;

        [Header("TargetLocation")]
        public Transform targetLocation;

        //Traits and Types of the character
        public enum Personality : int { Tense = -2, Enthusiastic = 2, Shy = -1, Kind = 1, Indifferent = 0 }
        public enum CharacaterType { Passenger, Tourist, Wanderer, Leader, Follower }

        [Header("Trait and CharacterType")]
        public Personality personality;
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

        }

    }
}

