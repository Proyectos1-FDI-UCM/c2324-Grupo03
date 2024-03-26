using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WaitCondition : MonoBehaviour, ICondition
{
    [SerializeField]
    private float _waitTime = 1f;
    private float currentTime = 0f;

    private GameObject previousGameObject;
    public bool Validate(GameObject _object)
    {

        if (PrefabUtility.GetCorrespondingObjectFromSource(previousGameObject) != PrefabUtility.GetCorrespondingObjectFromSource(_object))
        {
            currentTime = 0;
            previousGameObject = _object;
        }

        if (currentTime < _waitTime)
        {
            currentTime += Time.deltaTime;
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Awake()
    {
        previousGameObject = GetComponent<GameObject>();
    }

}
