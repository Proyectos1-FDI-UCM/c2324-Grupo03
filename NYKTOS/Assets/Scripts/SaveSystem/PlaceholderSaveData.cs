using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "Placeholder Save Data", menuName = "SaveSystem/Placeholder Data", order = 1)]
public class PlaceholderSaveData : ScriptableObject
{
    private Dictionary<int, PlaceholderDefense> _currentPlaceholders = 
        new Dictionary<int, PlaceholderDefense>();
    public Dictionary<int, PlaceholderDefense> CurrentPlaceholders
    {
        get { return _currentPlaceholders;}
        set { _currentPlaceholders = value; }
    }
    
    [SerializeField]
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
        //Debug.Log(elementsInDictionary);

        return _idCount;
    }

    public void RemovePlaceholder(int id)
    {
        _currentPlaceholders.Remove(id);

        string elementsInDictionary = "";

        foreach(var element in _currentPlaceholders)
        {
            elementsInDictionary = elementsInDictionary + "\n" + "(" + element.Key + ", " + element.Value + ")";
        }
        //Debug.Log(elementsInDictionary);
    }

    public void SetPlaceholderDefense(int id, PlaceholderDefense defense)
    {
        if(_currentPlaceholders.ContainsKey(id))
        {
            _currentPlaceholders[id] = defense;
        }
        else
        {
            _currentPlaceholders.Add(id, defense);

            _idCount = (id > _idCount) ? id : _idCount;
        }

        string elementsInDictionary = "";
        foreach(var element in _currentPlaceholders)
        {   
            elementsInDictionary = elementsInDictionary + "\n" + "(" + element.Key + ", " + element.Value + ")";
        }
    }

    public PlaceholderDefense GetPlaceholderDefense(int id)
    {
        if (_currentPlaceholders.TryGetValue(id, out PlaceholderDefense value))
        {
            return value;
        }
        else
        {
            return PlaceholderDefense.None;
        }
    }

    /*
    void OnValidate()
    {

        string elementsInDictionary = "";
        foreach(var element in _currentPlaceholders)
        {
            elementsInDictionary = elementsInDictionary + "\n" + "(" + element.Key + ", " + element.Value + ")";
        }
        Debug.Log(elementsInDictionary);
    }
    */
}

public enum PlaceholderDefense
{
    None,
    Beacon,
    Wall,
    Turret
}