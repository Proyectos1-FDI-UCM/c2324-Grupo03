using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderSaveData : ScriptableObject
{
    private Dictionary<int, PlaceholderBuilding> _currentPlaceholders = 
        new Dictionary<int, PlaceholderBuilding>();
    private int _idCount = 0;

    public int AddPlaceholder(PlaceholderBuilding newPlaceholder)
    {
        _idCount++;
        _currentPlaceholders.Add(_idCount, newPlaceholder);
        return _idCount;
    }

    public void RemovePlaceholder(int id)
    {
        _currentPlaceholders.Remove(id);
    }
}

public enum PlaceholderBuilding
{
    None,
    Beacon,
    Wall,
    Turret
}