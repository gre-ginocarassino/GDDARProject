using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChosenFlag : MonoBehaviour
{
    public Image chosenCountryimg;
    public TextMeshProUGUI choosenLanguage;
    public Sprite flagImage;
    
    public FlagSprite countryAssigner;

    void OnEnable()
    {
        
        choosenLanguage.SetText(ChosenLanguageAndFlag.Instance.chosenLanguage);
        DisplayFlag();
    }
    
    private void DisplayFlag()
    {

        countryAssigner = GameObject.FindGameObjectWithTag("countryAssigner").GetComponent<FlagSprite>();
        flagImage = countryAssigner.GetComponent<Image>().sprite;
        chosenCountryimg.sprite = flagImage;
    }



}