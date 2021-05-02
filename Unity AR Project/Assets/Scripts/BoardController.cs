using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.ARFoundation;
using TMPro;

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

    [Header("Board Variables")]
    public TMP_Text headingText;
    public TMP_Text subheadingText;
    public TMP_Text populationText;

    [Header("Music")]
    public ParticleSystem musicEffect;

    [Header("True Loads from Game, False Loads from Cloud")]
    public bool isTesting;

    [Header("Check if it has been spawned")]
    public bool Spawned;

    // Start is called before the first frame update
    void Start()
    {
        //arPlane.planePrefab.gameObject.GetComponent<MeshRenderer>().material = arPlane.planeMaterials[1];
        obj_Stats_Manager = (StatsManager)FindObjectOfType(typeof(StatsManager));

        headingText.text = "";
        subheadingText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (activeSection != null)
            populationText.text = "Population : " + activeSection.baseSectionVariables.CountrySign.threshold.ToString();
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
        headingText.text = "";
        subheadingText.text = "";

        if (activeSection != null)
        {
            ShrinkSection(activeSection);
            Debug.Log("BoardController : Shrinking Section : " + activeSection);

            yield return new WaitForSeconds(1.4f);
        }

        if (Spawned == false)
        {
            Spawn._Spawn.PrepareCharacters();
            Spawned = true;
        }
        growingSection.LoadPlace();

        Debug.Log("BoardController : Growing Section : " + growingSection);
        activeSection = growingSection;

        headingText.text = growingSection.name;
        subheadingText.text = growingSection.countryScore;

        //Load questions stats from JSON File
        obj_Stats_Manager.Load(growingSection.AssetbundleName);

        MainController.MCC.UpdateGameStatistics();
    }

    public void ShrinkSection(PlaceController shrinkingSection)
    {
        shrinkingSection.ShrinkPlace();
    }
}
