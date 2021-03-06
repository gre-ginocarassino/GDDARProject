﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation.Samples;

[RequireComponent(typeof(ARRaycastManager))]
public class ARInput : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    //private GameObject spawnedObject;

    public GameObject placeableObject;

    static List<ARRaycastHit> _sHits = new List<ARRaycastHit>();

    public Material scanning;
    public Material playing;
    public CameraTesting camTest;

    [Header("Light Variables")]
    public BasicLightEstimation bLE;
    public Color lColor;
    Light dLight;

    [Header("Debug Mode")]
    public bool spawnMoveState = true;
    public Text modeDebugText;


    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        dLight = bLE.gameObject.GetComponent<Light>();
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

    /// <summary>
    /// Disabling and Enabling / Starting and Ending the AR Session
    /// </summary>
    public void EnablePlaneScanning()
    {
        planeManager.enabled = true;

        GameObject[] planes = GameObject.FindGameObjectsWithTag("Plane");

        if (planes.Length != 0)
        {
            foreach (GameObject a in planes)
            {
                a.SetActive(true);
            }
        }

        Debug.Log("ARInput : ENABLING Plane Scanning");

        //Disable if Using Unity
        if (camTest.usingUnity)
        {
            DisablePlaneScanning();
            placeableObject.SetActive(true);
            spawnMoveState = false;
        }

        //Changes the Light to face always at the Placeable Object
        Vector3 lightTrans = new Vector3(placeableObject.transform.eulerAngles.x + 50, placeableObject.transform.eulerAngles.y, placeableObject.transform.eulerAngles.z);
        bLE.transform.eulerAngles = lightTrans;
        Debug.Log("Light : " + lightTrans);
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

        UIManager.Instance.SlideInOut(UIManager.Instance.scanningPanel);
        UIManager.Instance.SlideInOut(UIManager.Instance.playPanel);

        Debug.Log("ARInput : DISABLING Plane Scanning");
    }

    public void EndARSession()
    {
        planeManager.enabled = false;

        placeableObject.SetActive(false);

        GameObject[] planes = GameObject.FindGameObjectsWithTag("Plane");

        if (planes.Length != 0)
        {
            foreach (GameObject a in planes)
            {
                Destroy(a);
            }
        }

        Debug.Log("ARInput : TERMINATING Plane Scanning");
        spawnMoveState = true;
    }

    /// <summary>
    ///Completely Resets the AR Session
    /// </summary>
    public void CompleteResetter()
    {
        StartCoroutine(PlaneResetter());
    }

    IEnumerator PlaneResetter()
    {
        EndARSession();
        UIManager.Instance.SlideInOut(UIManager.Instance.scanningPanel);
        UIManager.Instance.SlideInOut(UIManager.Instance.playPanel);

        Debug.Log("ARInput : RESETTING (Disabling)");
        
        yield return new WaitForSeconds(1.5f);

        EnablePlaneScanning();
    }

    /// <summary>
    /// Controls the State:
    /// SpawnOrMove: Spawns or Moves the AR Objects
    /// Interact: Interacts with the AR Objects
    /// </summary>
    void SpawnOrMove()
    {

        if (!TryGetTouchPosition(out Vector2 touchPos))
        {
            return;
        }

        if (raycastManager.Raycast(touchPos, _sHits, TrackableType.PlaneWithinPolygon))
        {
            var hitPos = _sHits[0].pose;
            //if (spawnedObject == null)
            //{
            //    spawnedObject = Instantiate(placeableObject, hitPos.position, hitPos.rotation);
            //    Debug.Log("Inside: Instantiate");

            //    spawnMoveState = false;
            //    DisablePlaneScanning();

           //}
            
            placeableObject.transform.position = hitPos.position;
            placeableObject.transform.rotation = hitPos.rotation;
            placeableObject.SetActive(true);
            Debug.Log("Inside: Move");

            spawnMoveState = false;
            DisablePlaneScanning();

            

            if (!placeableObject.activeSelf)
            {
                placeableObject.SetActive(true);
            }
        }
    }

    void Interact()
    {
        if (camTest.usingUnity)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Touch Phase : Computer Unity");
                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
        else
        {
            for (var i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    Debug.Log("Touch Phase : Phone Touch Pos");
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

    public void LightEstimationToggler()
    {
        if (bLE.enabled == true)
        {
            Debug.Log("Disabling BLE");
            bLE.enabled = false;
            Debug.Log("Disabled BLE");

            dLight.intensity = 1;
            dLight.color = lColor;
            
        }
        else
        {
            Debug.Log("Enabling BLE");
            bLE.enabled = true;
            Debug.Log("Enabled BLE");
        }
    }
}
