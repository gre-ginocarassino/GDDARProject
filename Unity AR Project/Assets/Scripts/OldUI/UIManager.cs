using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.ARFoundation;

public class UIManager : MonoBehaviour
{
    [Header("Game Flow Variables")]
    public UIPanelValues introPanel;
    public UIPanelValues scanningPanel;
    public GameObject scanningIcon;
    public GameObject planeLoadingIcon;
    public bool planeSearch = false;

    [Header("Scalable Variables Variables")]
    public RectTransform canvas;
    float screenWidth, screenHeight;
    public Vector3[] newPlacement;

    [Header("UI Variables")]
    public UIPanelValues questionPanel;


    private static UIManager _instance;


    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //Scalable UI. Getting the Phone's Screen Size and using this.
        screenHeight = canvas.rect.height;
        screenWidth = canvas.rect.width;

        //sorting placement
        newPlacement[0] = new Vector3(0, screenHeight, 0);
        newPlacement[1] = new Vector3(screenWidth, 0, 0);
        newPlacement[2] = new Vector3(0, (screenHeight) * -1, 0);
        newPlacement[3] = new Vector3((screenWidth) * -1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //TitleTilter();
        LoadingRotater(scanningIcon);
    }

    public void SlideInOut(UIPanelValues panelS)
    {
        Debug.Log(newPlacement);

        if (panelS.isOpen)
        {
            panelS.transform.DOLocalMove(newPlacement[panelS.placement] ,1f);
            panelS.isOpen = false;
        } else
        {
            panelS.transform.DOLocalMove(new Vector3(0,0,0), 1f);
            panelS.isOpen = true;
        }
    }

    void LoadingRotater(GameObject loadingIcon)
    {
        loadingIcon.transform.Rotate(0, 0, -50 * Time.deltaTime);
    }

    public void OpenExternalLink(string url)
    {
        Application.OpenURL(url);
    }

    public void SwitchPlaneDetection(ARPlaneManager planeMan)
    {
        if (planeMan.isActiveAndEnabled)
            planeMan.enabled = false;
        else
            planeMan.enabled = true;
    }
}
