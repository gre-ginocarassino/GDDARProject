using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LandMarkInfoController : MonoBehaviour
{
    bool isOpen;
    public GameObject infoPanel;
    public GameObject rotater;

    public void OpenCloseLandMarkInfo()
    {

    }

    private void Update()
    {
        rotater.transform.Rotate(0, 0, 3);
    }
}
