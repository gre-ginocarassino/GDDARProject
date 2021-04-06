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

        IsGroupFormed = FormGroup.IfGroupFormable(TotalCohesion); //call a method of type bool to return either false or true

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
<<<<<<< Updated upstream
                            //Debug.Log(i);
                            
=======
                            Debug.Log(i);
>>>>>>> Stashed changes
                        }
                    }
                    if (confirmLeader == true)
                    {
                        currentCharacterType = CharacaterType.Leader;
                    }
                    break;
                case CharacaterType.Leader:
                    //Debug.Log("yes formed leader");
                    break;
            }
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
                Spawn._Spawn.ReturnObjToQueue(this.gameObject, "walker");
            }
        }
    }

    private void chooseLocation()
    {
        targetLocation = AttrationPoints.attrationPoints.allDirection[Random.Range(0, AttrationPoints.attrationPoints.allDirection.Length)].transform;
    }


}
