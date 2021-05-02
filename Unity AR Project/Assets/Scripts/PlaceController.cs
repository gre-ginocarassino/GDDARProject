using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaceController : MonoBehaviour
{
    //Crowd related
    private Pooler pooler;
    //private bool Spawned;

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

    [Header("Score")]
    public string countryScore;
    public int floatScore;

    private bool isTesting;

    public virtual void Awake()
    {
        //setting the variables we need as this object is going to be spawned at runtime :)
        boardController = (BoardController)FindObjectOfType(typeof(BoardController));
        isTesting = boardController.isTesting;
        az = (AssetBundleAzure)FindObjectOfType(typeof(AssetBundleAzure));
        flagCont = (FlagController)FindObjectOfType(typeof(FlagController));

        pooler = new Pooler();
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

                //if (baseSectionVariables == null)
                //{
                //    baseSectionVariables = baseSections.GetComponent<PlacementVariables>();
                //}
                //baseSections.SetActive(true);
                //StartCoroutine(VariablesResizing(1));
            }
            else
            {
                StartCoroutine(downloader());
            }
        }
        else
        {
            StartCoroutine(VariablesResizing(1));
            Debug.Log("Place Controller : Already One In The Scene : VR1");
        }

        StartCoroutine(flagCont.SelectNewFlag(countryFlag));

        baseSectionVariables.musicSource.clip = baseSectionVariables.musicClip;
        baseSectionVariables.musicSource.Play();
        boardController.musicEffect.Play();

    }

    IEnumerator downloader()
    {
        az.assetBundleName = AssetbundleName;
        az.assetName = AssetName;

        az.TappedLoadAssetBundle(this);

        yield return new WaitForSeconds(0f);
    }

    public virtual void ShrinkPlace()
    {
        if (baseSectionVariables == null)
        {
            baseSections.GetComponent<PlacementVariables>();
        }

        baseSectionVariables.musicSource.Stop();
        boardController.musicEffect.Stop();

        //baseSections.transform.DOScaleY(0, 1);
        StartCoroutine(VariablesResizing(0));
    }

    public IEnumerator VariablesResizing(int newScale)
    {
        baseSectionVariables.gameObject.SetActive(true);



        //If the new scale is = 1, it's getting bigger, if it's not
        //it's getting smaller.
        if (newScale == 1)
        {
            foreach (GameObject roadSection in roads)
            {
                roadSection.SetActive(false);
            }

            foreach (GameObject i in baseSectionVariables.variables)
            {
                i.SetActive(false);

                //i.transform.DOScaleY(newScale + 0.2f, 0.5f);
                //yield return new WaitForSeconds(0.5f);
                //i.transform.DOScaleY(newScale - 0.1f, 0.1f);
                //yield return new WaitForSeconds(0.1f);
                //i.transform.DOScaleY(newScale, 0.1f);
            }

            yield return new WaitForSeconds(0.5f);

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

            #region GROW SECTION

            baseSectionVariables.variables[0].SetActive(true);
            baseSectionVariables.variables[0].transform.DOScaleY(newScale, 0);
            StartCoroutine(RoadAnimation(newScale));

            //baseSectionVariables.variables[0].transform.DOScaleY(newScale + 0.2f, 0.5f);
            //yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[1].SetActive(true);
            baseSectionVariables.variables[1].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[2].SetActive(true);
            baseSectionVariables.variables[2].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[3].SetActive(true);
            baseSectionVariables.variables[3].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[4].SetActive(true);
            baseSectionVariables.variables[4].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[5].SetActive(true);
            baseSectionVariables.variables[5].transform.DOScaleY(newScale + 0.2f, 0.5f);
            //baseSectionVariables.variables[0].transform.DOScaleY(newScale - 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[6].SetActive(true);
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

            yield return new WaitForSeconds(0.1f);

            #endregion

            yield return new WaitForSeconds(1f);
            //pass bool true to pooler
            if (boardController.Spawned == true)
            {
                pooler.startSpawn(true);
            }

        }
        else
        {
            pooler.startSpawn(false);
            CharacterParent.characterParent.DisableCharacter();

            foreach (GameObject i in baseSectionVariables.variables)
            {
                i.transform.DOScaleY(newScale, 0.5f);
            }

            yield return new WaitForSeconds(0.5f);

            foreach (GameObject i in baseSectionVariables.variables)
            {
                i.SetActive(false);
            }

            foreach (GameObject roadSection in roads)
            {
                roadSection.SetActive(false);

                yield return new WaitForSeconds(0.02f);
            }

            baseSectionVariables.gameObject.SetActive(false);
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
            //foreach (GameObject roadSection in roads)
            //{
            //    roadSection.SetActive(false);

            //    yield return new WaitForSeconds(0.02f);
            //}
        }

    }

    public virtual void GetData()
    {

    }
}
