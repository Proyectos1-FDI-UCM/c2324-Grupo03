using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class ProgressData
{

    private int _night; 
    public int Night { get { return _night; }}

    private Dictionary<int, PlaceholderDefense> _placeholderData;
    public Dictionary<int, PlaceholderDefense> PlaceholderData { get { return _placeholderData; } }
    
    private int _yellow;
    public int Yellow { get { return _yellow; } }
    private int _magenta;
    public int Magenta { get { return _magenta; } }
    private int _cyan; 
    public int Cyan { get { return _cyan; } }

    private bool _upgradedWeapon;
    public bool UpgradedWeapon { get { return _upgradedWeapon; } }

    private bool _cinematicPlayed; 
    public bool CinematicPlayed { get { return _cinematicPlayed; } }

    private ProgressData
    (
        int night,
        Dictionary<int, PlaceholderDefense> placeholderData, 
        int yellow, 
        int magenta, 
        int cyan, 
        bool weapon,
        bool cinematicPlayed
    )
    {
        _night = night;
        _placeholderData = placeholderData;
        _yellow = yellow;
        _magenta = magenta;
        _cyan = cyan;
        _upgradedWeapon = weapon;    
        _cinematicPlayed = cinematicPlayed;
    }

    private const string SAVEPATH = "/savefile.save";
    public static string SavePath { get { return SAVEPATH;}}

    public static void Save
    (
        int night,
        Dictionary<int, PlaceholderDefense> placeholderData, 
        int yellow, 
        int magenta, 
        int cyan, 
        bool weapon,
        bool cinematicPlayed
    )
    {
        ProgressData saveData = new ProgressData(night, placeholderData, yellow, magenta, cyan, weapon, cinematicPlayed);

        string dataPath = Application.persistentDataPath + SAVEPATH;

        FileStream fileStream = new FileStream(dataPath, FileMode.Create);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, saveData);

        fileStream.Close();
    }

    public static ProgressData Load()
    {
        string dataPath = Application.persistentDataPath + SAVEPATH;
        Debug.Log("[PROGRESS DATA]" + dataPath);

        ProgressData loadedData;

        if(SaveFileExists())
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            fileStream.Position = 0;
            loadedData = (ProgressData) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }
        else
        {
            loadedData = null;
            Debug.Log("PATH NOT FOUND" + Application.persistentDataPath + SAVEPATH);
        }

        return loadedData;
    }

    public static bool SaveFileExists()
    {
        string dataPath = Application.persistentDataPath + SAVEPATH;

        return File.Exists(dataPath);
    }
}
