using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubHitboxBehaviour : MonoBehaviour
{
    #region properties
    //SE HACE LA ROTACION DESDE EL ANGULO QUE SE LE PASA HASTA EL ANGULO QUE SE LE PASA + ATTACKANGLERANGE

    public float attackAngleRange = 0f; //recorrido que va a hacer, se le pasa en el WeaponClub
    public float currentAngle = 0f; //angulo en el que se encuentra la hitbox
    private float maxAngle;
    public float angleVelocity;
    #endregion

    #region weaponProperties
    public int weaponDamage = 0;
    public AttackType attackType;
    #endregion

    #region references
    private Transform _myTransform;
    #endregion

    // Update is called once per frame
    void Update()
    {
        currentAngle = currentAngle - angleVelocity * Time.deltaTime;
        if (currentAngle >= maxAngle)
        {
            _myTransform.rotation = Quaternion.Euler(0,0,currentAngle);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.transform.parent.gameObject.TryGetComponent(out HealthComponent health)) //QUITAR VIDA
        {
            health.Damage(weaponDamage);
            //Javi ha hecho una correcci�n a este c�digo para que sea m�s limpio. Dejo este de ejemplo :)
        }

        if (collision.gameObject.transform.parent != null && collision.gameObject.transform.parent.GetComponentInChildren<IKnockback>()!= null)
        {
            
            IKnockback[] iknockbackChildren = collision.gameObject.transform.parent.GetComponentsInChildren<IKnockback>();

            for (int i = 0; i< iknockbackChildren.Length; i++)
            {
                iknockbackChildren[i].CallKnockback(_myTransform.parent.transform.position);
            }
            
        }

        if (attackType == AttackType.Fire && collision.transform.parent.gameObject.TryGetComponent( out SetOnFireDebuff setOnFire)) //DA�O DE FUEGO
        {
            setOnFire.enabled = true;
        }

        else if (attackType == AttackType.Slow && collision.transform.parent.gameObject.TryGetComponent(out SlowDebuff slow)) //RALENTIZAR
        {
            slow.enabled = true;
        }
    }

    private void Awake()
    {
        _myTransform = transform;
        
    }
    private void Start()
    {
        maxAngle = currentAngle - attackAngleRange;
    }
}
