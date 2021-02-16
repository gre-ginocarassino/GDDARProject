using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnglishButton : MonoBehaviour
{
    public TextMeshProUGUI continueButton;
    public ChosenLanguageAndFlag chosenLanguage;
    public TextMeshProUGUI uiText;



    private void Awake()
    {
        TextMeshProUGUI continueButton = GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI uiText = gameObject.GetComponent<TextMeshProUGUI>();

    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(DOChooseFlagbtn);
    }
    public void DONameContinueButton()
    {

        continueButton.SetText("Proceed with English");

    }

    public void DOSaveLanguage()
    {
        chosenLanguage = GameObject.FindGameObjectWithTag("singleton").GetComponent<ChosenLanguageAndFlag>();
        chosenLanguage.chosenLanguage = "English";
        

    }

    private void DOChooseFlagbtn()
    {
        uiText.SetText("");


    }
}
