using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkHitbox : MonoBehaviour
{
    #region references
    [SerializeField] private BlinkComponent blinkComponent;
    private Transform _hitboxTransform;
    private RBMovement rbMovement;
    #endregion

    public bool isColliding = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == blinkComponent.whatLayerToDetect)
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == blinkComponent.whatLayerToDetect)
        {
            isColliding = false;
        }
    }

    private void Awake()
    {
        blinkComponent= GetComponentInParent<BlinkComponent>();
        rbMovement = GetComponentInParent<RBMovement>();
        _hitboxTransform = transform;
        _hitboxTransform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        _hitboxTransform.localPosition = rbMovement.movementDirection * blinkComponent.blinkRange;
        print (rbMovement.movementDirection * blinkComponent.blinkRange);
        
    }
}
