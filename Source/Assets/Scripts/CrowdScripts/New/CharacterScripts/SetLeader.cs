using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLeader
{
    public static bool IfLeaderAvailable(int randomChance, bool hasLeader)
    {
        if (hasLeader == false)
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
        else
        {
            return false;
        }
    }
}
