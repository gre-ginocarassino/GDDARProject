using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterScript;

public class CharacterParent : MonoBehaviour
{

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        DisableCharacter();
    //    }
    //}


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
            return false;
        }
        else
        {
            return false;
        }
    }
}
