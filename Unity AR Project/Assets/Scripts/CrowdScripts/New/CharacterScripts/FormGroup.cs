using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FormGroup
{
    public static bool IfGroupFormable(int TotalCohesion)
    {
        if (TotalCohesion > 2)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
