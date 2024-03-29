using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionChecker
{
    [SerializeField]
    private GameObject _condition;

    [SerializeField]
    private bool _negate = false;
    private ICondition _icondition;

    private bool _initialized = false;

    public bool Check(GameObject _object)
    {
        if (!_initialized)
        {
            _icondition = _condition.GetComponent<ICondition>();
            _initialized = true;
        }

        if (!_negate)
        {
            return _icondition.Validate(_object);
        }
        else return !_icondition.Validate(_object);
    }

}
