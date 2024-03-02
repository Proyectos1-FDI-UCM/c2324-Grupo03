using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _deathskin;

    [SerializeField]
    private SpriteRenderer _aliveskin;
    

    public bool alive = true;

    public void Death()
    {
       _aliveskin.enabled = false;
       _deathskin.enabled = true;
       alive = false;
       
    }
}
