using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class SaveData
{
    private Dictionary<int, PlaceholderDefense> _placeholderData;
    
    private int yellow;
    private int magenta;
    private int cyan; 

    const string SAVEPATH = "/mondongo.save";
    private static bool _newGame = false;

    public static void SavePlaceholderData(PlaceholderSaveData placeholderSaveData)
    {
        string dataPath = Application.persistentDataPath + SAVEPATH;
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, placeholderSaveData._currentPlaceholders);
        fileStream.Close();
    }

    public static void LoadPlaceholderData(ref PlaceholderSaveData placeholderSaveData)
    {
        if(_newGame)
        {
            string dataPath = Application.persistentDataPath + SAVEPATH;

            if(File.Exists(dataPath))
            {
                FileStream fileStream = new FileStream(dataPath, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                placeholderSaveData._currentPlaceholders = (Dictionary<int, PlaceholderDefense>) binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
            else
            {
                Debug.Log("PATH NOT FOUND" + Application.persistentDataPath + SAVEPATH);
            }

            _newGame = false;
        }
    }

    public static bool SaveFileExists()
    {
        string dataPath = Application.persistentDataPath + SAVEPATH;

        return File.Exists(dataPath);
    }
}
