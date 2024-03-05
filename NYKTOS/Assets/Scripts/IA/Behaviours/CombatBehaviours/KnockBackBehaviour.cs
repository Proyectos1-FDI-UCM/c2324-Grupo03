using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackBehaviour : MonoBehaviour, IBehaviour, IKnockback
{
    #region references
    private RBMovement _rbMovement;
    #endregion
    private Vector2 _pushPosition = Vector2.zero;

    public void PerformBehaviour()
    {
        _rbMovement.Knockback(_pushPosition);
    }

    public void CallKnockback(Vector2 pushPosition)
    {
        _pushPosition = pushPosition;
    }

    private void Awake()
    {
        _rbMovement = GetComponentInParent<RBMovement>();
    }
}
