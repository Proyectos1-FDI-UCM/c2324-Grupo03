using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerBehaviour : MonoBehaviour, IBehaviour
{
    #region references
    private WeaponHandler _weaponHandler;
    private Transform _myTransform;
    #endregion
    public void PerformBehaviour()
    {
        _weaponHandler.CallPrimaryUse(PlayerController.playerTransform.position - _myTransform.position);
    }

    private void Awake()
    {
        _weaponHandler = GetComponentInParent<WeaponHandler>();
        _myTransform = transform;
    }
}
