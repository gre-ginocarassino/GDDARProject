using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractableObjectLandMarkInfo : InteracbleObject
{
    bool isOpen;
    public GameObject infoPanel;
    public GameObject rotater;

    public void OpenCloseLandMarkInfo()
    {
        if (isOpen)
        {
            isOpen = false;
            infoPanel.transform.DOScale(0, 1);
        } else
        {
            isOpen = true;
            infoPanel.transform.DOScale(1, 1);
        }
    }

    protected override void Update()
    {
        rotater.transform.Rotate(0, 0, 3);
    }

    public override void Interact()
    {
        base.Interact();

        OpenCloseLandMarkInfo();
    }
}
