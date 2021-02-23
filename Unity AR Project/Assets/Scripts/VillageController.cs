using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VillageController : PlaceController
{
    [Header("Village Specific Variables")]
    public bool[] villageUnlocked;
    //public GameObject[] villagePartsPrefabs;
    //public GameObject[] villageParts;

    public override void Awake()
    {
        base.Awake();
        boardController.villageController = this;
        Debug.Log("Setting Village Controller : " + boardController.villageController);

        //LoadPlace();
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void LoadPlace()
    {
        base.LoadPlace();

        for (int v = 0; v < baseSectionVariables.variables.Length - 2; v++)
        {
            if (villageUnlocked[v])
            {
                baseSectionVariables.variables[v + 1].SetActive(true);
            }
            else
            {
                baseSectionVariables.variables[v + 1].SetActive(true);
            }
        }
    }

    public override void ShrinkPlace()
    {
        base.ShrinkPlace();
    }
}
