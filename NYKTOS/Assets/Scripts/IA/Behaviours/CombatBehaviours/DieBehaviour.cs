using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DieBejaviour : MonoBehaviour, IBehaviour
{
    public void PerformBehaviour()
    {
        //Debug.Log("[DIE BEHAVIOUR] Muerte]");
        Destroy(GetComponentInParent<HealthComponent>().gameObject);
        
    }
}
