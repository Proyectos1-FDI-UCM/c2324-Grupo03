using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderSaveComponent : MonoBehaviour
{
    [SerializeField]
    private PlaceholderDefense _currentDefense = PlaceholderDefense.None; 

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
        if(_saveData != null)
        {
            if(_saveData.CheckPlaceholder(_placeholderId))
            {
                _saveData.SetPlaceholderDefense(_placeholderId, _currentDefense);
            }
            else
            {
                _placeholderId = _saveData.AddPlaceholder(_currentDefense);
            }
        }
    }

    void OnDestroy()
    {
        #if UNITY_EDITOR 
            if(_placeholderId != -1 && _saveData != null)
            {
                _saveData.RemovePlaceholder(_placeholderId);
            }
        #endif
    }
}