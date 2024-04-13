using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollidersBehaviour : MonoBehaviour, IBehaviour
{
    public void PerformBehaviour()
    {
        foreach (Collider2D a in GetComponentInParent<HealthComponent>().GetComponents<Collider2D>())
        {
            a.enabled = false;
        }
    }
}
