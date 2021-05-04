using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endNode : MonoBehaviour
{
    public static endNode _endNode;

    private void Awake()
    {
        _endNode = this;
    }

    public Transform nodePoint;
}
