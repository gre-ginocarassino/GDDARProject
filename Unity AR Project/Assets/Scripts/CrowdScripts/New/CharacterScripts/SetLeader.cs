using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLeader : MonoBehaviour
{
    public static bool IfLeaderAvailable(int randomChance)
    {
        if (randomChance == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
