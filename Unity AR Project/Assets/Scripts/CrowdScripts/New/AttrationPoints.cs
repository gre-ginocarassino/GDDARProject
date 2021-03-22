using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttrationPoints : MonoBehaviour
{
    public static AttrationPoints attrationPoints;

    private void Awake()
    {
        attrationPoints = this;
    }
    public Transform[] allDirection;
}
