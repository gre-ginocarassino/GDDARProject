using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VillageController : PlaceController
{
    [Header("Italy Variables")]
    public bool italyUnlocked;
    public GameObject italyMonument;
    public GameObject italyLockedObject;

    [Header("England Variables")]
    public bool englandUnlocked;
    public GameObject englandMonument;
    public GameObject englandLockedObject;

    [Header("France Variables")]
    public bool franceUnlocked;
    public GameObject franceMonument;
    public GameObject franceLockedObject;
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

        italyUnlocked = MainController.MCC.totalItaly >= 200 ? true : false;
        englandUnlocked = MainController.MCC.totalEngland >= 200 ? true : false;
        franceUnlocked = MainController.MCC.totalFrance >= 200 ? true : false;
        


        //TODO: Gino we need to connect these bools to your cloud.
        if (italyUnlocked)
        {
            italyMonument.SetActive(true);
            italyLockedObject.SetActive(false);
        }
        else
        {
            italyMonument.SetActive(false);
            italyLockedObject.SetActive(true);
        }

        if (englandUnlocked)
        {
            englandMonument.SetActive(true);
            englandLockedObject.SetActive(false);
        }
        else
        {
            englandMonument.SetActive(false);
            englandLockedObject.SetActive(true);
        }

        if (franceUnlocked)
        {
            franceMonument.SetActive(true);
            franceLockedObject.SetActive(false);
        }
        else
        {
            franceMonument.SetActive(false);
            franceLockedObject.SetActive(true);
        }
    }

    public override void ShrinkPlace()
    {
        base.ShrinkPlace();
    }
}
