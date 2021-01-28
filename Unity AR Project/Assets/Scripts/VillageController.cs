using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VillageController : PlaceController
{
    [Header("Village Specific Variables")]
    public bool[] villageUnlocked;
    public GameObject[] villagePartsPrefabs;
    public GameObject[] villageParts;

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

        StartCoroutine(VillageGrower());
    }

    IEnumerator VillageGrower()
    {
        yield return new WaitForSeconds(0.2f);

        //check to see which village sections are unlocked
        for (int v = 0; v < villageParts.Length; v++)
        {
            if (villageUnlocked[v])
            {
                if (villageParts[v] == null)
                {
                    villageParts[v] = Instantiate(villagePartsPrefabs[v], transform);
                }

                //This village section is unlocked 
                //villageParts[v].SetActive(true);
                villageParts[v].transform.DOScaleY(1, 1);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    public override void ShrinkPlace()
    {
        base.ShrinkPlace();

        StartCoroutine(VillageShrinker());
    }

    IEnumerator VillageShrinker()
    {
        yield return new WaitForSeconds(0.2f);

        //check to see which village sections are unlocked
        for (int v = 0; v < villageParts.Length; v++)
        {
            villageParts[v].transform.DOScaleY(0, 1);
        }

        //yield return new WaitForSeconds(1);

        //for (int v = 0; v < villageParts.Length; v++)
        //{
        //    villageParts[v].SetActive(false);
        //}
    }
}
