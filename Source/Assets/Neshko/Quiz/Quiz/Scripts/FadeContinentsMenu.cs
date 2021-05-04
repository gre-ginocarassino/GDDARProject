using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeContinentsMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _gameObjectToDisable;

    public void FadeContinents()
    {
        // Calling Static Fade Method
        StartCoroutine(FadeUI.Fade(0, _canvasGroup, 1f)); 

        
        if (_canvasGroup.alpha < 0.1)
        {
            _gameObjectToDisable.gameObject.SetActive(false);    
        }
    }
    
}
