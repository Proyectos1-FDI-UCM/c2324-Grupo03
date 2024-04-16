using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "Placeholder Save Data", menuName = "SaveSystem/Placeholder Data", order = 1)]
public class PlaceholderSaveData : ScriptableObject
{
    private bool _newGame = false;

    public void EnableNewGame()
    {
        _newGame = true;
    }

    private Dictionary<int, PlaceholderDefense> _currentPlaceholders = 
        new Dictionary<int, PlaceholderDefense>();
    private int _idCount = 0;

    public int AddPlaceholder(PlaceholderDefense newPlaceholder)
    {
        _idCount++;

        _currentPlaceholders.Add(_idCount, newPlaceholder);

        string elementsInDictionary = "";
        foreach(var element in _currentPlaceholders)
        {
            elementsInDictionary = elementsInDictionary + "\n" + "(" + element.Key + ", " + element.Value + ")";
        }
        Debug.Log(elementsInDictionary);

        return _idCount;
    }

    public bool CheckPlaceholder(int id)
    {
        return _currentPlaceholders.TryGetValue(id, out var value);
    }

    public void RemovePlaceholder(int id)
    {
        _currentPlaceholders.Remove(id);

        string elementsInDictionary = "";

        foreach(var element in _currentPlaceholders)
        {
            elementsInDictionary = elementsInDictionary + "\n" + "(" + element.Key + ", " + element.Value + ")";
        }
        Debug.Log(elementsInDictionary);
    }

    public void SetPlaceholderDefense(int id, PlaceholderDefense defense)
    {
        _currentPlaceholders[id] = defense;

        string elementsInDictionary = "";
        foreach(var element in _currentPlaceholders)
        {
            elementsInDictionary = elementsInDictionary + "\n" + "(" + element.Key + ", " + element.Value + ")";
        }
        Debug.Log(elementsInDictionary);
    }

    public void SavePlaceholderData()
    {
        string dataPath = Application.persistentDataPath + "/" + name + ".save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, _currentPlaceholders);
        fileStream.Close();
    }

    public void LoadPlaceholderData()
    {
        if(_newGame)
        {
            string dataPath = Application.persistentDataPath + "/" + name + ".save";

            if(File.Exists(dataPath))
            {
                FileStream fileStream = new FileStream(dataPath, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                _currentPlaceholders = (Dictionary<int, PlaceholderDefense>) binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
            else
            {
                Debug.Log("PATH NOT FOUND" + Application.persistentDataPath + "/" + name + ".save");
            }

            _newGame = false;
        }
    }

    public bool SaveFileExists()
    {
        string dataPath = Application.persistentDataPath + "/" + name + ".save";

        return File.Exists(dataPath);
    }
}



public enum PlaceholderDefense
{
    None,
    Beacon,
    Wall,
    Turret
}