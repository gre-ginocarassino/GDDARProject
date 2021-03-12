using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTesting : MonoBehaviour
{
    public Transform camTrans;
    public bool usingUnity = false;

#if UNITY_EDITOR

    void Start()
    {
        Debug.Log("Using Unity Engine : Defining Different Camera Pos");
        transform.position = camTrans.position;
        transform.rotation = camTrans.rotation;

        usingUnity = true;
    }
#endif
}
