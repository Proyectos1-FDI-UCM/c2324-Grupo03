using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RBMovement))]
public class SlowDebuff : MonoBehaviour
{
    #region references
    private RBMovement _myRBMovement;
    #endregion

    #region parameters
    [SerializeField] private float deactivateDebuffTime = 2f;
    [SerializeField] private float speedMultiplier = 0.5f;
    #endregion

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), deactivateDebuffTime);
        _myRBMovement.ChangeSpeed(_myRBMovement.movementSpeed * speedMultiplier, deactivateDebuffTime);
    }
    private void Deactivate()
    {
        this.enabled = false;
    }

    private void Awake()
    {
        _myRBMovement = GetComponent<RBMovement>();
        this.enabled = false;
    }
}
