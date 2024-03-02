using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaitCondition : MonoBehaviour, ICondition
{
    [SerializeField]
    private float _waitTime = 1f;
    private float currentTime = 0f;

    private GameObject previousGameObject;

    private bool isSame = false;
    public bool Validate(GameObject _object)
    {

        if (previousGameObject != _object)
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
