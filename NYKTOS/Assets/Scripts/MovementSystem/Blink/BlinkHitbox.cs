using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkHitbox : MonoBehaviour
{
    #region references
    private BlinkComponent blinkComponent;
    private Transform _hitboxTransform;
    private RBMovement rbMovement;
    private SpriteRenderer renderer;
    #endregion

    public bool isColliding = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        //el numero de capa de whatLayerToDetect, es decir, TerrainCollision, es 3. OutOfBounds es 6.
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
        {
            isColliding = true;
            renderer.color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
        {
            isColliding = false;
            renderer.color = Color.green;
        }
    }

    private void Awake()
    {
        blinkComponent= GetComponentInParent<BlinkComponent>();
        rbMovement = GetComponentInParent<RBMovement>();
        _hitboxTransform = transform;
        _hitboxTransform.localPosition = Vector3.zero;
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _hitboxTransform.localPosition = blinkComponent.blinkDirection * blinkComponent.blinkRange;
    }
}
