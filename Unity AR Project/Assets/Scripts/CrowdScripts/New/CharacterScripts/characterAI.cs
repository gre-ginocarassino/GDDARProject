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
    public float countdown;
    private float startCountdown;
    public float distanceFromPoint;
    public int FollowerThreshold;

    private bool GTTriggered;
    public float GroupTime;

    private bool leaderPrepared;

    public List<GameObject> FollowerList;
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
        startCountdown = 9;
        time = 4;
        countdown = 4;
        distanceFromPoint = 2;

        currentStatus = CurrentStatus.Solo;
        currentCharacterType = CharacaterType.Passenger;

        FollowerList = new List<GameObject>();
    }

    protected override void Update()
    {
        base.Update();

        //IsGroupFormed = FormGroup.IfGroupFormable(TotalCohesion); //call a method of type bool to return either false or true

        //if (IsGroupFormed == false)
        //{
        //    currentStatus = CurrentStatus.Solo;
        //}
        //else
        //{
        //    currentStatus = CurrentStatus.InGroup;
        //}

        if (currentStatus == CurrentStatus.Solo)
        {
            navmeshAgent.stoppingDistance = 0.1f;
            navmeshAgent.SetDestination(targetLocation.position);
            //navmeshAgent.destination = targetLocation.position;
            animator.SetBool("StartWalking", true);

            switch (currentCharacterType) //switch from type to type
            {
                case CharacaterType.Passenger:
                    targetLocation = endNode._endNode.nodePoint.transform;
                    break;
                case CharacaterType.Tourist:
                    if (Vector3.Distance(transform.position, targetLocation.position) < distanceFromPoint)
                    {
                        currentCharacterType = CharacaterType.Wanderer;
                    }
                    else
                    {
                        countdown -= Time.deltaTime;
                    }
                    if (countdown <= 0)
                    {
                        chooseLocation();
                        countdown = startCountdown;
                    }
                    break;
                case CharacaterType.Wanderer:
                    if (targetLocation == null)
                    {
                        chooseLocation();
                        navmeshAgent.avoidancePriority = Random.Range(45, 70);
                    }
                    if (Vector3.Distance(transform.position, targetLocation.position) < distanceFromPoint)
                    {
                        time -= Time.deltaTime;
                        animator.SetBool("StartWalking", false);
                        if (time <= 0)
                        {
                            chooseLocation();
                            time = Random.Range(1,4);
                            int i = Random.Range(1, 4);
                            IsLeaderFormed = SetLeader.IfLeaderAvailable(i, false);
                            confirmLeader = CharacterParent.characterParent.ChooseLeader(IsLeaderFormed);
                            //Debug.Log(i);
                            
                        }
                    }
                    else
                    {
                        countdown -= Time.deltaTime;
                    }
                    if (countdown <= 0)
                    {
                        chooseLocation();
                        countdown = startCountdown;
                    }
                    if (confirmLeader == true)
                    {
                        currentCharacterType = CharacaterType.Leader;
                        confirmLeader = false;
                    }
                    else
                    {
                        IsLeaderFormed = false;
                    }
                    break;
                case CharacaterType.Leader:
                    navmeshAgent.avoidancePriority = 90;
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
                    else
                    {
                        countdown -= Time.deltaTime;
                    }
                    if (countdown <= 0)
                    {
                        chooseLocation();
                        countdown = startCountdown;
                    }
                    foreach (Transform t in CharacterParent.characterParent.transform)
                    {
                        if (FollowerThreshold <= 5 && Vector3.Distance(transform.position, t.position) < 3f)
                        {
                            if (t.GetComponent<characterAI>().currentCharacterType == CharacaterType.Wanderer)
                            {
                                TotalCohesion = TotalCohesion + t.GetComponent<characterAI>().cohesion;
                                FollowerThreshold++;
                                FollowerList.Add(t.gameObject);
                                if (FollowerList.Count >= 6)
                                {
                                    leaderPrepared = true;
                                }
                            }
                        }
                    }
                    //Yes, form group
                    if (TotalCohesion >= 0 && leaderPrepared == true)
                    {
                        if (GTTriggered != true)
                        {
                            GroupTime = FormGroup.DetermineGrouptime(TotalCohesion);
                            selectAction(2);
                            GTTriggered = true;
                        }
                        if (GroupTime >= 0)
                        {
                            GroupTime -= Time.deltaTime;
                        } 
                        else
                        {
                            selectAction(3);
                            FollowerList.Clear();
                            currentCharacterType = CharacaterType.Wanderer;
                            TotalCohesion = 0;
                            FollowerThreshold = 0;
                            leaderPrepared = false;
                            GTTriggered = false;
                            //say that there is no leader anymore
                            CharacterParent.characterParent.confirmExistence(0);
                        }
                    }
                    //No, become a wanderer
                    else if (TotalCohesion < 0 && leaderPrepared == true)
                    {
                        leaderPrepared = false;
                        GTTriggered = false;
                        FollowerList.Clear();
                        currentCharacterType = CharacaterType.Wanderer;
                        TotalCohesion = 0;
                        FollowerThreshold = 0;
                        //say that there is no leader anymore
                        CharacterParent.characterParent.confirmExistence(0);
                    }
                    break;
                case CharacaterType.Follower:
                    navmeshAgent.avoidancePriority = 70;
                    //check for leader
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
            case 2: //For the leader to make followers
                foreach (GameObject follower in FollowerList)
                {
                    follower.GetComponent<characterAI>().currentCharacterType = CharacaterType.Follower;
                }
                break;
            case 3: //For the leader to remove followers
                foreach (GameObject follower in FollowerList)
                {
                    follower.GetComponent<characterAI>().currentCharacterType = CharacaterType.Wanderer;
                }
                break;
        }
    }


    public void SetInactive()
    {
        if (currentCharacterType == CharacaterType.Passenger)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnDisable()
    {
        if (Spawn._Spawn.prepared != false)
        {
            if (Spawn._Spawn != null)
            {
                currentCharacterType = CharacaterType.Passenger;
                Spawn._Spawn.ReturnObjToQueue(this.gameObject, "Walker");
            }
        }
    }

    private void chooseLocation()
    {
        targetLocation = AttrationPoints.attrationPoints.allDirection[Random.Range(0, AttrationPoints.attrationPoints.allDirection.Length)].transform;
    }


}
