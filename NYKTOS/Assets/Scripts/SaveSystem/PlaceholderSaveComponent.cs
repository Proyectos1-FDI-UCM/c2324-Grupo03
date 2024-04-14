using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderSaveComponent : MonoBehaviour
{
    [SerializeField]
    private PlaceholderBuilding _currentBuilding = PlaceholderBuilding.None; 

    [SerializeField]
    private PlaceholderSaveData _saveData;

    [SerializeField]
    private int _placeholderId = -1;

    void Start()
    {
        Debug.Log(_placeholderId);
    }

    void OnValidate()
    {
        if(_placeholderId == -1)
        {
            _placeholderId = _saveData.AddPlaceholder(_currentBuilding);
        }
    }
}