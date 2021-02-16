using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RandomAnswersList
{
    public static List<TE> ShuffleListItems<TE>(List<TE> inputList)
    {
        List<TE> initialList = new List<TE>();
        initialList.AddRange(inputList);
        List<TE> randomList = new List<TE>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (initialList.Count > 0)
        {
            randomIndex = r.Next(0, initialList.Count); //Choose a random object in the list
            randomList.Add(initialList[randomIndex]); //add it to the new, random list
            initialList.RemoveAt(randomIndex); //remove to avoid duplicates
        }

        return randomList; //return the new random list
    }
}
