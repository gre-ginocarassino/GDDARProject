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

    //Board controller hjolding all the gameplay variables
    private BoardController bCont;
     
     private void Start()
     {
        //finding the board controller in the scene
        bCont = (BoardController)FindObjectOfType(typeof(BoardController));
      
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
                    // ShrinkToInitialSize();
                     bCont = (BoardController)FindObjectOfType(typeof(BoardController));
                     bCont.NewSection(bCont.franceController);

                break;

                case Countries.England:
                    Deselect();
                    Deactivate();
                    StartCoroutine(FadeUI.Fade(1, _canvasGroups[0], 1f));
                    Prefabs[0].gameObject.SetActive(true);
                   // ShrinkToInitialSize();
                    bCont = (BoardController)FindObjectOfType(typeof(BoardController));
                    bCont.NewSection(bCont.englandController);

                break;

            case Countries.Italy:
                    Deselect();
                    Deactivate();
                    StartCoroutine(FadeUI.Fade(1, _canvasGroups[0], 1f));
                    Prefabs[0].gameObject.SetActive(true);
                   // ShrinkToInitialSize();
                    bCont = (BoardController)FindObjectOfType(typeof(BoardController));
                    bCont.NewSection(bCont.italyController);

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
        try
        {
            for (int i = 0; i < _canvasGroups.Length; i++)
            {
                StartCoroutine(FadeUI.Fade(0, _canvasGroups[i], 1f));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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

    // void ShrinkToInitialSize()
    // {
    //     for (int i = 0; i < _objectToShrink.Length; i++)
    //     {
    //         _objectToShrink[i].localScale = new Vector3(0.1f, 0.1f);
    //     }
    // }

  

    public enum Countries 
    { 
     France, 
     England, 
     Italy, 
     China
    };
}
