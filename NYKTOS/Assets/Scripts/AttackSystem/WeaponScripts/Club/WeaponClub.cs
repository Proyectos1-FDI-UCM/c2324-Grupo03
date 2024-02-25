using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClub : MonoBehaviour, IWeapon
{
    #region references
    [SerializeField]
    private GameObject attackHitbox;
    private Transform _myTransform;
    #endregion

    #region parameters
    [SerializeField]
    private float attackAngleRange = 30f; //area del barrido en grados, es decir, tamano del barrido
    [SerializeField] private float angleVelocity = 1f;
    #endregion

    #region properties

    #endregion
    public void PrimaryUse(Vector2 direction)
    {
        //se requiere instanciar el objeto a la rotacion de direction, pero como es un barrido tomara la direccion de direction-attackAngleRange
        GameObject currentHitbox = 
            Instantiate(attackHitbox, _myTransform.position, Quaternion.Euler(0, 0, DirectionAngle(direction) + attackAngleRange/2));
        currentHitbox.transform.parent = _myTransform;
        
        ClubHitboxBehaviour behaviour = currentHitbox.GetComponent<ClubHitboxBehaviour>();
        
        behaviour.attackAngleRange = attackAngleRange;
        behaviour.currentAngle = DirectionAngle(direction) + attackAngleRange / 2;
        behaviour.angleVelocity = angleVelocity;
    }

    public void SecondaryUse(Vector2 direction)
    {

    }

    private float DirectionAngle(Vector2 direction) //saca el angulo de la direccion dando por sentado que el modulo de la direccion es 1
    {
        float rad;
        
        
        //print(Mathf.Atan2(direction.y,direction.x)* Mathf.Rad2Deg);
        return (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
    private void Awake()
    {
        _myTransform = transform;
    }
    private void Update()
    {
        
    }
}
