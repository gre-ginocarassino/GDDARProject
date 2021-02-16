using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class ContinentPicker : MonoBehaviour
{
    [FormerlySerializedAs("Continents")] public List<GameObject> continents;
    [FormerlySerializedAs("RemovedContinentAfterCLick")] public List<GameObject> removedContinentAfterCLick;
    
}

