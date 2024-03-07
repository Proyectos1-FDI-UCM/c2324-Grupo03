using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnAttackRangeCondition : MonoBehaviour, ICondition
{
    #region references
    private Transform _myTransform;
    #endregion

    [SerializeField] private float attackRange = 1.5f;
    public bool Validate(GameObject game)
    {
        float distanceToPlayer = Vector3.Magnitude(PlayerController.playerTransform.position - _myTransform.position);

        return distanceToPlayer <= attackRange;
    }

    private void Awake()
    {
        _myTransform = transform;
    }
}
