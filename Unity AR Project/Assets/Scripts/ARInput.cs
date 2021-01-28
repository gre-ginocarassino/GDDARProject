using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


[RequireComponent(typeof(ARRaycastManager))]
public class ARInput : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    public GameObject placeableObject;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    [Header("Debug Mode")]
    public bool spawnMoveState = true;
    public Text modeDebugText;


    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
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
            }
            else

            placeableObject.transform.position = hitPos.position;
            placeableObject.transform.rotation = hitPos.rotation;
            Debug.Log("Inside: Move");

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
