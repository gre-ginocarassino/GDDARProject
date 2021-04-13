using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.ARFoundation;

public class BoardController : MonoBehaviour
{
    [Header("Current")]
    public PlaceController activeSection;
    public string activeSectionName;
    //TODO: Prefabs -> Asset Bundles

    [Header("Loaded GameObjects")]
    public PlaceController villageController;
    public PlaceController englandController;
    public PlaceController franceController;
    public PlaceController italyController;

    private StatsManager obj_Stats_Manager;

    public bool isTesting;

    // Start is called before the first frame update
    void Start()
    {
        //arPlane.planePrefab.gameObject.GetComponent<MeshRenderer>().material = arPlane.planeMaterials[1];
        obj_Stats_Manager = (StatsManager)FindObjectOfType(typeof(StatsManager));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewSection(PlaceController newS)
    {
        if (newS == activeSection)
        {
            Debug.Log("Board Controller : Already an Active Section");
        } else
        {
            GrowSection(newS);
        }
    }

    /// <summary>
    /// Grows the New Section, Loads the section from the PlaceController but first shrinks the old section and sets the active section
    /// </summary>
    /// <param name="growingSection"></param>
    public void GrowSection(PlaceController growingSection)
    {
        StartCoroutine(NewSectionCoroutine(growingSection));
    }

    IEnumerator NewSectionCoroutine(PlaceController growingSection)
    {
        if (activeSection != null)
        {
            ShrinkSection(activeSection);
            Debug.Log("BoardController : Shrinking Section : " + activeSection);

            yield return new WaitForSeconds(1.4f);
        }

        growingSection.LoadPlace();
        Debug.Log("BoardController : Growing Section : " + growingSection);
        activeSection = growingSection;

        //Load questions stats from JSON File
        obj_Stats_Manager.Load(growingSection.AssetbundleName);
    }

    public void ShrinkSection(PlaceController shrinkingSection)
    {
        shrinkingSection.ShrinkPlace();
    }
}
