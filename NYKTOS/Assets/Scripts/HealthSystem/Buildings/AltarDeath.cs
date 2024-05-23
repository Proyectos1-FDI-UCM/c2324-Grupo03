using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// No se usa
public class AltarDeath : MonoBehaviour, IDeath
{
    public void Death()
    {
        Destroy(gameObject);
    }
}
