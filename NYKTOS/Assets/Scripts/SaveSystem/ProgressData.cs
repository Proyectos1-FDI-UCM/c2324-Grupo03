using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ProgressData
{
    private Dictionary<int, PlaceholderDefense> placeholderData;
    public Dictionary<int, PlaceholderDefense> PlaceholderData
    {
        get { return placeholderData; }
    }
    
    private int yellow;
    public int Yellow
    {
        get { return yellow; }
    }
    private int magenta;
    public int Magenta
    {
        get { return magenta; }
    }
    private int cyan; 
    public int Cyan
    {
        get { return cyan; }
    }

    private WeaponScriptableObject weapon;
    public WeaponScriptableObject Weapon
    {
        get { return weapon; }
    }

    private ProgressData
    (
        Dictionary<int, PlaceholderDefense> placeholderData, 
        int yellow, 
        int magenta, 
        int cyan, 
        WeaponScriptableObject weapon
    )
    {
        this.placeholderData = placeholderData;
        this.yellow = yellow;
        this.magenta = magenta;
        this.cyan = cyan;
        this.weapon = weapon;
    }

    private const string SAVEPATH = "/mondongo.save";

    public static void Save
    (
        Dictionary<int, PlaceholderDefense> placeholderData, 
        int yellow, 
        int magenta, 
        int cyan, 
        WeaponScriptableObject weapon
    )
    {
        ProgressData saveData = new ProgressData(placeholderData, yellow, magenta, cyan, weapon);

        string dataPath = Application.persistentDataPath + SAVEPATH;

        FileStream fileStream = new FileStream(dataPath, FileMode.Create);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, saveData);

        fileStream.Close();
    }

    public static ProgressData Load()
    {
        string dataPath = Application.persistentDataPath + SAVEPATH;

        ProgressData loadedData;

        if(SaveFileExists())
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
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
