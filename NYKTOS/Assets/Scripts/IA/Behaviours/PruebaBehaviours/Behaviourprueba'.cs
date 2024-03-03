using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviourprueba: MonoBehaviour, IBehaviour
{
    private RBMovement _rbMovement;

    public void PerformBehaviour()
    {
        if (_rbMovement != null)
        {
            _rbMovement.xAxisMovement(1);
        }
    }

    private void Awake()
    {
        _rbMovement = GetComponentInParent<RBMovement>();
    }
}
