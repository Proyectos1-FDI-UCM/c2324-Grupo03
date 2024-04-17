using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButtonEnabler : MonoBehaviour
{
    [SerializeField]
    private PlaceholderSaveData _placeholderData;

    void Awake()
    {
        if (_placeholderData == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            //gameObject.SetActive(_placeholderData.SaveFileExists());
        }
    }
}
