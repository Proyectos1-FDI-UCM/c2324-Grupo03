using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Se le asigna un GameObject con un script IBehaviour, lee su contenido y reproduce lo que haya en Perform
/// </summary>
[System.Serializable]
public class BehaviourPerformer
{
    [SerializeField]
    private GameObject _behaviour;
    private IBehaviour _ibehaviour;

    private bool _initialized = false;

    public void Perform()
    {
        if (!_initialized)
        {
            _ibehaviour = _behaviour.GetComponent<IBehaviour>();
            _initialized = true;
        }

        _ibehaviour.PerformBehaviour();
    }
}
