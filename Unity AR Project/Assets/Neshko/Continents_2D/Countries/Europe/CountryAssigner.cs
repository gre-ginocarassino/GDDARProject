using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using UnityEngine.Serialization;

public class CountryAssigner : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    [SerializeField] private Image displayFlagImg;
    public Sprite thisImage;
    [SerializeField] private FlagSprite flagSprite;



    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(DisplayFlag);
        GetComponent<Button>().onClick.AddListener(ChooseFlagbtn);
        TextMeshProUGUI uiText = gameObject.GetComponent<TextMeshProUGUI>();
        FlagSprite flagSprite = gameObject.GetComponent<FlagSprite>();

    }

    public void GetCountry()
    {
        ChosenLanguageAndFlag.Instance.chosenCountry = EventSystem.current.currentSelectedGameObject.name;
    }

    private void ChooseFlagbtn()
    {
        if (uiText != null)
        {
            uiText.SetText("");
            
        }
    }

    private void DisplayFlag()
    {
        
        thisImage = this.gameObject.GetComponent<Image>().sprite;
        displayFlagImg.sprite = thisImage;
        flagSprite.gameObject.GetComponent<Image>().enabled = true;
    }

}
