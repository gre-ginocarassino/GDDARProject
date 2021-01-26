using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIPanelValues : MonoBehaviour
{
    //public Vector3 startVector3;
    //public Vector3 endVector3;

    [Header("Placement 0=Top , 1=Right , 2=Bottom , 3=Left")]
    public int placement;

    [Header("Does This Panel Start Open")]
    public bool isOpen;

    [Header("Should the Panel Start Open or Close Y/N = Open/Close")]
    public bool startOpen;

    private void Start()
    {
        if (startOpen)
            transform.DOLocalMove(new Vector3(0, 0, 0), 0);
        else
        {
            transform.DOLocalMove(UIManager.Instance.newPlacement[placement], 0f);
        }
    }
}
