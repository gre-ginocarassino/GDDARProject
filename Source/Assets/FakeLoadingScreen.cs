using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class FakeLoadingScreen : MonoBehaviour
{
    [SerializeField] private Sprite[] _backgroundImages;
    [SerializeField] private Image _backgroundImage;
    
    [SerializeField] private String[] _hints;
    [SerializeField] private TextMeshProUGUI _hint;

    private CanvasGroup _canvasGroup;

    void Start()
    {
        ChooseBackground();
        ChooseHint();
    }
  
    
    void ChooseBackground()
    {
        var randomNumber = Random.Range(0, _backgroundImages.Length);
        _backgroundImage.sprite = _backgroundImages[randomNumber];
        
    }

    void ChooseHint()
    {
        var randomNumber = Random.Range(0, _hints.Length);
        _hint.text = _hints[randomNumber];
    }

    
    // Create event OnLoading
    // Or Call these to Show/Hide Loading Screen
    public void ShowLoadingScreen()
    {
        StartCoroutine(FadeUI.Fade(1, _canvasGroup, 1f));
    }
    
    public void HideLoadingScreen()
    {
        StartCoroutine(FadeUI.Fade(1, _canvasGroup, 1f));
    }

}
