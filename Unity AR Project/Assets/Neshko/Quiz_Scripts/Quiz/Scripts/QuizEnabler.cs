using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class QuizEnabler : MonoBehaviour
{
    [SerializeField]private GameObject[] Prefabs;
    private Countries _countriesEnum;
    [SerializeField]private CanvasGroup[] _canvasGroups;
    [SerializeField]private RectTransform[] _objectToShrink;
     private float maxSize = 1f;

     [SerializeField]private GameObject _removeContinents;
     private string _country;
     
     private void Start()
     {
       
      
        _removeContinents.gameObject.SetActive(false);
     }

     public void EnableQuizUI()
    {
        CountrySelector();
        

        switch (_countriesEnum)
             {
                 case Countries.France :
                     Deselect();
                     Deactivate();
                     StartCoroutine(FadeUI.Fade(1, _canvasGroups[0], 1f));
                     Prefabs[0].gameObject.SetActive(true);
                     ShrinkToInitialSize();
                     _removeContinents.gameObject.SetActive(true);
                     
                     // AR Spawn Method for that country can be called here
                     // Activate the button that can take the player to the quiz section
                   
                     break;
                 
                 case Countries.China : 

                     Deselect();
                     Deactivate();
                     StartCoroutine(FadeUI.Fade(1, _canvasGroups[1], 1f));
                     Prefabs[1].gameObject.SetActive(true);
                     
                     
                     for (int i = 0; i < _objectToShrink.Length; i++)
                     {
                         StartCoroutine(ShrinkEffect(_objectToShrink[i], maxSize));
                     }
                     break;
             }
    }

    void CountrySelector()
    {
        ConvertStringToEnum();
    }

    public void ConvertStringToEnum()
    {
        _country =  EventSystem.current.currentSelectedGameObject.name; 
        _countriesEnum = (Countries) Enum.Parse(typeof (Countries), _country);
    }
    
    void Deselect()
    {
        for (int i = 0; i < _canvasGroups.Length; i++)
        {
            StartCoroutine(FadeUI.Fade(0, _canvasGroups[i], 1f));
        }
    }
    
    
    void Deactivate()
    {
        for (int i = 0; i < Prefabs.Length; i++)
        {
            Prefabs[i].gameObject.SetActive(false);
        }
    }

    IEnumerator ShrinkEffect(RectTransform objectToShrink, float _maxSize)
    {
        float timeToGrow = 0.7f;
        _maxSize = maxSize;
        while (objectToShrink.localScale.x < _maxSize)
        {
            timeToGrow += Time.deltaTime;
            for (int i = 0; i < _objectToShrink.Length; i++)
            {
                objectToShrink.localScale += new Vector3(1f, 1f) * Time.deltaTime * timeToGrow;
            }

            yield return null;
        }

        Debug.Log(timeToGrow);
    }

    void ShrinkToInitialSize()
    {
        for (int i = 0; i < _objectToShrink.Length; i++)
        {
            _objectToShrink[i].localScale = new Vector3(0.1f, 0.1f);
        }
    }

  

    public enum Countries 
    { 
     France, 
     UnitedKingdom, 
     Italy, 
     China
    };
}
