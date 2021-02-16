using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContentCountries : MonoBehaviour
{
    public TextMeshProUGUI[] txtColor;
    public TextMeshProUGUI[] txtSize;




    void Start()
    {
        txtColor = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var item in txtColor)
        {
            
            item.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
            item.gameObject.GetComponent<TextMeshProUGUI>().fontSize = 28;
            item.gameObject.GetComponent<RectTransform>().transform.Translate(new Vector2(0, -6));
        }

        
    }

}
