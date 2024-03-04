using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyDeath : MonoBehaviour, IDeath
{
    public void Death()
    {
        
        Destroy(gameObject);

    }

    public void Talk()
    {
        Debug.Log("Soy un enemigo!!");
    }
}
