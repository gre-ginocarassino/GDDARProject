using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    [SerializeField]
    private Quaternion objectToRotate;
    
    [SerializeField] 
    private float timeToRotate = 0.5f;
    
    [SerializeField] 
    private float smoothRotation = 1f;

    private bool StartCounting = false;
    private void Start()
    {
        objectToRotate = transform.rotation;
    }

    private void Update()
    {

        if (StartCounting == true)
        {
            timeToRotate-= Time.deltaTime;
            
            if (timeToRotate <= 0)
            {
                timeToRotate = 0.5f;
                StartCounting = false;
            }
        }
        
    }

    public void RightRotation()
    {
        
        StartCoroutine( RotateToRight());
    }
    public void LefttRotation()
    {
        
        StartCoroutine( RotateToLeft());
    }


    private IEnumerator RotateToRight()
    {
        StartCounting = true;
        
        while (StartCounting)
        {
            
            objectToRotate *=  Quaternion.AngleAxis(1, Vector3.up);
            transform.rotation = Quaternion.Lerp (transform.rotation, objectToRotate , 10 * smoothRotation * Time.deltaTime);
            
            yield return null;
        }
        yield break;
    }
    
    private IEnumerator RotateToLeft()
    {
        StartCounting = true;
        
        while (StartCounting)
        {
            
            objectToRotate *=  Quaternion.AngleAxis(1, Vector3.down);
            transform.rotation = Quaternion.Lerp (transform.rotation, objectToRotate , 10 * smoothRotation * Time.deltaTime);
            
            yield return null;
        }
        yield break;
    }
}

