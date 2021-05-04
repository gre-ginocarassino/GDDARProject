using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveInParabola : MonoBehaviour
{

  
 
    [SerializeField] private Transform _finalPoint;
    public Transform _finalPoint2;// Screen Position

    //I've added a final point 2 that can find the move to position at runtime. This way it can be instantiated in when needed
    //hope that's alright :)

    private float _timeToReachDestination = 2f;
    private float _currentTime = 0;
    private float _targetDelay;

    private void Start()
    {
        _finalPoint2 = Camera.main.transform.Find("MoveTo");
    }

    public void MoveObjectRoutineWrapper()
    {
        StartCoroutine(MoveObjectWithParabolaAndRotation());
    }

  
  public  IEnumerator MoveObjectWithParabolaAndRotation()
         {
           
             while (_currentTime < _timeToReachDestination)
             {
                 transform.LookAt(_finalPoint2);
                 _currentTime += Time.deltaTime;
                 transform.Translate(Vector3.up  * Time.deltaTime * 0.01f);
                 
                 transform.position = Vector3.Slerp(transform.position, _finalPoint2.transform.position, _currentTime * 1f);
     
                 yield return null;

                 while (_currentTime < _timeToReachDestination)
                 {

                     transform.Translate(Vector3.up * Time.deltaTime * 0.01f);
                     transform.position = Vector3.Slerp(transform.position, _finalPoint2.transform.position, _currentTime * 1.5f);
                     yield return null;
                 }
             }
         }
}



