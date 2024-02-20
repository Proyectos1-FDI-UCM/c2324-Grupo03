using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkComponent : MonoBehaviour
{
    #region references
    private RBMovement rbMovement;
    private Transform _myTransform;
    private BlinkHitbox blinkHitbox;
    #endregion

    #region properties
    private Ray ray;
    #endregion

    #region parameters
    [SerializeField] public float blinkRange = 2f;
    [SerializeField] public LayerMask whatLayerToDetect;
    #endregion

    public void Blink()
    {
        if (blinkHitbox != null)
        {
            if (!blinkHitbox.isColliding) //caso de que la hitbox no colisione con la pared ------> se teleporta a la distancia maxima del blink
            {
                rbMovement.TeleportTo(new Vector2 (_myTransform.position.x, _myTransform.position.y) 
                    + rbMovement.movementDirection * blinkRange);
            }
            else //caso contrario -----> se teleporta al punto de colision del raycast con la pared (un poco menos quizas para que no se encalle)
            {

            }
        }
    }

    private void Awake()
    {
        rbMovement = GetComponent<RBMovement>();    
        _myTransform = transform;
        blinkHitbox = GetComponentInChildren<BlinkHitbox>();
    }

    private void Update()
    {
        ray = new Ray(_myTransform.position, rbMovement.movementDirection);
        Debug.DrawRay(ray.origin, ray.direction*blinkRange);
    }
}
