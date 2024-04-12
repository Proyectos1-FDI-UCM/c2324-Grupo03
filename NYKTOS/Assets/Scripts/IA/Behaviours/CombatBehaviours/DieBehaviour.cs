using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DieBejaviour : MonoBehaviour, IBehaviour
{
    public void PerformBehaviour()
    {
        Debug.Log("morir");
        Destroy(GetComponentInParent<HealthComponent>().gameObject);
    }
}
