﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatsManager : MonoBehaviour
{
    public List<string> monument = new List<string>();

    public string NameOfCountry;

    private MainController mainCont;

    private void Start()
    {
        mainCont = (MainController)FindObjectOfType(typeof(MainController));

        
    }

    public void Load(string CountryName)
    {
        NameOfCountry = CountryName;

        SaveSystem.Init();
        string Json = SaveSystem.Load(CountryName);
        CountryProp prop = new CountryProp();

        if (Json != null)
        {
            prop = JsonUtility.FromJson<CountryProp>(Json);

            //FirstMonument = prop.QState[0];
            //SecondMonument = prop.QState[1];
            //ThirdMonument = prop.QState[2];
            //FourthMonument = prop.QState[3];

            for (int i = 0; i < prop.QState.Count; i++)
            {
                monument[i] = prop.QState[i];
            }
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


    public void UpdateNumber(int index, int score)
    {
        int number = Convert.ToInt32(monument[index]);
        Debug.Log("Updating Score");
        if (number < score)
        {
            monument[index] = score.ToString();

            int newPoints = (score - number) * 10;
            //Debug.Log(NameOfCountry);


            if (NameOfCountry == "england")
            {
                mainCont.totalEngland = mainCont.totalEngland + newPoints;
            }
            if (NameOfCountry == "italy")
            {
                mainCont.totalItaly = mainCont.totalItaly + newPoints;
            }
            if (NameOfCountry == "france")
            {
                mainCont.totalFrance = mainCont.totalFrance + newPoints;
            }

            
            CountryProp EmptyObject = new CountryProp
            {
                CountryName = NameOfCountry,
                QState = monument
            };

            string json = JsonUtility.ToJson(EmptyObject);
            SaveSystem.Save(json, NameOfCountry);
            mainCont.UpdateGameStatistics();
            mainCont.StartCloudUpdatePlayerStats();
        }

    }

}
