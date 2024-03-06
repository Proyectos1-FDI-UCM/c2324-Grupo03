using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackCondition : MonoBehaviour, ICondition, IKnockback
{
    private bool validateKnockback = false;
    public bool Validate(GameObject _gameObject)
    {
        if (validateKnockback)
        {
            validateKnockback = false;
            return true;
        }
        else return false;
    }

    public void CallKnockback(Vector2 pushPosition)
    {
        if (!validateKnockback)
        validateKnockback = true;
    }
}
