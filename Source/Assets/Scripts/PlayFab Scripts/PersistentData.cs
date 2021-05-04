using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData PD;
    private MainController Main_Controller;
    public bool[] allSkins;

    public int mySkin;

    private void Start()
    {
        Main_Controller = (MainController)FindObjectOfType(typeof(MainController));
    }
    private void OnEnable()
    {
        PersistentData.PD = this;
    }

    public void SkinsStringToData(string skinsIn)
    {
        for (int i=0; i < skinsIn.Length; i++)
        {
            if (int.Parse(skinsIn[i].ToString()) > 0)
            {
                allSkins[i] = true;
            }
            else
            {
                allSkins[i] = false;
            }
        }
        Main_Controller.SetupStore();
    }

    public string SkinsDataToString()
    {
        string toString = "";
        for (int i=0; i < allSkins.Length; i++)
        {
            if (allSkins[i] == true)
            {
                toString += "1";
            }
            else
            {
                toString += "0";
            }
        }
        return toString;
    }
}
