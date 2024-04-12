using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIsDeadCondition : MonoBehaviour, ICondition
{

    EnemyDeath _death;
    private void Awake()
    {
        _death = GetComponentInParent<EnemyDeath>();
    }
    public bool Validate(GameObject _object)
    {
        return _death.isDead;
    }
}
