using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string json, string name)
    {
        File.WriteAllText(SAVE_FOLDER + "/" + name + ".txt", json);
    }

    public static string Load(string name)
    {
        string SaveString;

        if (Directory.Exists(Application.persistentDataPath + "/Saves/"))
        {
            if (File.Exists(SAVE_FOLDER + "/" + name + ".txt"))
            {
                SaveString = File.ReadAllText(SAVE_FOLDER + "/" + name + ".txt");

                return SaveString;
            }
            else { return null; }
        }
        else
        {
            return null;
        }
    }
}
