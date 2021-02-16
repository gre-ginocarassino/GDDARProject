using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ContinentObject : MonoBehaviour, IPointerClickHandler
{
    private Vector3 _originalScale;
    [SerializeField] private float growFactor;
    [SerializeField] private float shrinkFactor;
    [SerializeField] private float waitTime;
    [SerializeField] private float maxSize;
    public ContinentPicker continentPicker;
    private CanvasGroup _canvasGroup;
    public GameObject slider;
    public GameObject backButton;
    [SerializeField] private Button backBtn;
   // [SerializeField] private LanguageBackBtn languageButton;
    public TextMeshProUGUI uiText;
    



    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        //LanguageBackBtn languageButton = gameObject.GetComponent<LanguageBackBtn>();
        ContinentPicker continentPicker = gameObject.GetComponent<ContinentPicker>();
        TextMeshProUGUI uiText = gameObject.GetComponent<TextMeshProUGUI>();
        GetComponent<Button>().onClick.AddListener(GetContinent);

    }

    void Update()
    {
        if (this.gameObject.transform.localScale.x <= 1)
        {
            if (uiText != null)
            {
                uiText.SetText("Do you have a continent in mind?");
            }
        }
    }

    public IEnumerator Scale()
    {

        

        float timer = 0;

        while (true)
        {
            
            backBtn.interactable = true;
            
            while (maxSize > transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale += new Vector3(1, 1, 1) *  growFactor * Time.deltaTime;
               // languageButton.GetComponent<CanvasGroup>().blocksRaycasts = false;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
                yield return null;
            }

           // languageButton.GetComponent<CanvasGroup>().blocksRaycasts = true;
            slider.SetActive(true);
            backButton.SetActive(true);

            timer = 0;
            yield return new WaitForSeconds(waitTime);
            break;
        }
    }


    public IEnumerator UnScale()
    {
        float timer = 0;

        while (true)
        {
            backBtn.interactable = false;
            while (1 < transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * shrinkFactor;
                yield return null;


                if (transform.localScale.x <= 1)
                {
                    GetComponent<CanvasGroup>().blocksRaycasts = true;
                    backButton.SetActive(false);
                    slider.SetActive(false);
                    transform.localScale = new Vector3(1,1,1);
                }
            }

            timer = 0;
            yield return new WaitForSeconds(waitTime);
            break;

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        continentPicker.continents.Remove(gameObject); // removing from list with continents
        continentPicker.removedContinentAfterCLick.Add(gameObject); // Adding to another list 
        StartCoroutine(Scale());

        uiText.SetText("Which country do you want to explore?");


        StartCoroutine(AddBackToList(0.01f));
        StartCoroutine(RemoveBackToList(0.02f));

        foreach (var item in continentPicker.continents)
        {
            item.SetActive(false);
        }

        IEnumerator AddBackToList(float time)
        {
            yield return new WaitForSeconds(0.01f);
            foreach (var item in continentPicker.removedContinentAfterCLick)
            {
                continentPicker.continents.Add(gameObject);
                break;
            }

        }

        IEnumerator RemoveBackToList(float time)
        {
            yield return new WaitForSeconds(0.02f);
            foreach (var item in continentPicker.removedContinentAfterCLick)
            {
                continentPicker.removedContinentAfterCLick.Remove(gameObject);
                break;
            }

        }


    }


    public void GetContinent()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Europe")
        {
            ChosenLanguageAndFlag.Instance.chosenContinent = "Europe";

        }

        if (EventSystem.current.currentSelectedGameObject.name == "North_America")
        {
            ChosenLanguageAndFlag.Instance.chosenContinent = "North America";

        }

        if (EventSystem.current.currentSelectedGameObject.name == "South_America")
        {
            ChosenLanguageAndFlag.Instance.chosenContinent = "South America";

        }

        if (EventSystem.current.currentSelectedGameObject.name == "Australia")
        {
            ChosenLanguageAndFlag.Instance.chosenContinent = "Australia";

        }

        if (EventSystem.current.currentSelectedGameObject.name == "Asia")
        {
            ChosenLanguageAndFlag.Instance.chosenContinent = "Asia";

        }

        if (EventSystem.current.currentSelectedGameObject.name == "Africa")
        {
            ChosenLanguageAndFlag.Instance.chosenContinent = "Africa";

        }
    }

}
