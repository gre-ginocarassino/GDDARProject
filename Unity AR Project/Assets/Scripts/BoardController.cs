using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardController : MonoBehaviour
{
    public PlaceController activeSection;

    //TODO: Prefabs -> Asset Bundles

    [Header("Loaded GameObjects")]
    public PlaceController villageController;
    public PlaceController bigBenController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewSection(PlaceController newS)
    {
        GrowSection(newS);

        if (activeSection != null)
            ShrinkSection(activeSection);

        activeSection = newS;
    }


    public void GrowSection(PlaceController growingSection)
    {
        StartCoroutine(NewSectionCoroutine(growingSection));
    }

    IEnumerator NewSectionCoroutine(PlaceController growingSection)
    {
        if (activeSection != null)
        {
            ShrinkSection(activeSection);

            yield return new WaitForSeconds(1.2f);
        }

        growingSection.LoadPlace();
    }

    public void ShrinkSection(PlaceController shrinkingSection)
    {
        shrinkingSection.ShrinkPlace();
    }
}
