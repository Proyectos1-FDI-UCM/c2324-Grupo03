using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkComponent : MonoBehaviour
{
    #region references
    private RBMovement rbMovement;
    private Transform _myTransform;
    private BlinkHitbox blinkHitbox;
    [SerializeField]
    private Collider2D entityHitbox;
    #endregion

    #region properties
    private Ray2D ray;
    private RaycastHit2D hit;
    public Vector2 blinkDirection { get { return _blinkDirection; } }
    private Vector2 _blinkDirection = Vector2.zero; //blink direction va a ser la direccion hacia donde apunta el blink. es lo mismo que rbMovement.movementDirection solo que sin poder ser vector zero.
    #endregion

    #region parameters
    [SerializeField] public float blinkRange = 2f;
    [SerializeField] public LayerMask[] whatLayerToDetect = new LayerMask[3]; //deteccion de las capas terrain collider, out of bounds y buildings

    [SerializeField] private float playerHitboxDissapearingTime;
    #endregion

    public void Blink()
    {
        if (blinkHitbox != null)
        {
            entityHitbox.enabled = false;
            if (!blinkHitbox.isColliding) //caso de que la hitbox no colisione con la pared ------> se teleporta a la distancia maxima del blink
            {
                rbMovement.TeleportTo(new Vector2 (_myTransform.position.x, _myTransform.position.y) 
                    + _blinkDirection * blinkRange);
            }
            else //caso contrario -----> se teleporta al punto de colision del raycast con la pared (un poco menos quizas para que no se encalle)
            {
                Vector2 closest = new Vector2(blinkRange, blinkRange);
                Vector2 closestHit = Vector2.zero;
                for (int i = 0; i < whatLayerToDetect.Length; i++) //ENTRE TODAS LAS CAPAS, BUSCA LA COLISION MAS CERCANA CON EL RAYCAST PARA TELEPORTARSE A ELLA
                {
                    hit = Physics2D.Raycast(ray.origin, ray.direction, blinkRange, whatLayerToDetect[i]);

                    Vector2 distanceToPlayer = new Vector2(hit.point.x - _myTransform.position.x, hit.point.y - _myTransform.position.y); //distancia del player

                    if ( distanceToPlayer.magnitude < closest.magnitude)
                    {
                        closest = distanceToPlayer;
                        closestHit = hit.point;
                    }
                }
                if (closestHit != Vector2.zero)
                    rbMovement.TeleportTo(closestHit - 0.25f * _blinkDirection);
            }

            Invoke("ActivatePlayerHitbox", playerHitboxDissapearingTime);
        }
    }

    private void ActivatePlayerHitbox()
    {
        entityHitbox.enabled= true;
    }

    private void Awake()
    {
        rbMovement = GetComponent<RBMovement>();    
        _myTransform = transform;
        blinkHitbox = GetComponentInChildren<BlinkHitbox>();
        
    }

    private void Update()
    {
        ray = new Ray2D(_myTransform.position, rbMovement.movementDirection);
        Debug.DrawRay(ray.origin, ray.direction * blinkRange);


        #region blinkDirection
        if (rbMovement != null && rbMovement.movementDirection!= Vector2.zero)
        {
            _blinkDirection = rbMovement.movementDirection;
        }
        #endregion

    }
}
