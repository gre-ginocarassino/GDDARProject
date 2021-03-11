using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveInParabola : MonoBehaviour
{

  
 
    [SerializeField] private Transform _finalPoint;  // Screen Position
    private float _timeToReachDestination = 2f;
    private float _currentTime = 0;
    private float _targetDelay;

   

    public void MoveObjectRoutineWrapper()
    {
        StartCoroutine(MoveObjectWithParabolaAndRotation());
    }

  
  public  IEnumerator MoveObjectWithParabolaAndRotation()
         {
           
             while (_currentTime < _timeToReachDestination)
             {
                 transform.LookAt(_finalPoint);
                 _currentTime += Time.deltaTime;
                 transform.Translate(Vector3.up  * Time.deltaTime * 0.01f);
                 
                 transform.position = Vector3.Slerp(transform.position, _finalPoint.transform.position, _currentTime * 1f);
     
                 yield return null;

                 while (_currentTime < _timeToReachDestination)
                 {

                     transform.Translate(Vector3.up * Time.deltaTime * 0.01f);
                     transform.position = Vector3.Slerp(transform.position, _finalPoint.transform.position, _currentTime * 1.5f);
                     yield return null;
                 }
             }
         }
}



