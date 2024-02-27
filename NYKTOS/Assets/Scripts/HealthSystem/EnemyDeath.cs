using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyDeath : MonoBehaviour
{
    
    public void Die()
    {
        Destroy(this.gameObject);
        
    }
}
