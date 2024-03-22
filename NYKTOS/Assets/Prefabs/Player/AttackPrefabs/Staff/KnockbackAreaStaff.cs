using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackAreaStaff : WeaponBehaviour
{
    float _radius;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Knockback(collision, transform);
    }

    public void SetKnockbackArea(float radius)
    {
        if (TryGetComponent<CircleCollider2D>(out CircleCollider2D collider))
        {
            collider.radius = radius;
            _radius = radius;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
