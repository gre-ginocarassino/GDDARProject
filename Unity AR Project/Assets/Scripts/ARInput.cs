using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
public class ARInput : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    public GameObject placeableObject;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();


    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPos)
    {
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            Debug.Log("Inside: True");
            return true;
        }

        touchPos = default;
        Debug.Log("Inside: False");
        return false;
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPos))
        {
            return;
        }

        if (raycastManager.Raycast(touchPos, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPos = s_Hits[0].pose;
            //if (spawnedObject == null)
            //{
            //    spawnedObject = Instantiate(placeableObject, hitPos.position, hitPos.rotation);
            //    Debug.Log("Inside: Instantiate");
            //}
            //else
            //{
            placeableObject.transform.position = hitPos.position;
            placeableObject.transform.rotation = hitPos.rotation;
            Debug.Log("Inside: Move");

            if (!placeableObject.activeSelf)
            {
                placeableObject.SetActive(true);
            }
        }
    }
}
