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
    public float slowTime { get { return deactivateDebuffTime; } }
    [SerializeField] private float deactivateDebuffTime = 2f;
    [SerializeField] private float speedAdder = -10f;
    #endregion

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), deactivateDebuffTime);
        _myRBMovement.AddSpeed(speedAdder, deactivateDebuffTime);

        renderer.color = Color.cyan;
    }
    private void Deactivate()
    {
        this.enabled = false;

        renderer.color = Color.white;
    }

    private void Awake()
    {
        _myRBMovement = GetComponent<RBMovement>();
        this.enabled = false;

        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    #region debug
    private SpriteRenderer renderer;
    #endregion
}
