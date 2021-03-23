using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaceController : MonoBehaviour
{
    public BoardController boardController;
    private AssetBundleAzure az;
    private FlagController flagCont;

    public string AssetbundleName;
    public string AssetName;

    [Header("Base Variables")]
    public GameObject baseSectionsPrefabs;
    public GameObject baseSections;
    public PlacementVariables baseSectionVariables;
    public GameObject countryFlag;

    [Header("Inside Variables")]
    List<GameObject> roads = new List<GameObject>();

    private bool isTesting;

    public virtual void Awake()
    {
        //setting the variables we need as this object is going to be spawned at runtime :)
        boardController = (BoardController)FindObjectOfType(typeof(BoardController));
        isTesting = boardController.isTesting;
        az = (AssetBundleAzure)FindObjectOfType(typeof(AssetBundleAzure));
        flagCont = (FlagController)FindObjectOfType(typeof(FlagController));
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Just here for testing
        //LoadPlace();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public virtual void LoadPlace()
    {
        //boardController.activeSection = this;

        if (baseSections == null)
        {
            if (isTesting)
            {
                baseSections = Instantiate(baseSectionsPrefabs, transform);

                if (baseSectionVariables == null)
                {
                    baseSectionVariables = baseSections.GetComponent<PlacementVariables>();
                }
                baseSections.SetActive(true);
                StartCoroutine(VariablesResizing(1));
            } else
            {
                StartCoroutine(downloader());
            }      
        }

        StartCoroutine(flagCont.SelectNewFlag(countryFlag));

    }

    IEnumerator downloader()
    {
        az.assetBundleName = AssetbundleName;
        az.assetName = AssetName;

        az.TappedLoadAssetBundle(this);

        yield return new WaitForSeconds(1);

        baseSectionVariables = transform.GetComponentInChildren<PlacementVariables>();
        baseSections = baseSectionVariables.gameObject;

        //Make the place grow

        if (baseSectionVariables == null)
        {
            baseSectionVariables = baseSections.GetComponent<PlacementVariables>();
        }
        baseSections.SetActive(true);
        StartCoroutine(VariablesResizing(1));
    }

    public virtual void ShrinkPlace()
    {
        if (baseSectionVariables == null)
        {
            baseSections.GetComponent<PlacementVariables>();
        }



        //baseSections.transform.DOScaleY(0, 1);
        StartCoroutine(VariablesResizing(0));
    }

    IEnumerator VariablesResizing(int newScale)
    {
        yield return new WaitForSeconds(0.5f);

        //If the new scale is = 1, it's getting bigger, if it's not
        //it's getting smaller.
        if (newScale == 1)
        {
            foreach (GameObject i in baseSectionVariables.variables)
            {
                i.SetActive(true);

                //i.transform.DOScaleY(newScale + 0.2f, 0.5f);
                //yield return new WaitForSeconds(0.5f);
                //i.transform.DOScaleY(newScale - 0.1f, 0.1f);
                //yield return new WaitForSeconds(0.1f);
                //i.transform.DOScaleY(newScale, 0.1f);
            }

            if (roads.Count == 0)
            {
                foreach (Transform child in baseSectionVariables.variables[0].transform)
                {
                    roads.Add(child.gameObject);
                }
            }
            else
            {
                //DoNothing
            }

            baseSectionVariables.variables[0].transform.DOScaleY(newScale, 0);
            StartCoroutine(RoadAnimation(newScale));

            //baseSectionVariables.variables[0].transform.DOScaleY(newScale + 0.2f, 0.5f);
            //yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[1].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[2].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[3].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[4].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[5].transform.DOScaleY(newScale + 0.2f, 0.5f);
            //baseSectionVariables.variables[0].transform.DOScaleY(newScale - 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[6].transform.DOScaleY(newScale + 0.2f, 0.5f);
            //baseSectionVariables.variables[0].transform.DOScaleY(newScale, 0.1f);
            baseSectionVariables.variables[1].transform.DOScaleY(newScale - 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[1].transform.DOScaleY(newScale, 0.1f);
            baseSectionVariables.variables[2].transform.DOScaleY(newScale - 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[2].transform.DOScaleY(newScale, 0.1f);
            baseSectionVariables.variables[3].transform.DOScaleY(newScale - 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[3].transform.DOScaleY(newScale, 0.1f);
            baseSectionVariables.variables[4].transform.DOScaleY(newScale - 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[4].transform.DOScaleY(newScale, 0.1f);
            baseSectionVariables.variables[5].transform.DOScaleY(newScale - 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[5].transform.DOScaleY(newScale, 0.1f);
            baseSectionVariables.variables[6].transform.DOScaleY(newScale - 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[6].transform.DOScaleY(newScale, 0.1f);
        } 
        else
        {
            foreach (GameObject i in baseSectionVariables.variables)
            {
                i.transform.DOScaleY(newScale, 0.5f);
            }

            yield return new WaitForSeconds(0.5f);

            foreach (GameObject i in baseSectionVariables.variables)
            {
                i.SetActive(false);
            }
        }
    }

    IEnumerator RoadAnimation(float number)
    {
        if (number == 1)
        {
            for (int i = 0; i < roads.Count; i++)
            {
                yield return new WaitForSeconds(0.03f);
                roads[i].SetActive(true);
            }
        }
        else
        {
            foreach (GameObject roadSection in roads)
            {
                roadSection.SetActive(false);

                yield return new WaitForSeconds(0.02f);
            }
        }

    }
}
