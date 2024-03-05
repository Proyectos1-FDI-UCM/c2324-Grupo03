using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackTimeCondition : MonoBehaviour, ICondition
{
    private RBMovement _rbMovement;
    private float currentTime = 0f;
    public bool Validate(GameObject _object)
    {

        if (currentTime < _rbMovement.knockBackTime)
        {
            currentTime += Time.deltaTime;
            return false;
        }
        else
        {
            currentTime = 0;
            return true;
        }
    }

    private void Awake()
    {
        _rbMovement = GetComponentInParent<RBMovement>();
    }
}
