using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterScript;

public class CharacterParent : MonoBehaviour
{

    public int leaderCount;
    public int count;
    public static CharacterParent characterParent;


    private void Start()
    {
        characterParent = this;
        count = 0;
    }

    public void DisableCharacter()
    {
        foreach (Transform eachChild in transform)
        {
            eachChild.gameObject.SetActive(false);
        }
    }

    public bool ChooseLeader(bool hasFormed)
    {
        if (hasFormed == true)
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<characterAI>().currentCharacterType == characterTypes.CharacaterType.Wanderer && count < 1)
                {
                    count++;
                    return true; //true to form leader
                }
            }
            checkIfLeaderexist();
            return false;
        }
        else
        {
            return false;
        }
    }

    private void checkIfLeaderexist()
    {
        foreach (Transform child in transform)
        {
            //if the child equal to leader
            if (child.GetComponent<characterAI>().currentCharacterType == characterTypes.CharacaterType.Leader)
            {
                leaderCount++;
            }
        }
        //no leader
        confirmExistence(leaderCount);
    }

    public void confirmExistence(int leaderCount)
    {
        if (leaderCount <= 0)
        {
            count = 0;
        }
    }
}
