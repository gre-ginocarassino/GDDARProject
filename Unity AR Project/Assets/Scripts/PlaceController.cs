using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaceController : MonoBehaviour
{
    public BoardController boardController;

    [Header("Base Variables")]
    public GameObject baseSectionsPrefabs;
    public GameObject baseSections;
    public PlacementVariables baseSectionVariables;

    public virtual void Awake()
    {
        //setting the variables we need as this object is going to be spawned at runtime :)
        boardController = (BoardController)FindObjectOfType(typeof(BoardController));
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

            //TODO Instatiate -> Microsoft Azure

            baseSections = Instantiate(baseSectionsPrefabs, transform);
        }

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

            baseSectionVariables.variables[0].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[1].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[2].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[3].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[4].transform.DOScaleY(newScale + 0.2f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[5].transform.DOScaleY(newScale + 0.2f, 0.5f);
            baseSectionVariables.variables[0].transform.DOScaleY(newScale - 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);

            baseSectionVariables.variables[6].transform.DOScaleY(newScale + 0.2f, 0.5f);
            baseSectionVariables.variables[0].transform.DOScaleY(newScale, 0.1f);
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
}
