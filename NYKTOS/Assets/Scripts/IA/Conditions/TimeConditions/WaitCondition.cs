using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WaitCondition : MonoBehaviour, ICondition
{
    [SerializeField]
    private float _waitTime = 1f;
    bool waited = false;

    bool started = false;
    public bool Validate(GameObject _object)
    {
        if (!started) 
        StartCoroutine(Wait(_waitTime));

        print(waited);
        if (waited)
        {
            waited = false;
            started = false;
            return true;
        }
        else return false;
    }

    private IEnumerator Wait(float _waitTime)
    {
        started = true;
        yield return new WaitForSeconds(_waitTime);
        waited = true;
    }

    private void OnValidate()
    {
        gameObject.name = "Wait " + _waitTime + "sCondition";
    }
}
