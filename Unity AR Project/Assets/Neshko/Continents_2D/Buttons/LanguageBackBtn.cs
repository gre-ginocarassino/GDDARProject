using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;


public class LanguageBackBtn : MonoBehaviour
{
    public ContinentObject[] continentObjects;


    
    [SerializeField] private GameObject europeSlider;
    [SerializeField] private GameObject northAmericaSlider;
    [SerializeField] private GameObject southAmericaSlider;
    [SerializeField] private GameObject australiaSlider;
    [SerializeField] private GameObject africaSlider;
    [SerializeField] private GameObject asiaSlider;
  
    public bool backToContinents;
    [SerializeField] private float waitTime;
    public TextMeshProUGUI uiText;
    [SerializeField] private FlagSprite flagSprite;



    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(BackToCountries);
        ContinentObject continentObjects = gameObject.GetComponent<ContinentObject>();
        TextMeshProUGUI uiText = gameObject.GetComponent<TextMeshProUGUI>();
        GameObject languageSlider = gameObject.GetComponent<GameObject>();
        GameObject continueButton = gameObject.GetComponent<GameObject>();
        flagSprite = GetComponent<FlagSprite>();
    }
    
    public void BackToCountries()
    {
        
        if (ChosenLanguageAndFlag.Instance.chosenContinent == "Europe" || 
            ChosenLanguageAndFlag.Instance.chosenContinent == "North America" ||
            ChosenLanguageAndFlag.Instance.chosenContinent == "South America" || 
            ChosenLanguageAndFlag.Instance.chosenContinent == "Africa" ||
            ChosenLanguageAndFlag.Instance.chosenContinent == "Australia" ||
            ChosenLanguageAndFlag.Instance.chosenContinent == "Asia")
        {
            
            foreach (var item in continentObjects)
            {
                item.gameObject.SetActive(true);

                if (item.gameObject.name == "Europe" || item.gameObject.name == "North_America" || 
                    item.gameObject.name == "South_America" || item.gameObject.name == "Australia"|| 
                    item.gameObject.name == "Africa" || item.gameObject.name == "Asia")
                {
                   
                        StartCoroutine(item.UnScale(item.transform));
                        item.gameObject.SetActive(true);

                        if (GameObject.FindGameObjectWithTag("Flag") != null)
                        {
                            GameObject.FindGameObjectWithTag("Flag").SetActive(false);
                        }
                }
            }
        }
    }


}



