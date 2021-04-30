using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterScript;
public class characterAI : characterTypes
{
    public bool IsLeaderFormed;
    public bool confirmLeader;
    public bool IsGroupFormed;
    public float time;
    public float distanceFromPoint;
    public int FollowerThreshold;

    public float GroupTime;
    
    public enum CurrentStatus { Solo, InGroup}
    public enum SoloAction { Idling, Wandering}
    public enum GroupAction { Following, Leading}

    [Header("Action and Status")]
    //public SoloAction currentSoloAction;
    //public GroupAction currentGroupAction;
    public CurrentStatus currentStatus;
    protected override void Start()
    {
        base.Start();
        time = 4;
        distanceFromPoint = 1;

        currentStatus = CurrentStatus.Solo;
        currentCharacterType = CharacaterType.Passenger;
    }

    protected override void Update()
    {
        base.Update();

        //IsGroupFormed = FormGroup.IfGroupFormable(TotalCohesion); //call a method of type bool to return either false or true

        if (IsGroupFormed == false)
        {
            currentStatus = CurrentStatus.Solo;
        }
        else
        {
            currentStatus = CurrentStatus.InGroup;
        }

        if (currentStatus == CurrentStatus.Solo)
        {
            navmeshAgent.SetDestination(targetLocation.position);
            //navmeshAgent.destination = targetLocation.position;
            animator.SetBool("StartWalking", true);

            switch (currentCharacterType) //switch from type to type
            {
                case CharacaterType.Tourist:
                    if (Vector3.Distance(transform.position, targetLocation.position) < distanceFromPoint)
                    {
                        currentCharacterType = CharacaterType.Wanderer;
                    }
                    break;
                case CharacaterType.Wanderer:
                    if (Vector3.Distance(transform.position, targetLocation.position) < distanceFromPoint)
                    {
                        time -= Time.deltaTime;
                        animator.SetBool("StartWalking", false);
                        if (time <= 0)
                        {
                            chooseLocation();
                            time = Random.Range(1,4);
                            int i = Random.Range(1, 11);
                            IsLeaderFormed = SetLeader.IfLeaderAvailable(i, false);
                            confirmLeader = CharacterParent.characterParent.ChooseLeader(IsLeaderFormed);
                            //Debug.Log(i);
                            
                        }
                    }
                    if (confirmLeader == true)
                    {
                        currentCharacterType = CharacaterType.Leader;
                    }
                    break;
                case CharacaterType.Leader:
                    List<GameObject> FollowerList = new List<GameObject>();
                    navmeshAgent.avoidancePriority = 90;
                    foreach (Transform t in CharacterParent.characterParent.transform)
                    {
                        if (FollowerThreshold <= 5)
                        {
                            if (Vector3.Distance(transform.position, t.position) < 3f)
                            {
                                if (t.GetComponent<characterAI>().currentCharacterType == CharacaterType.Wanderer)
                                {
                                    TotalCohesion += cohesion + t.GetComponent<characterAI>().cohesion;
                                    if (TotalCohesion >= 0)
                                    {
                                        FollowerThreshold++;
                                        FollowerList.Add(t.gameObject);
                                        TotalCohesion = 0;
                                    }
                                    else //This part needs to be redone to make it work
                                    {
                                        TotalCohesion = 0;
                                    }
                                    //IsGroupFormed = FormGroup.IfGroupFormable(TotalCohesion);
                                    //if (IsGroupFormed == true)
                                    //{
                                    //    t.GetComponent<characterAI>().currentCharacterType = CharacaterType.Follower;
                                    //}
                                }
                            }
                        }
                        else
                        {
                            if (IsGroupFormed == true)
                            {
                                GroupTime = TotalCohesion * 2; //this is in update , should not be here 
                                if (GroupTime >= 1)
                                {
                                    GroupTime -= 1 * Time.deltaTime;
                                }
                                if (GroupTime <= 0)
                                {
                                    TotalCohesion = 0;
                                    FollowerThreshold = 0;
                                    currentCharacterType = CharacaterType.Wanderer;
                                    confirmLeader = false;
                                }
                            }
                            else
                            {
                                TotalCohesion = 0;
                                FollowerThreshold = 0;
                                currentCharacterType = CharacaterType.Wanderer;
                                CharacterParent.characterParent.count = 0;
                                confirmLeader = false;
                            }
                        }
                    }
                    if (Vector3.Distance(transform.position, targetLocation.position) < distanceFromPoint)
                    {
                        time -= Time.deltaTime;
                        animator.SetBool("StartWalking", false);
                        if (time <= 0)
                        {
                            chooseLocation();
                            time = Random.Range(1, 4);
                        }
                    }
                    break;
                case CharacaterType.Follower:
                    navmeshAgent.avoidancePriority = 80;
                    foreach (Transform t in CharacterParent.characterParent.transform)
                    {
                        if (t.GetComponent<characterAI>().currentCharacterType == CharacaterType.Leader)
                        {
                            targetLocation = t.transform;
                        }
                    }
                    if (Vector3.Distance(transform.position, targetLocation.position) < distanceFromPoint)
                    {
                        animator.SetBool("StartWalking", false);
                    }
                    break;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (currentCharacterType == CharacaterType.Leader)
        {
            Gizmos.DrawWireSphere(this.transform.position, 3f);
        }
    }

    public void selectAction(int action)
    {
        switch (action)
        {
            case 0:
                if (currentCharacterType == CharacaterType.Passenger)
                {
                    currentCharacterType = CharacaterType.Tourist;
                    chooseLocation();
                }
                break;
            case 1:
                if (currentCharacterType == CharacaterType.Passenger)
                {
                    currentCharacterType = CharacaterType.Passenger;
                }
                break;
        }
    }


    public void SetInactive()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable() //gameobject ondiasble will be returned to queue
    {
        if (currentCharacterType == CharacaterType.Passenger)
        {
            if (Spawn._Spawn != null)
            {
                Spawn._Spawn.ReturnObjToQueue(this.gameObject, "Walker");
            }
        }
        if (currentCharacterType == CharacaterType.Leader)
        {
            if (Spawn._Spawn != null)
            {
            }
        }
    }

    private void chooseLocation()
    {
        targetLocation = AttrationPoints.attrationPoints.allDirection[Random.Range(0, AttrationPoints.attrationPoints.allDirection.Length)].transform;
    }


}
