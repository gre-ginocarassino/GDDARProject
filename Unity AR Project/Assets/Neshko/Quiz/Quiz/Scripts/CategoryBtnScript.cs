using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CategoryBtnScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI categoryTitleText;
    [SerializeField] private TextMeshProUGUI timeToComplete;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button btn;

    public Button Btn { get => btn; }

    public void SetButton(string title, int totalQuestion, string timeToComplete)
    {
       
        categoryTitleText.text = title;
        this.timeToComplete.text = timeToComplete;
        scoreText.text = PlayerPrefs.GetInt(title, 0) + "/" + totalQuestion; 
    }

}
