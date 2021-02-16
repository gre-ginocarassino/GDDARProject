using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

//Summary: This altered version focuses on scaling the AR camera to make content remain in
//its proper scale without having to scale it down to fit phone's device coordinate system
//therefore physics will not be affected due to scale issue
[RequireComponent(typeof(ARSessionOrigin))]

[RequireComponent(typeof(ARRaycastManager))]
public class ARInput_Altered : MonoBehaviour
{
    private ARSessionOrigin _sessionOrigin;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private GameObject spawnedObject;

    public GameObject placeableObject;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    [Header("Debug Mode")]
    public bool spawnMoveState = true;
    public Text modeDebugText;


    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        _sessionOrigin = GetComponent<ARSessionOrigin>();
    }

    bool TryGetTouchPosition(out Vector2 touchPos)
    {
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            Debug.Log("Inside: True Touch");
            return true;
        }

        touchPos = default;
        //Debug.Log("Inside: False");
        return false;
    }

    private void Update()
    {
        //Check the current state of interaction
        if (spawnMoveState)
        {
            SpawnOrMove();
            modeDebugText.text = "Input Mode : Spawning";
        }
        else
        {
            Interact();
            modeDebugText.text = "Input Mode : Interacting";
        }

    }

    public void EnablePlaneScanning()
    {
        planeManager.enabled = true;

        GameObject[] planes = GameObject.FindGameObjectsWithTag("Plane");

        if (planes.Length != 0)
        {
            foreach (GameObject a in planes)
            {
                a.SetActive(false);
            }
        }

        Debug.Log("ARInput : ENABLING Plane Scanning");
    }

    public void DisablePlaneScanning()
    {
        planeManager.enabled = false;

        GameObject[] planes = GameObject.FindGameObjectsWithTag("Plane");

        if (planes.Length != 0)
        {
            foreach (GameObject a in planes)
            {
                a.SetActive(false);
            }
        }

        Debug.Log("ARInput : DISABLING Plane Scanning");
    }

    void SpawnOrMove()
    {
        if (!TryGetTouchPosition(out Vector2 touchPos))
        {
            return;
        }

        if (raycastManager.Raycast(touchPos, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPos = s_Hits[0].pose;
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(placeableObject, hitPos.position, hitPos.rotation);
                

                Debug.Log("Inside: Instantiate");

                spawnMoveState = false;
                DisablePlaneScanning();
                UIManager.Instance.SlideInOut(UIManager.Instance.scanningPanel);
            }
            else
            {
                //spawnedObject.transform.position = hitPos.position;
                //spawnedObject.transform.rotation = hitPos.rotation;
                //if you want to rotate or scale camera, the script below can be used
               // _sessionOrigin.MakeContentAppearAt(spawnedObject.transform, spawnedObject.transform.position, spawnedObject.transform.rotation);
               
                Debug.Log("Inside: Move");

                spawnMoveState = false;
                DisablePlaneScanning();
                UIManager.Instance.SlideInOut(UIManager.Instance.scanningPanel);
            }

            if (!placeableObject.activeSelf)
            {
                placeableObject.SetActive(true);
            }
        }
    }

    void Interact()
    {
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Debug.Log("In");
                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                // Create a particle if hit
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "InteractableObject")
                    {
                        Debug.Log("Interactable Object Hit : " + hit.collider.gameObject.name);
                        hit.collider.gameObject.GetComponent<InteracbleObject>().Interact();
                    }
                }
            }
        }
    }
}
