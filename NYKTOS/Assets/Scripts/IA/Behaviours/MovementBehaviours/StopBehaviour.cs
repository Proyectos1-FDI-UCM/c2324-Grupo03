using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBehaviour : MonoBehaviour, IBehaviour
{
    private RBMovement _rbMovement;

    public void PerformBehaviour()
    {
        if (_rbMovement != null)
        {
            _rbMovement.StopVelocity();
        }
    }

    private void Awake()
    {
        _rbMovement = GetComponentInParent<RBMovement>();
    }
}
