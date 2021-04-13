using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public string FirstMonument;
    public string SecondMonument;
    public string ThirdMonument;
    public string FourthMonument;

    public void Load(string CountryName)
    {
        string Json = SaveSystem.Load(CountryName);
        CountryProp prop = new CountryProp();

        if (Json != null)
        {
            prop = JsonUtility.FromJson<CountryProp>(Json);

            FirstMonument = prop.QState[0];
            SecondMonument = prop.QState[1];
            ThirdMonument = prop.QState[2];
            FourthMonument = prop.QState[3];
        }
        else { SaveEmpty(CountryName); }
    }

    private void SaveEmpty(string CountryName)
    {
        List<string> EmptyList = new List<string>();
        EmptierList(ref EmptyList);

        CountryProp EmptyObject = new CountryProp
        {
            CountryName = CountryName,
            QState = EmptyList
        };

        string json = JsonUtility.ToJson(EmptyObject);
        SaveSystem.Save(json, CountryName);

        Load(CountryName);
    }

    void EmptierList(ref List<string> EmptyList)
    {
        for(int i=0;i < 4;i++)
        {
            EmptyList.Add("0");
        }
    }

}
