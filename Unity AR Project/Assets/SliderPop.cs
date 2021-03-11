using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPop : MonoBehaviour
{
   

    float timeOfTravel = 1f; //time after object reach a target place 
    float currentTime = 0; // actual floting time 
    private float normalizedValue;
    
    float timeOfTravel1 = 1f; //time after object reach a target place 
    float currentTime1 = 0; // actual floting time 
    private float normalizedValue1;
    
    private Vector2 startPos;
    private Vector2 finalPos;
    private bool isRunning = false;
    private int _counter;

  [SerializeField]  private RectTransform[] _transforms;
    
    private 
    void Start()
    {
        startPos = new Vector2(13, -1906);
        finalPos = new Vector2(13, -1400);
        _counter = 0;
        StartCoroutine(LerpObjectUp(_transforms[0]));
    }

    private void Update()
    {
         _counter = Mathf.Clamp(_counter, 0, 5);
    }

    public void ToRight()
    {
        _counter++;

        if (_counter > 0)
        {
            StartCoroutine(LerpObjectDown(_transforms[_counter - 1]));
        }
        StartCoroutine(LerpObjectUp(_transforms[_counter]));
        Debug.Log("Counter" + _counter);
    }
    
    public void ToLeft()
    {
        _counter--;

        if (_counter < 5)
        {
            StartCoroutine(LerpObjectDown(_transforms[_counter + 1]));
        }
        StartCoroutine(LerpObjectUp(_transforms[_counter]));
        
        Debug.Log("Counter2" + _counter);
    }

    IEnumerator LerpObjectUp(RectTransform _rectTransform)
    {

        while (currentTime1 <= timeOfTravel1) 
        { 
            currentTime1 += Time.deltaTime; 
            normalizedValue1 = currentTime1 / timeOfTravel; 
 
          _rectTransform.anchoredPosition = Vector2.Lerp(startPos,finalPos, normalizedValue1); 
          yield return null;
        }
        
        if (currentTime1 > 1)
        {
            currentTime1 = 0;
        }
        
    }
    
    IEnumerator LerpObjectDown(RectTransform _rectTransform)
    {

        while (currentTime <= timeOfTravel) 
        { 
            currentTime += Time.deltaTime; 
            normalizedValue = currentTime / timeOfTravel; 
 
            _rectTransform.anchoredPosition = Vector2.Lerp(finalPos,startPos, normalizedValue);
            yield return null;
        }
        
        if (currentTime > 1)
        {
            currentTime = 0;
        }
    }
}



     
