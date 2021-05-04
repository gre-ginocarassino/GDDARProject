using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlagController : MonoBehaviour
{
    [Header("Flags")]
    public GameObject flagHome;
    public GameObject flagEngland;
    public GameObject flagFrance;
    public GameObject flagItaly;
    public GameObject flagBulgaria;
    public GameObject flagChina;

    [Header("Flag Variables")]
    public Transform flagUp;
    public Transform flagDown;
    public GameObject currentRaisedFlag;
    public Animator flagAnim;


    private void Start()
    {
        flagAnim.enabled = true;
        //Lower all the flags
        //if (flagHome)
        //flagHome.transform.position = flagDown.position;
        //if (flagEngland)
        //    flagEngland.transform.position = flagDown.position;
        //if (flagFrance)
        //    flagFrance.transform.position = flagDown.position;
        //if (flagItaly)
        //    flagItaly.transform.position = flagDown.position;
        //if (flagBulgaria)
        //    flagBulgaria.transform.position = flagDown.position;
        //if (flagChina)
        //    flagChina.transform.position = flagDown.position;

        StartCoroutine(StartUp());
    }

    IEnumerator StartUp()
    {
        flagAnim.SetLayerWeight(0, 0.99f);

        yield return null;

        flagAnim.SetLayerWeight(0, 1f);
        LowerAllFlags();
    }

    void LowerAllFlags()
    {
        if (flagHome)
            flagHome.transform.position = flagDown.position;
        if (flagEngland)
            flagEngland.transform.position = flagDown.position;
        if (flagFrance)
            flagFrance.transform.position = flagDown.position;
        if (flagItaly)
            flagItaly.transform.position = flagDown.position;
        if (flagBulgaria)
            flagBulgaria.transform.position = flagDown.position;
        if (flagChina)
            flagChina.transform.position = flagDown.position;
    }

    void LowerFlag()
    {
        currentRaisedFlag.transform.DOMove(flagDown.position, 0.7f);
    }

    void RaiseFlag(GameObject newFlag)
    {
        currentRaisedFlag = newFlag;
        currentRaisedFlag.transform.DOMove(flagUp.position, 0.7f);
    }

    public IEnumerator SelectNewFlag(GameObject newFlag)
    {
        if (currentRaisedFlag)
        {
            LowerFlag();
        }

        yield return new WaitForSeconds(1);
        LowerAllFlags();

        RaiseFlag(newFlag);
    }
}
