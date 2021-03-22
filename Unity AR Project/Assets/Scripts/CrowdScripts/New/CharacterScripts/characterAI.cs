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
        time = 5;

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
            animator.SetBool("shouldAnimating", true);

            switch (currentCharacterType) //switch from type to type
            {
                case CharacaterType.Tourist:
                    if (Vector3.Distance(transform.position, targetLocation.position) < 0.5)
                    {
                        currentCharacterType = CharacaterType.Wanderer;
                    }
                    break;
                case CharacaterType.Wanderer:
                    if (Vector3.Distance(transform.position, targetLocation.position) < 0.5)
                    {
                        time -= Time.deltaTime;
                        animator.SetBool("shouldAnimating", false);
                        if (time <= 0)
                        {
                            chooseLocation();
                            time = Random.Range(2,5);
                            int i = Random.Range(1, 11);
                            IsLeaderFormed = SetLeader.IfLeaderAvailable(i, false);
                            confirmLeader = CharacterParent.characterParent.ChooseLeader(IsLeaderFormed);
                            Debug.Log(i);
                            
                        }
                    }
                    if (confirmLeader == true)
                    {
                        currentCharacterType = CharacaterType.Leader;
                    }
                    break;
                case CharacaterType.Leader:
                    Debug.Log("yes formed leader");
                    break;
            }
        }
    }

    public void selectAction(int action)
    {
        switch (action)
        {
            case 0:
                currentCharacterType = CharacaterType.Tourist;
                chooseLocation();
                break;
            case 1:
                currentCharacterType = CharacaterType.Passenger;
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
