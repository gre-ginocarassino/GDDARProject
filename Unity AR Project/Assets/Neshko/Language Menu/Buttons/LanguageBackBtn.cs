using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;


public class LanguageBackBtn : MonoBehaviour
{
    public ContinentObject[] continentObjects;


    
    [FormerlySerializedAs("EuropeSlider")] public GameObject europeSlider;
    [FormerlySerializedAs("NorthAmericaSlider")] public GameObject northAmericaSlider;
    [FormerlySerializedAs("SouthAmericaSlider")] public GameObject southAmericaSlider;
    [FormerlySerializedAs("AustraliaSlider")] public GameObject australiaSlider;
    [FormerlySerializedAs("AfricaSlider")] public GameObject africaSlider;
    [FormerlySerializedAs("AsiaSlider")] public GameObject asiaSlider;
  
    public bool backToContinents;
    [SerializeField] private float waitTime;
    public GameObject continueButton;
    public TextMeshProUGUI uiText;
    [FormerlySerializedAs("_flagSprite")] public FlagSprite flagSprite;



    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(BackToCountries);
        GetComponent<Button>().onClick.AddListener(BackFromLanguage);
       

        ContinentObject continentObjects = gameObject.GetComponent<ContinentObject>();
        TextMeshProUGUI uiText = gameObject.GetComponent<TextMeshProUGUI>();
        GameObject languageSlider = gameObject.GetComponent<GameObject>();
        GameObject continueButton = gameObject.GetComponent<GameObject>();
        FlagSprite flagSprite = gameObject.GetComponent<FlagSprite>();

    }
    

    public void BackFromLanguage()
    {
       
          
            uiText.SetText("Which country do you want to explore");
            flagSprite.gameObject.GetComponent<Image>().enabled = false;

            foreach (var item in continentObjects)
            {

                if (ChosenLanguageAndFlag.Instance.chosenContinent == "Europe")
                {
                    europeSlider.gameObject.SetActive(true);

                    if (GameObject.FindGameObjectWithTag("Flag") != null)
                    {
                        GameObject.FindGameObjectWithTag("Flag").SetActive(false);
                    }
                }

                if (ChosenLanguageAndFlag.Instance.chosenContinent == "North America")
                {
                    northAmericaSlider.gameObject.SetActive(true);

                    if (GameObject.FindGameObjectWithTag("Flag") != null)
                    {
                        GameObject.FindGameObjectWithTag("Flag").SetActive(false);
                    }
                 
                }

                if (ChosenLanguageAndFlag.Instance.chosenContinent == "South America")
                {
                    southAmericaSlider.gameObject.SetActive(true);

                    if (GameObject.FindGameObjectWithTag("Flag") != null)
                    {
                        GameObject.FindGameObjectWithTag("Flag").SetActive(false);
                    }
                   
                }

                if (ChosenLanguageAndFlag.Instance.chosenContinent == "Africa")
                {
                    africaSlider.gameObject.SetActive(true);

                    if (GameObject.FindGameObjectWithTag("Flag") != null)
                    {
                        GameObject.FindGameObjectWithTag("Flag").SetActive(false);
                    }
                   
                }

                if (ChosenLanguageAndFlag.Instance.chosenContinent == "Australia")
                {
                    australiaSlider.gameObject.SetActive(true);

                    if (GameObject.FindGameObjectWithTag("Flag") != null)
                    {
                        GameObject.FindGameObjectWithTag("Flag").SetActive(false);
                    }
                
                }

                if (ChosenLanguageAndFlag.Instance.chosenContinent == "Asia")
                {
                    asiaSlider.gameObject.SetActive(true);

                    if (GameObject.FindGameObjectWithTag("Flag") != null)
                    {
                        GameObject.FindGameObjectWithTag("Flag").SetActive(false);
                    }
                
                }

            }
        




        }
    public void BackToCountries()
    {

        if (europeSlider.activeInHierarchy || northAmericaSlider.activeInHierarchy || southAmericaSlider.activeInHierarchy || 
            africaSlider.activeInHierarchy || australiaSlider.activeInHierarchy || asiaSlider.activeInHierarchy)
        {
            
            foreach (var item in continentObjects)
            {
                item.gameObject.SetActive(true);

                if (item.gameObject.name == "Europe")
                {
                    if (item.transform.localScale.x > 1)
                    {
                        StartCoroutine(item.UnScale());
                    }
                }
            }

            foreach (var item in continentObjects)
            {
                if (item.gameObject.name == "North_America")
                {
                    item.gameObject.SetActive(true);

                    if (item.transform.localScale.x > 1)
                    {
                        StartCoroutine(item.UnScale());
                    }


                }

            }

            foreach (var item in continentObjects)
            {
                if (item.gameObject.name == "South_America")
                {
                    item.gameObject.SetActive(true);

                    if (item.transform.localScale.x > 1)
                    {
                        StartCoroutine(item.UnScale());
                    }


                }

            }

            foreach (var item in continentObjects)
            {
                if (item.gameObject.name == "Australia")
                {
                    item.gameObject.SetActive(true);

                    if (item.transform.localScale.x > 1)
                    {
                        StartCoroutine(item.UnScale());
                    }


                }

            }

            foreach (var item in continentObjects)
            {
                if (item.gameObject.name == "Africa")
                {
                    item.gameObject.SetActive(true);

                    if (item.transform.localScale.x > 1)
                    {
                        StartCoroutine(item.UnScale());
                    }


                }

            }

            foreach (var item in continentObjects)
            {
                if (item.gameObject.name == "Asia")
                {
                    item.gameObject.SetActive(true);

                    if (item.transform.localScale.x > 1)
                    {
                        StartCoroutine(item.UnScale());
                    }


                }

            }

        }
        else
        {

            backToContinents = false;
        }

      
    }


}



