using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementVariables : MonoBehaviour
{
    [Header("Must Contain 7 Exactly")]
    [Tooltip("0 = Foliage, 1 - 5 = BaseTown, 6 = Landmarks")]
    public GameObject[] variables;
    public navigationSign CountrySign;

    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip musicClip;

    public bool spawnonLoad;

    private void Awake()
    {
        if (spawnonLoad)
        {
            PlaceController newPC = transform.GetComponentInParent<PlaceController>();

            newPC.baseSectionVariables = this;
            newPC.baseSections = this.gameObject;

            //Make the place grow
            StartCoroutine(newPC.VariablesResizing(1));
        }

    }
}
 