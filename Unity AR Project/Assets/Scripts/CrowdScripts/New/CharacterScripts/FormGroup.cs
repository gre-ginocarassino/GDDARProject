using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FormGroup
{
    public static float DetermineGrouptime(int TotalCohesion)
    {
        if (TotalCohesion == 0)
        {
            return 5;
        }
        else
        {
            return (float)TotalCohesion + 5f;
        }
        
    }
}
