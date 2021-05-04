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

    private BoardController boardController;
    private StatsManager obj_Stats_Manager;

    private void Awake()
    {
        if (spawnonLoad)
        {
            PlaceController newPC = transform.GetComponentInParent<PlaceController>();
            boardController = (BoardController)FindObjectOfType(typeof(BoardController));
            newPC.baseSectionVariables = this;
            newPC.baseSections = this.gameObject;

            //Make the place grow
            StartCoroutine(newPC.VariablesResizing(1));

            Debug.Log("BoardController : Growing Section : " + name);
            boardController.activeSection = newPC;

            boardController.headingText.text = newPC.name;
            boardController.subheadingText.text = newPC.countryScore;

            //Load questions stats from JSON File
            obj_Stats_Manager = (StatsManager)FindObjectOfType(typeof(StatsManager));
            obj_Stats_Manager.Load(newPC.AssetbundleName);

            MainController.MCC.UpdateGameStatistics();
        }

    }
}
 